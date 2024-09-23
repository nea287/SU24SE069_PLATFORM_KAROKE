using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        public Task<bool> AddPost(Post post);
        public Task<bool> DeletePost(Guid id);
        public Task<bool> UpdatePost(Post post);
        public bool ExistedPost(Guid id);
        public Task<Post> GetPost(Guid id);
        public Task<float> GetPostScore(Guid id);
        public Task<IQueryable<Post>> UpdateScores(string? include);
        public Task<Post> GetPostOrign(Guid PostId);
    }
}
