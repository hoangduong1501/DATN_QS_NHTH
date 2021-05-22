using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_DATN_QS.Models.DB_Entities;
using UI_DATN_QS.Models.DB_Models;
using UI_DATN_QS.Models.Sessions;

namespace UI_DATN_QS.Areas.GiangVien.Controllers
{
    public class DangXuatGVController : Controller
    {
        [HttpGet]
        public ActionResult LOGOUT_GiangVien()
        {
            SessionHelper.Remove_SessionHV();
            return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });

        }

        [HttpGet]
        public ActionResult CHANGE_MatKhau()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionHV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CHANGE_MatKhau(MatKhau_Model pMatKhau)
        {
            if (SessionHelper.Get_SessionHV() == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });

            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        int a = SessionHelper.Get_SessionHV().ID_TaiKhoan;
                        TAI_KHOAN TaiKhoan = entities.TAI_KHOAN.Where(p => p.ID_TaiKhoan == a).FirstOrDefault();

                        if (TaiKhoan.PASS_TaiKhoan.Equals(Models.HashCodes.Hash_MD5.GetHash_MD5(pMatKhau.OLD_Password)))
                        {
                            TaiKhoan.PASS_TaiKhoan = Models.HashCodes.Hash_MD5.GetHash_MD5(pMatKhau.NEW_Password);

                            entities.SaveChanges();
                            return RedirectToAction("GET_DeThi", "DeThiGV", new { area = "GiangVien" });
                        }
                        else
                        {
                            ViewBag.Notifi = 1;
                        }
                    }
                }
                return View();
            }
            catch (Exception) { return View(); }
        }

    }
}