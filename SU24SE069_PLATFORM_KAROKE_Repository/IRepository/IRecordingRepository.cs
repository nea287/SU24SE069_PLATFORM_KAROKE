using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IRecordingRepository : IBaseRepository<Recording>
    {
        public Task<bool> AddRecording(Recording recording);
        public Task<bool> DeleteRecording(Guid id);
        public Task<bool> UpdateRecording(Recording recording);
        public bool ExistedRecording(Guid id);    
        public Task<Recording> GetRecording(Guid id);
    }
}
