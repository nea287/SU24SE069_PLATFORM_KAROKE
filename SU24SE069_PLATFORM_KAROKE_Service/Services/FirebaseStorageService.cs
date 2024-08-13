using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SU24SE069_PLATFORM_KAROKE_Service.Commons;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private const string KaraokeVideoDirectory = "KaraokeVideos";
        private readonly FirebaseCredential firebaseCredential;

        public FirebaseStorageService(IOptions<FirebaseCredential> options)
        {
            firebaseCredential = options.Value;
        }

        public async Task<(bool, string)> UploadKaraokeVideoAsync(IFormFile videoFile)
        {
            if (!IsVideoFile(videoFile))
            {
                Console.WriteLine($"Error: Uploaded file is not a video file");
                return (false, "Uploaded file is not a video");
            }

            var firebaseAuthLink = await GetFirebaseAuthentication();
            var cancellation = new CancellationTokenSource();

            try
            {
                using var stream = videoFile.OpenReadStream();

                var firebaseStorageOptions = new FirebaseStorageOptions()
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(firebaseAuthLink.FirebaseToken),
                    ThrowOnCancel = true
                };

                var task = new FirebaseStorage(firebaseCredential.Bucket, firebaseStorageOptions)
                    .Child(KaraokeVideoDirectory)
                    .Child(Path.GetFileName(videoFile.FileName))
                    .PutAsync(stream, cancellation.Token);

                return (true, await task);
            }
            catch (FirebaseStorageException ex)
            {
                Console.WriteLine($"Firebase error: {ex.Message}");
                return (false, "An unexpected FirebaseStorageException occurred");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return (false, "An unexpected exception occurred");
            }
            finally
            {
                cancellation.Dispose();
            }
        }

        public async Task<bool> RemoveKaraokeVideoAsync(string fileName)
        {
            try
            {
                var firebaseAuthLink = await GetFirebaseAuthentication();

                var firebaseStorageOptions = new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(firebaseAuthLink.FirebaseToken),
                    ThrowOnCancel = true
                };

                var firebaseStorage = new FirebaseStorage(
                    firebaseCredential.Bucket, firebaseStorageOptions);

                // Build the path to the file in Firebase Storage
                var fileReference = firebaseStorage
                    .Child(KaraokeVideoDirectory)
                    .Child(fileName);

                // Delete the file
                await fileReference.DeleteAsync();

                // Indicating success
                return true;
            }
            catch (FirebaseStorageException ex)
            {
                Console.WriteLine($"Firebase error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private async Task<FirebaseAuthLink> GetFirebaseAuthentication()
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(firebaseCredential.ApiKey));
            return await auth.SignInWithEmailAndPasswordAsync(firebaseCredential.AuthEmail, firebaseCredential.AuthPassword);
        }

        private bool IsVideoFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            var allowedExtensions = new[] { ".mp4", ".avi", ".mov", ".wmv", ".mkv", ".flv", ".webm", ".mpeg", ".mpg", ".m4v" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return allowedExtensions.Contains(fileExtension);
        }
    }
}
