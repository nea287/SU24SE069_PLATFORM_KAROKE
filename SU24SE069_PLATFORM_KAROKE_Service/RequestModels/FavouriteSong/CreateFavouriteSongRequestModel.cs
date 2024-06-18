using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.FavouriteSong
{
    public class CreateFavouriteSongRequestModel
    {
        public Guid MemberId { get; set; }
        public Guid SongId { get; set; }
    }
}
