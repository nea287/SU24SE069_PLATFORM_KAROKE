﻿using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item
{
    public class UpdateItemRequestModel
    {
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string? ItemCode { get; set; }
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string? ItemName { get; set; } 
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string? ItemDescription { get; set; }
        public ItemType? ItemType { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public decimal? ItemBuyPrice { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public decimal? ItemSellPrice { get; set; }
        public ItemStatus? ItemStatus { get; set; }
        public bool? CanExpire { get; set; }
        public bool? CanStack { get; set; }
        //public DateTime CreatedDate { get; set; }
        public Guid? CreatorId { get; set; }
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string? PrefabCode { get; set; }
    }
}
