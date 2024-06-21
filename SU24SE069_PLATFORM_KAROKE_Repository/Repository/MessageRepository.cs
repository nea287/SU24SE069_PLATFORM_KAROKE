﻿using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public async Task<bool> CreateMessage(Message request)
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
    }
}
