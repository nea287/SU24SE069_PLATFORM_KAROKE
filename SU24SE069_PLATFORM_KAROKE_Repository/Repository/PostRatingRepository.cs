using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class PostRatingRepository : BaseRepository<PostRating>, IPostRatingRepository
    {
        public async Task<bool> AddRating(PostRating request)
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

        public async Task<bool> DeleteRating(Guid memberId, Guid postId)
        {
            try
            {
                var data = await FirstOrDefaultAsync(x => x.MemberId == memberId && x.PostId == postId);

                Delete(data);
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool ExistedRating(Guid memberId, Guid postId)
        {
            return Any(x => x.MemberId == memberId && x.PostId == postId);
        }

        public async Task<PostRating> GetRating(Guid memberId, Guid postId)
        {
            PostRating data = new PostRating();
            try
            {
                data = await FirstOrDefaultAsync(x => x.MemberId == memberId && x.PostId == postId);
            }catch(Exception ex)
            {
                return null;
            }

            return data;
        }

        public async Task<bool> UpdateRating(PostRating request)
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
