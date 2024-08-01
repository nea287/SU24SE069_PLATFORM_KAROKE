using Microsoft.EntityFrameworkCore;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class SongRepository : BaseRepository<Song>, ISongRepository
    {
        public async Task<bool> CreateSong(Song song)
        {
            try
            {
                await InsertAsync(song);
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

        public async Task<Song> GetSong(Guid id)
        {
            Song result = new Song();
            try
            {
                result = await FirstOrDefaultAsync(x => x.SongId == id && x.SongStatus != 0);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public async Task<Song> GetSongByCode(string code)
        {
            Song result = new Song();
            try
            {
                result = await FirstOrDefaultAsync(x => x.SongCode.ToLower().Equals(code));
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<bool> UpdateSong(Guid id, Song song)
        {
            try
            {
                await UpdateGuid(song, id);
                SaveChages();
                

            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteSong(Guid id)
        {
            try
            {
                Song data = await GetSong(id);
                data.SongStatus = 0;

                _ = UpdateGuid(data, id);
                SaveChages();
            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<(int, List<Song>)> GetSongsPurchaseFavoriteFiltered(int pageNumber, int pageSize, Expression<Func<Song, bool>>? filter = null, Func<IQueryable<Song>, IOrderedQueryable<Song>>? orderBy = null, bool isTracking = false)
        {
            IQueryable<Song> query = GetDbSet().AsQueryable();
            try
            {
                query = !isTracking ? query.AsNoTracking() : query.AsTracking();
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                 query = query.Include(s => s.SongArtists)
                .ThenInclude(sa => sa.Artist)
                .Include(s => s.SongSingers)
                .ThenInclude(ss => ss.Singer)
                .Include(s => s.SongGenres)
                .ThenInclude(sg => sg.Genre);

                int totalCount = await query.CountAsync();

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                return (totalCount, await query.ToListAsync());
            }
            catch (Exception ex)
            {
                throw new Exception($"[GetSongsPurchaseFavoriteFiltered]: Error when trying to query song data: " + ex.Message);
            }
        }

        public async Task<Song?> GetSongById(Guid id)
        {
            return await GetDbSet().Include(s => s.SongArtists).ThenInclude(sa => sa.Artist)
                .Include(s => s.SongSingers).ThenInclude(ss => ss.Singer)
                .Include(s => s.SongGenres).ThenInclude(sg => sg.Genre)
                .FirstOrDefaultAsync(s => s.SongId == id);
        }
    }
}
