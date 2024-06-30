using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class PostCommentRepository : BaseRepository<PostComment>, IPostCommentRepository
    {
        public async Task<bool> CreateComment(PostComment request)
        {
            try
            {
                await InsertAsync(request);
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool ExistedComment(Guid id)
          => Any(x => x.ParentCommentId == id);

        public async Task<PostComment> GetComment(Guid id)
        {
            PostComment data = new PostComment();
            try
            {
                data = await GetByIdGuid(id);
            }catch(Exception ex)
            {
                return null;
            }

            return data;
        }

        public async Task<bool> UpdateComment(PostComment request)
        {
            try
            {
                await Update(request);
                await SaveChagesAsync();

            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
