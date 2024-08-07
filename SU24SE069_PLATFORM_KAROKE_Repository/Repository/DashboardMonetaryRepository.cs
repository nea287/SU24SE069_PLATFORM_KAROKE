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
                if (date.HasValue)
                {
                    var data = GetAll(x => x.CreatedDate.Date == date)
                        .GroupBy(transaction => transaction.CreatedDate.Date)
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
                    var data = GetAll(x => x.CreatedDate.Date >= startDate && x.CreatedDate.Date <= endDate)
                          .GroupBy(transaction => transaction.CreatedDate.Date)
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

        public async Task<Dictionary<int, decimal>> GetDashboardByMonth(int? month = null, int? startMonth = null, int? endMonth = null, int? startYear = null, int? endYear = null)
        {
            Dictionary<int, decimal> result = new Dictionary<int, decimal>();

            try
            {

                if (month.HasValue)
                {

                     var data = GetAll(x => x.CreatedDate.Month == month.Value && x.CreatedDate.Year >= startYear && x.CreatedDate.Year <= endYear)
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
                    //var data1 = Enumerable.Range(startMonth.Value, (endMonth.Value - (startMonth.Value == 1? 0:startMonth.Value)));
                    var data1 = Enumerable.Range(startMonth.Value, (endMonth.Value - startMonth.Value + 1));


                    var data = GetAll(x => x.CreatedDate.Month >= startMonth && x.CreatedDate.Month <= endMonth
                                       && x.CreatedDate.Year >= startYear && x.CreatedDate.Year <= endYear)
                        .GroupBy(transaction => transaction.CreatedDate.Month)
                        .Select(group => new
                        {
                            Month = group.Key,
                            TotalAmount = group.Sum(transaction => transaction.MoneyAmount)
                        });

                    data = data1.GroupJoin(data, month => month, data => data.Month, (month, data) => new
                    {
                        Month = month,
                        TotalAmount = data.FirstOrDefault()?.TotalAmount ?? 0
                    }).AsQueryable();


                    foreach(var item in data)
                    {
                        result.Add(item.Month, item.TotalAmount);
                    }

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
