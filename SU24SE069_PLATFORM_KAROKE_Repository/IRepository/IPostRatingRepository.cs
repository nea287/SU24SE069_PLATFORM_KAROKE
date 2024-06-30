using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IPostRatingRepository : IBaseRepository<PostRating>
    {
        public Task<bool> AddRating(PostRating request);
        public Task<bool> UpdateRating(PostRating request);
        public Task<PostRating> GetRating(Guid memberId, Guid postId);
        public bool ExistedRating(Guid memberId, Guid postId);
        public Task<bool> DeleteRating(Guid memberId, Guid postId);
    }
}
