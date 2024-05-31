using SU24SE069_PLATFORM_KAROKE_DAO.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IBaseRepository <TEntity> : IBaseDAO<TEntity> where TEntity : class
    {
    }
}
