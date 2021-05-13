using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_DATN_QS.Models.DB_Entities;
using UI_DATN_QS.Models.DB_Models;

namespace UI_DATN_QS.ViewModels
{
    public class LopHoc_ViewModel
    {
        public List<NGANH_HOC> list_NganhHoc { get; set; }
        public List<KHOA_HOC> list_KhoaHoc { get; set; }
        public List<NIEN_KHOA> list_NienKhoa { get; set; }
        public LopHoc_Model object_LopHoc { get; set; }
    }
}