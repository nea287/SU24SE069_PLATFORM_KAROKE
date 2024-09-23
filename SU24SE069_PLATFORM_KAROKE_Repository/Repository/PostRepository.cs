using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public async Task<bool> AddPost(Post post)
        {
            try
            {
                await InsertAsync(post);
                await SaveChagesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<float> GetPostScore(Guid id)
        {
            float score = 0;
            try
            {
                var postRating = await GetByIdGuid(id);


                score = (postRating.PostRatings.Sum(r => r.Score))/(postRating.PostRatings.Count());
            }
            catch (Exception)
            {
                throw;
            }

            return score;
        }

        public async Task<IQueryable<Post>> UpdateScores(string? include)
        {

            IQueryable<Post> data;
            try
            {
                await DisponseAsync();

                data = GetAll(includeProperties: include);

                //AttachEntities(data);

                data = data.AsEnumerable() 
                          .Select(item =>
                          {
                              item.Score = item.PostRatings.Any()
                                            ? item.PostRatings.Sum(a => a.Score) / item.PostRatings.Count()
                                            : 0;
                              return item;
                          })
                          .AsQueryable();

                

                SaveChages();


            }catch(Exception)
            {
                return null;
            }

            return data;
        }

        public async Task<Post> GetPostOrign(Guid PostId)
        {
            Post data = new Post();
            try
            {
                data = await FirstOrDefaultAsync(x => x.OriginPostId == PostId);
            }
            catch (Exception)
            {
                return null;
            }

            return data;
        }

        public async Task<bool> DeletePost(Guid id)
        {
            try
            {
                await HardDeleteGuid(id);
                SaveChages();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExistedPost(Guid id)
            => Any(x => x.PostId == id);

        public async Task<Post> GetPost(Guid id)
        {
            Post? rs = new Post();
            try
            {
                rs = await FirstOrDefaultAsync(x => x.PostId == id);

            }
            catch (Exception ex)
            {
                return rs = null;
            }
            return rs;
        }

        public async Task<bool> UpdatePost(Post post)
        {
            try
            {
                await UpdateGuid(post, post.PostId);
                SaveChages();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
