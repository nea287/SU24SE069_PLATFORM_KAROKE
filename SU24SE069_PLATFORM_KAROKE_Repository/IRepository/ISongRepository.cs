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
        public Song GetSong(Guid id);
        public Song GetSongByCode(string code);
        public bool CreateSong(Song song);
        public bool UpdateSong(Guid id, Song song);
        public bool ExistedSong(string code);
        public bool DeleteSong(Guid id);
    }
}
