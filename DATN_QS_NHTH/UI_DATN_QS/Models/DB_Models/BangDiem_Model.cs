using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_DATN_QS.Models.DB_Models
{
    public class BangDiem_Model
    {
        public int ID_BangDiem { get; set; }
        public int ID_HocVien { get; set; }
        public int ID_DeThi { get; set; }
        public Nullable<double> DIEM_BangDiem { get; set; }
        public Nullable<int> DUNG_BangDiem { get; set; }
        public int IS_Deleted { get; set; }
        public Nullable<System.DateTime> TIME_Create { get; set; }
        public Nullable<System.DateTime> TIME_Update { get; set; }

        public string MA_DeThi { get; set; }
        public string TEN_LBaiThi { get; set; }
        public string TEN_MonHoc { get; set; }
    }
}