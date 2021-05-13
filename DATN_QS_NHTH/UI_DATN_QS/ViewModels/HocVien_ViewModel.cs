using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_DATN_QS.Models.DB_Entities;
using UI_DATN_QS.Models.DB_Models;

namespace UI_DATN_QS.ViewModels
{
    public class HocVien_ViewModel
    {
        public List<LOP_HOC> list_LopHoc { get; set; }
        public HocVien_Model object_HocVien { get; set; }
    }
}