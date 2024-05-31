using SU24SE069_PLATFORM_KAROKE_DAO.IDAO;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DAO.DAO
{
    public class AccountDAO : BaseDAO<Account>, IAccountDAO
    {
        public AccountDAO(PLATFORM_KARAOKEContext context) : base(context)
        {
        }
    }
}
