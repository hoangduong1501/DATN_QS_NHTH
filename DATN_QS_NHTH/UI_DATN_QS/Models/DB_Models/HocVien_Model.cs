using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_DATN_QS.Models.DB_Models
{
    public class HocVien_Model
    {
        public int ID_HocVien { get; set; }
        public string MA_HocVien { get; set; }
        public string TEN_HocVien { get; set; }
        public Nullable<System.DateTime> NSINH_HocVien { get; set; }
        public Nullable<int> GTINH_HocVien { get; set; }
        public byte[] ANH_HocVien { get; set; }

        public int ID_TaiKhoan { get; set; }
        public string USER_TaiKhoan { get; set; }
        public string PASS_TaiKhoan { get; set; }
        public string NOTE_TaiKhoan { get; set; }
        public Nullable<int> IS_Locked { get; set; }

        public int ID_LopHoc { get; set; }
        public int MA_LopHoc { get; set; }
        public int TEN_LopHoc { get; set; }

        public int ID_CTLopHoc { get; set; }
    }
}