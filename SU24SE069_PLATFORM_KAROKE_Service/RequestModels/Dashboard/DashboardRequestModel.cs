using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_Service.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Dashboard
{
    public class MonthRequestModel
    {
        [RegularExpression(Constraints.VALIDATE_ONE_TO_TWELVE, ErrorMessage = Constraints.ONE_TO_TWELVE)]
        public int? Month { get; set; }
        [RegularExpression(Constraints.VALIDATE_ONE_TO_TWELVE, ErrorMessage = Constraints.ONE_TO_TWELVE)]
        public int StartMonth { get; set; } = (int)SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons.Month.January;

        [RegularExpression(Constraints.VALIDATE_ONE_TO_TWELVE, ErrorMessage = Constraints.ONE_TO_TWELVE)]
        [GreaterThanOrEqualByInt(nameof(StartMonth), ErrorMessage = Constraints.START_MONTH_END_MONTH)]
        public int EndMonth { get; set; } = (int)SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons.Month.December;
        [InRangeOneHundred(ErrorMessage = Constraints.OVER_100_YEARS)]
        public int Year { get; set; } = DateTime.Now.Year;
        private int? _startYear;
        private int? _endYear;
        public int? StartYear
        {
            get => _startYear ?? Year;
            set => _startYear = value;
        }
        public int? EndYear
        {
            get => _endYear ?? Year;
            set => _endYear = value;
        }
    }

    public class DateRequestModel
    {
        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
        [GreaterThanOrEqualDate(nameof(StartDate), ErrorMessage = Constraints.START_DATE_END_DATE)]
        public DateTime EndDate { get; set; } = new DateTime(DateTime.Now.Year, 12, 1, 23, 59, 59);
        public DateTime? Date { get; set; }
    }

    public class YearRequestModel
    {
        [InRangeOneHundred(ErrorMessage = Constraints.OVER_100_YEARS)]
        public int? Year { get; set; }
        [InRangeOneHundred(ErrorMessage = Constraints.OVER_100_YEARS)]
        public int FromYear { get; set; } = DateTime.Now.Year - 50;
        [GreaterThanOrEqualByInt(nameof(FromYear), ErrorMessage = Constraints.START_YEAR_END_YEAR)]
        [InRangeOneHundred(ErrorMessage = Constraints.OVER_100_YEARS)]
        public int ToYear { get; set; } = DateTime.Now.Year + 50;

    }
}
