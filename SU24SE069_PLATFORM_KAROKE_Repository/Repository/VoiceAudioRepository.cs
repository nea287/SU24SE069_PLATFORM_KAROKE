using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class VoiceAudioRepository : BaseRepository<VoiceAudio>, IVoiceAudioRepository
    {
        public async Task<bool> DeleteVoiceAudio(Guid voiceId)
        {
            try
            {
                await this.HardDeleteGuid(voiceId);
                //await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteVoiceAudios(IQueryable<VoiceAudio> voiceAudios)
        {
            try
            {
                 DeleteRange(voiceAudios);
                SaveChages();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
