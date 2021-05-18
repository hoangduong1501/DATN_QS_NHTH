using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_DATN_QS.Models.DB_Entities;
using UI_DATN_QS.Models.DB_Models;

namespace UI_DATN_QS.ViewModels
{
    public class DeThi_ViewModel
    {
        public List<MON_HOC> list_MonHoc { get; set; }
        public List<DeThi_Model> list_DeThi { get; set; }
        public int ID_MonHoc { get; set; }
        public DeThi_Model object_DeThi { get; set; }
        public List<LOAI_BAI_THI> list_LBaiThi { get; set; }
        public List<CauHoi_Model> list_CauHoi { get; set; }
    }
}