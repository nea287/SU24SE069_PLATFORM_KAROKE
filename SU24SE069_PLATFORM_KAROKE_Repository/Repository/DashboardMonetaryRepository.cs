using Microsoft.EntityFrameworkCore;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class DashboardMonetaryRepository : BaseRepository<MonetaryTransaction>, IDashboardMonetaryRepository
    {
        public async Task<Dictionary<DateTime, decimal>> GetDashboardByDate(DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            Dictionary<DateTime, decimal> result = new Dictionary<DateTime, decimal>();
            try
            {
                //startDate = startDate ?? DateTime.Now;
                //endDate = endDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                if (date.HasValue)
                {
                    var data = GetAll(x => x.CreatedDate == date)
                        .GroupBy(transaction => transaction.CreatedDate)
                        .Select(group => new
                        {
                            Date = group.Key,
                            TotalAmount = group.Sum(transaction => transaction.MoneyAmount)
                        });

                    await data.ForEachAsync(x =>
                    {
                        result.Add(x.Date, x.TotalAmount);
                    });
                }
                else
                {
                    var data = GetAll(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                          .GroupBy(transaction => transaction.CreatedDate)
                          .Select(group => new
                          {
                              Date = group.Key,
                              TotalAmount = group.Sum(transaction => transaction.MoneyAmount)
                          });


                    await data.ForEachAsync(x =>
                    {
                        result.Add(x.Date, x.TotalAmount);
                    });
                }

            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }

        public async Task<Dictionary<int, decimal>> GetDashboardByMonth(int? month = null, int? startMonth = null, int? endMonth = null, int? year = null)
        {
            Dictionary<int, decimal> result = new Dictionary<int, decimal>();

            try
            {
                //startMonth = startMonth?? DateTime.Now.Month;
                //endMonth = startMonth?? DateTime.Now.Month;
                if (month.HasValue)
                {
                    var data = GetAll(x => x.CreatedDate.Month == month.Value && x.CreatedDate.Year == year.Value)
                                 .GroupBy(transaction => transaction.CreatedDate.Month)
                                 .Select(group => new
                                 {
                                     Month = group.Key,
                                     TotalAmount = group.Sum(transaction => transaction.MoneyAmount)
                                 });

                    await data.ForEachAsync(x =>
                    {
                        result.Add(x.Month, x.TotalAmount);
                    });
                }
                else
                {
                    var data = GetAll(x => x.CreatedDate.Month >= startMonth && x.CreatedDate.Month <= endMonth
                                       && x.CreatedDate.Year == year)
                        .GroupBy(transaction => transaction.CreatedDate.Month)
                        .Select(group => new
                        {
                            Month = group.Key,
                            TotalAmount = group.Sum(transaction => transaction.MoneyAmount)
                        });

                    await data.ForEachAsync(x =>
                    {
                        result.Add(x.Month, x.TotalAmount);
                    });
                }
            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }

        public async Task<Dictionary<int, decimal>> GetDashboardByYear(int? year = null, int? fromYear = null, int? toYear = null)
        {
            Dictionary<int, decimal> result = new Dictionary<int, decimal>();
            try
            {
                //fromYear = fromYear ?? DateTime.Now.Year;
                //toYear = toYear ?? DateTime.Now.Year;
                if (year.HasValue)
                {
                    var data = GetAll(x => x.CreatedDate.Year == year.Value && x.CreatedDate.Year == year.Value)
                                 .GroupBy(transaction => transaction.CreatedDate.Year)
                                 .Select(group => new
                                 {
                                     Year = group.Key,
                                     TotalAmount = group.Sum(transaction => transaction.MoneyAmount)
                                 });

                    await data.ForEachAsync(x =>
                    {
                        result.Add(x.Year, x.TotalAmount);
                    });
                }
                else
                {
                    var data = GetAll(x => x.CreatedDate.Year >= fromYear && x.CreatedDate.Year <= toYear)
                        .GroupBy(transaction => transaction.CreatedDate.Year)
                        .Select(group => new
                        {
                            Year = group.Key,
                            TotalAmount = group.Sum(transaction => transaction.MoneyAmount)
                        });
                    await data.ForEachAsync(x =>
                    {
                        result.Add(x.Year, x.TotalAmount);
                    });
                }
            }
            catch (Exception)
            {
                return null;
            }

            return result;
        }
    }
}
