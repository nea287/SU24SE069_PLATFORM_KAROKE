﻿using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardGameRepository _gameRepository;
        private readonly IDashboardMonetaryRepository _monetaryRepository;

        public DashboardService(IDashboardGameRepository gameRepository, IDashboardMonetaryRepository monetaryRepository)
        {
            _gameRepository = gameRepository;
            _monetaryRepository = monetaryRepository;
        }

        public async Task<DashboardResponse<DateTime>> GetDashboardGamebyDate(DateRequestModel request)
        {
            Dictionary<DateTime, decimal> result = new Dictionary<DateTime, decimal>();
            try
            {
                //request.StartDate = request.StartDate ?? DateTime.Now;
                //request.EndDate = request.EndDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                result = await _gameRepository.GetDashboardByDate(request.Date, request.StartDate, request.EndDate) ?? throw new Exception();

            }
            catch (Exception)
            {
                return new DashboardResponse<DateTime>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DashboardResponse<DateTime>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }
        public async Task<DashboardResponse<DateTime>> GetDashboardGameByTransaction(DateRequestModel request)
        {
            Dictionary<DateTime, decimal> result = new Dictionary<DateTime, decimal>();
            try
            {

                result = await _gameRepository.GetDashboardByTransaction(request.Date, request.StartDate, request.EndDate) ?? throw new Exception();

            }
            catch (Exception)
            {
                return new DashboardResponse<DateTime>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DashboardResponse<DateTime>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }

        public async Task<DashboardResponse<Month>> GetDashboardGameByMonth(MonthRequestModel request)
        {
            Dictionary<Month, decimal> result = new Dictionary<Month, decimal>();
            try
            {
                //request.StartMonth = request.Month ?? DateTime.Now.Month;
                //request.EndMonth = request.Month?? DateTime.Now.Month;

                //if (request.StartYear == null)
                //{
                //    request.StartYear = request.Year;
                //}

                //if (request.EndYear == null)
                //{
                //    request.EndYear = request.Year;
                //}
                //else 
                if(request.StartMonth > request.EndMonth)
                {
                    int month = request.StartMonth;
                    request.StartMonth = request.EndMonth;
                    request.EndMonth = month;
                }
                if (request.EndYear < request.StartYear)
                {
                    int? year = request.StartYear;
                    request.StartYear = request.EndYear;
                    request.EndYear = year;
                }

                var data = await _gameRepository.GetDashboardByMonth(request.Month, request.StartMonth, request.EndMonth, request.StartYear, request.EndYear) ?? throw new Exception();

                result = data.Where(x => Enum.IsDefined(typeof(Month), x.Key))
                             .ToDictionary(k => (Month)k.Key, k => k.Value);

            }
            catch (Exception)
            {
                return new DashboardResponse<Month>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DashboardResponse<Month>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }

        public async Task<DashboardResponse<int>> GetDashboardGameByYear(YearRequestModel request)
        {
            Dictionary<int, decimal> result = new Dictionary<int, decimal>();
            try
            {
                request.FromYear = request.Year ?? DateTime.Now.Year;
                request.ToYear = request.Year ?? DateTime.Now.Year;

                result = await _gameRepository.GetDashboardByMonth(request.Year, request.FromYear, request.ToYear) ?? throw new Exception();

            }
            catch (Exception)
            {
                return new DashboardResponse<int>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DashboardResponse<int>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }

        public async Task<DashboardResponse<Month>> GetDashboardMonetaryByMonth(MonthRequestModel request)
        {
            Dictionary<Month, decimal> result = new Dictionary<Month, decimal>();
            try
            {
                //request.StartMonth = request.Month ?? DateTime.Now.Month;
                //request.EndMonth = request.Month ?? DateTime.Now.Month;

                //if (request.StartYear == null)
                //{
                //    request.StartYear = request.Year;
                //}

                //if (request.EndYear == null)
                //{
                //    request.EndYear = request.Year;
                //}
                //else 
                if (request.StartMonth > request.EndMonth)
                {
                    int month = request.StartMonth;
                    request.StartMonth = request.EndMonth;
                    request.EndMonth = month;
                }
                if (request.EndYear < request.StartYear)
                {
                    int? year = request.StartYear;
                    request.StartYear = request.EndYear;
                    request.EndYear = year;
                }


                var data = await _monetaryRepository.GetDashboardByMonth(request.Month, request.StartMonth, request.EndMonth, request.StartYear, request.EndYear) ?? throw new Exception();

                result = data.Where(x => Enum.IsDefined(typeof(Month), x.Key))
                             .ToDictionary(k => (Month)k.Key, x => x.Value);

            }
            catch (Exception)
            {
                return new DashboardResponse<Month>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DashboardResponse<Month>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }

        public async Task<DashboardResponse<int>> GetDashboardMonetaryByYear(YearRequestModel request)
        {
            Dictionary<int, decimal> result = new Dictionary<int, decimal>();
            try
            {
                request.FromYear = request.Year ?? DateTime.Now.Year;
                request.ToYear = request.Year ?? DateTime.Now.Year;

                result = await _monetaryRepository.GetDashboardByMonth(request.Year, request.FromYear, request.ToYear) ?? throw new Exception();

            }
            catch (Exception)
            {
                return new DashboardResponse<int>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DashboardResponse<int>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }

        public async Task<DashboardResponse<DateTime>> GetDashboardMoneytarybyDate(DateRequestModel request)
        {
            Dictionary<DateTime, decimal> result = new Dictionary<DateTime, decimal>();
            try
            {
                //request.StartDate = request.StartDate ?? DateTime.Now;
                //request.EndDate = request.EndDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                result = await _monetaryRepository.GetDashboardByDate(request.Date, request.StartDate, request.EndDate) ?? throw new Exception();

            }
            catch (Exception)
            {
                return new DashboardResponse<DateTime>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DashboardResponse<DateTime>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }
        
        public async Task<DashboardResponse<DateTime>> GetDashboardByTransaction(DateRequestModel request)
        {
            Dictionary<DateTime, decimal> result = new Dictionary<DateTime, decimal>();
            try
            {

                result = await _monetaryRepository.GetDashboardByTransaction(request.Date, request.StartDate, request.EndDate) ?? throw new Exception();

            }
            catch (Exception)
            {
                return new DashboardResponse<DateTime>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DashboardResponse<DateTime>()
            {
                Message = Constraints.INFORMATION,
                Values = result
            };
        }
    }
}
