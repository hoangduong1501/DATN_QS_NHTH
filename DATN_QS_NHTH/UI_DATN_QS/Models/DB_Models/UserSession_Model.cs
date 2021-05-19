using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_DATN_QS.Models.DB_Models
{
    public class UserSession_Model
    {
        public int ID_TaiKhoan { get; set; }
        public string USER_TaiKhoan { get; set; }
        public string TEN_NguoiDung { get; set; }
        public byte[] ANH_NguoiDung { get; set; }
    }
}