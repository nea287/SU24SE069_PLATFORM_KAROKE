using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class AccountCharacter
    {
        public Guid AccountId { get; set; }
        public Guid CharacterId { get; set; }
    }
}
