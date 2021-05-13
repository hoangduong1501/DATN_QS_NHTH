using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_DATN_QS.Models.DB_Models
{
    public class LopHoc_Model
    {
        public int ID_LopHoc { get; set; }
        public string MA_LopHoc { get; set; }
        public string TEN_LopHoc { get; set; }

        public int ID_NienKhoa { get; set; }
        public string MA_NienKhoa { get; set; }
        public string TEN_NienKhoa { get; set; }

        public int ID_NganhHoc { get; set; }
        public string MA_NganhHoc { get; set; }
        public string TEN_NganhHoc { get; set; }

        public int ID_KhoaHoc { get; set; }
        public string MA_KhoaHoc { get; set; }
        public string TEN_KhoaHoc { get; set; }

        public int NUM_LopHoc { get; set; }
    }
}