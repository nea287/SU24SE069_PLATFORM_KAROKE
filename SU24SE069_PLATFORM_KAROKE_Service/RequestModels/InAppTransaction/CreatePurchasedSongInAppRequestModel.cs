using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PurchasedSong;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.InAppTransaction
{
    public class CreatePurchasedSongInAppRequestModel
    {
        //public InAppTransactionStatus Status { get; set; }
        //public DateTime CreatedDate { get; set; }
       // public InAppTransactionType TransactionType { get; set; }
       public ICollection<PurchasedSongRequestModel> PurchasedSongs { get; set; }

    }
}
