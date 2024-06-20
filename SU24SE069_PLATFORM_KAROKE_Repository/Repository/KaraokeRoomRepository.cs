using Microsoft.EntityFrameworkCore;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class KaraokeRoomRepository : BaseRepository<KaraokeRoom>, IKaraokeRoomRepository
    {
        public async Task<bool> CreateRoom(KaraokeRoom request)
        {
            try
            {
                await InsertAsync(request);
                await SaveChagesAsync();

            } catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool ExistedRoom(string? roomLog = null, Guid? id = null)
            => Any(x => x.RoomLog.Equals(roomLog ?? "null") || x.RoomId == (id??new Guid()));

        public async Task<KaraokeRoom> GetRoom( Guid id)
        {
            var rs = new KaraokeRoom();
            try
            {
                rs = await FirstOrDefaultAsync(x => x.RoomId == id);

            }
            catch(Exception ex)
            {
                return null;
            }

            return rs;
        }

        public async Task<bool> UpdateRoom(KaraokeRoom room)
        {
            try
            {
                await Update(room);
                await SaveChagesAsync();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
