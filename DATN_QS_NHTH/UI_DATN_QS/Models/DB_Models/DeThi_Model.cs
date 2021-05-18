using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_DATN_QS.Models.DB_Models
{
    public class DeThi_Model
    {
        public int ID_DeThi { get; set; }
        public string MA_DeThi { get; set; }
        public string TEN_DeThi { get; set; }
        public string PASS_DeThi { get; set; }
        public Nullable<int> TGIAN_DeThi { get; set; }
        public Nullable<int> IS_Locked { get; set; }
        public Nullable<System.DateTime> TIME_Create { get; set; }

        public int ID_MonHoc { get; set; }
        public string MA_MonHoc { get; set; }
        public string TEN_MonHoc { get; set; }

        public int ID_LBaiThi { get; set; }
        public string MA_LBaiThi { get; set; }
        public string TEN_LBaiThi { get; set; }

        public int NUM_DeThi { get; set; }
    }
}