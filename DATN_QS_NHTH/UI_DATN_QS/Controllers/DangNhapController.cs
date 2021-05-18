using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_DATN_QS.Models.DB_Entities;
using UI_DATN_QS.Models.DB_Models;
using UI_DATN_QS.Models.Sessions;

namespace UI_DATN_QS.Controllers
{
    public class DangNhapController : Controller
    {

        [HttpGet]
        public ActionResult Dang_Nhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dang_Nhap(TAI_KHOAN pTaiKhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        TAI_KHOAN TaiKhoan = entities.TAI_KHOAN.Where(p => p.USER_TaiKhoan == pTaiKhoan.USER_TaiKhoan).FirstOrDefault();
                        if (TaiKhoan != null && TaiKhoan.PASS_TaiKhoan.Equals(Models.HashCodes.Hash_MD5.GetHash_MD5(pTaiKhoan.PASS_TaiKhoan)))
                        {
                            if (SessionHelper.Get_Session() != null) SessionHelper.Remove_Session();

                            if (TaiKhoan.LOAI_TaiKhoan == 2)
                            {
                                SessionHelper.Set_Session(new UserSession_Model
                                {
                                    ID_TaiKhoan = TaiKhoan.ID_TaiKhoan,
                                    USER_TaiKhoan = TaiKhoan.USER_TaiKhoan,
                                    TEN_NguoiDung = entities.NGUOI_DUNG.Where(p => p.ID_TaiKhoan == TaiKhoan.ID_TaiKhoan).FirstOrDefault().TEN_NguoiDung
                                });

                                return RedirectToAction("GET_TrangChu", "TrangChu", new { area = "NguoiDung" });
                            }
                            else return View();
                        }
                        else
                        {
                            ViewBag.StatusAccount = 1;
                            return View();
                        }
                    }
                }

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}