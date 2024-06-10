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
    }
}
