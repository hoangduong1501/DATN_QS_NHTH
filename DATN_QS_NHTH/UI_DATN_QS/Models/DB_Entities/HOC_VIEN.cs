//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UI_DATN_QS.Models.DB_Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOC_VIEN
    {
        public int ID_HocVien { get; set; }
        public string MA_HocVien { get; set; }
        public string TEN_HocVien { get; set; }
        public Nullable<System.DateTime> NSINH_HocVien { get; set; }
        public Nullable<int> GTINH_HocVien { get; set; }
        public byte[] ANH_HocVien { get; set; }
        public Nullable<int> ID_TaiKhoan { get; set; }
    }
}
