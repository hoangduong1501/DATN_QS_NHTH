using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_DATN_QS.Models.DB_Models
{
    public class NguoiDung_Model
    {
        public int ID_NguoiDung { get; set; }
        public string MA_NguoiDung { get; set; }
        public string TEN_NguoiDung { get; set; }
        public Nullable<System.DateTime> NSINH_NguoiDung { get; set; }
        public string MAIL_NguoiDung { get; set; }
        public byte[] ANH_NguoiDung { get; set; }

        public int ID_TaiKhoan { get; set; }
        public string USER_TaiKhoan { get; set; }
        public string PASS_TaiKhoan { get; set; }
        public string NOTE_TaiKhoan { get; set; }
        public Nullable<int> IS_Locked { get; set; }
    }
}