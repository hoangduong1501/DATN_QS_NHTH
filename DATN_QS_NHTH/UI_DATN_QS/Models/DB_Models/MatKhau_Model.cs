using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_DATN_QS.Models.DB_Models
{
    public class MatKhau_Model
    {
        public int ID_TaiKhoan { get; set; }
        public string OLD_Password { get; set; }
        public string NEW_Password { get; set; }
    }
}