using SU24SE069_PLATFORM_KAROKE_DAO.DAO;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class RecordingRepository : BaseRepository<Recording>, IRecordingRepository
    {
        public async Task<bool> AddRecording(Recording recording)
        {
            try
            {
                await InsertAsync(recording);
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteRecording(Guid id)
        {
            try
            {
                await HardDeleteGuid(id);
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExistedRecording(Guid id)
            => Any(x => x.RecordingId == id);

        public async Task<Recording> GetRecording(Guid id)
        {
            Recording? rs = new Recording();
            try
            {
                rs = await FirstOrDefaultAsync(x => x.RecordingId == id);

            }catch(Exception ex)
            {
                return rs = null;
            }
            return rs;
        }

        public async Task<bool> UpdateRecording(Recording recording)
        {
            try
            {
                await UpdateGuid(recording, recording.RecordingId);
                SaveChages();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
