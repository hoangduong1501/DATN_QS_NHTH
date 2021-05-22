using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_DATN_QS.Models.Sessions;

namespace UI_DATN_QS.Controllers
{
    public class DangXuatController : Controller
    {
        [HttpGet]
        public ActionResult LOGOUT_HocVien()
        {
            SessionHelper.Remove_SessionHV();
            return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });

        }

    }
}