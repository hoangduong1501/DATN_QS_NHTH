using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_DATN_QS.ViewModels
{
    public class CauHoiList_ViewModel
    {
        public int ID_CauHoi { get; set; }
        public string NDUNG_CauHoi { get; set; }
        public byte[] ANH_CauHoi { get; set; }
        public string LCHON_1 { get; set; }
        public string LCHON_2 { get; set; }
        public string LCHON_3 { get; set; }
        public string LCHON_4 { get; set; }
        public string LCHON_Dung { get; set; }
        public Nullable<int> ID_Chuong { get; set; }
        public Nullable<int> ID_MonHoc { get; set; }
        public Nullable<int> IS_Deleted { get; set; }
        public Nullable<System.DateTime> TIME_Create { get; set; }
        public Nullable<System.DateTime> TIME_Update { get; set; }
        public string TEN_MonHoc { get; set; }
    }
}