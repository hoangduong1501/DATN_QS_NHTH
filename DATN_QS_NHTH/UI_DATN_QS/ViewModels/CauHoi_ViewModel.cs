using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_DATN_QS.Models.DB_Entities;

namespace UI_DATN_QS.ViewModels
{
    public class CauHoi_ViewModel
    {
        public List<MON_HOC> list_MonHoc { get; set; }
        public List<CAU_HOI> list_CauHoi { get; set; }
        public int ID_MonHoc { get; set; }
        public CAU_HOI object_CauHoi { get; set; }
    }
}