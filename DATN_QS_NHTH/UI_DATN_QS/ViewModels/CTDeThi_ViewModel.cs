using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_DATN_QS.Models.DB_Entities;

namespace UI_DATN_QS.ViewModels
{
    public class CTDeThi_ViewModel
    {
        public int ID_DeThi { get; set; }
        public int TGIAN_DeThi { get; set; }
        public List<CAU_HOI> list_CauHoi { get; set; }
    }
}