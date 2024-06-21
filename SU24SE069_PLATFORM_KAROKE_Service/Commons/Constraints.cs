using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons
{
    public class Constraints
    {
        #region Generic
        public const string NOT_FOUND = "Thông tin không tồn tại!";
        public const string LOAD_FAILED = "Tải thông tin thất bại!";
        public const string CREATE_SUCCESS = "Tạo thông tin thành công!";
        public const string CREATE_FAILED = "Tạo thông tin thất bại!";
        public const string DELETE_SUCCESS = "Xóa thông tin thành công!";
        public const string DELETE_FAILED = "Xóa thông tin thất bại!";
        public const string UPDATE_SUCCESS = "Cập nhật thông tin thành công!";
        public const string UPDATE_FAILED = "Cập nhật thông tin thất bại!";
        public const string INFORMATION = "Thông tin: ";
        public const string INFORMATION_EXISTED = "Thông tin đã tồn tại!";

        #endregion

        #region Login
        public const string EMAIL_PASSWORD_INVALID = "email hoặc mật khẩu không đúng!";
        #endregion

        #region Page
        public const int DefaultPaging = 50;
        public const int LimitPaging = 500;
        public const int DefaultPage = 1;

        #endregion
        #region Authenticate
        public const string INVALID_VERIFICATION_CODE = "mã xác thực không hợp lệ!";
        #endregion

        #region Validate data
        public const string EMPTY_INPUT_INFORMATION = "Vui lòng nhập thông tin!";
        public const string STAR_INVALID = "Vui lòng nhập số tiền hợp lệ!";
        public const string INFORMATION_INVALID = "Vui lòng nhập thông tin hợp lệ!";
        public const string VALIDATE_NEGATIVE = @"^[0-9]+$";
        public const string VALIDATE_AMOUNT = @"^\d+(\.\d+)?$";
        public const string VALIDATE_RANGE_0_TO_100 = "Vui lòng nhập điểm từ 0 đến 100";
        public const string VALIDATE_ENDDATE = "Vui lòng nhập thời gian kết thúc lớn hơn thời gian bắt đầu!";
        #endregion
    }
}
