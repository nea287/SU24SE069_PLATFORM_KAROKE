using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class SongRepository : BaseRepository<Song>, ISongRepository
    {
        public bool CreateSong(Song song)
        {
            try
            {
                Insert(song);
                SaveChages();

            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExistedSong(string code)
        {
            return Any(x => x.SongCode.ToLower().Equals(code));
        }

        public Song GetSong(Guid id)
        {
            Song result = new Song();
            try
            {
                result = GetFirstOrDefault(x => x.SongId == id && x.SongStatus != 0);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public Song GetSongByCode(string code)
        {
            Song result = new Song();
            try
            {
                result = GetFirstOrDefault(x => x.SongCode.ToLower().Equals(code));
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool UpdateSong(Guid id, Song song)
        {
            try
            {
                Song data = GetSong(id);

                song.CreatedDate = data.CreatedDate;
                song.UpdatedDate = DateTime.Now;
                song.SongStatus = data.SongStatus;
                song.SongId = id;

                _ = UpdateGuid(song, id);
                SaveChages();
                

            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteSong(Guid id)
        {
            try
            {
                Song data = GetSong(id);
                data.SongStatus = 0;

                _ = UpdateGuid(data, id);
                SaveChages();
            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
