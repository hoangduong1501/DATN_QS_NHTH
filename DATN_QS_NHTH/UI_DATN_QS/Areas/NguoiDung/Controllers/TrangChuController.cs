using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_DATN_QS.Models.DB_Models;
using UI_DATN_QS.Models.Sessions;

namespace UI_DATN_QS.Areas.NguoiDung.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: NguoiDung/TrangChu
        public ActionResult GET_TrangChu()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if(user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;
            return View();
        }
    }
}