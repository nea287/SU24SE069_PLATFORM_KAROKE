using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class SupportRequestRepository : BaseRepository<Ticket>, ISupportRequestRepository
    {
        public async Task<bool> AddRequest(Ticket request)
        {
            try
            {
                await InsertAsync(request);
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateRequest(Ticket request)
        {
            try
            {
                await Update(request);
                await SaveChagesAsync();
            } catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
