using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface ISongRepository : IBaseRepository<Song>
    {
        public Task<Song> GetSong(Guid id);
        public Task<Song> GetSongByCode(string code);
        public Task<bool> CreateSong(Song song);
        public Task<bool> UpdateSong(Guid id, Song song);
        public bool ExistedSong(string code);
        public Task<bool> DeleteSong(Guid id);
    }
}
