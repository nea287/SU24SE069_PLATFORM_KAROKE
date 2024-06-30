using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IPostCommentRepository : IBaseRepository<PostComment>
    {
        public Task<bool> CreateComment(PostComment request);
        public Task<bool> UpdateComment(PostComment request);
        //public Task<bool> DeteleComment(PostComment request);
        public bool ExistedComment(Guid id);
        public Task<PostComment> GetComment(Guid id);    
    }
}
