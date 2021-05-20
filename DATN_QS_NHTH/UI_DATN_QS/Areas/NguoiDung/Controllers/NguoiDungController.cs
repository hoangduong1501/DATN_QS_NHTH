using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_DATN_QS.Models.DB_Entities;
using UI_DATN_QS.Models.DB_Models;
using UI_DATN_QS.Models.Sessions;

namespace UI_DATN_QS.Areas.NguoiDung.Controllers
{
    public class NguoiDungController : Controller
    {
        /*NguoiDung*/
        #region
        [HttpGet]
        public ActionResult GET_NguoiDung()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    var obj_NguoiDung = entities.NGUOI_DUNG.ToList().Join(entities.TAI_KHOAN.Where(p => p.IS_Deleted == 0).ToList(),
                        nd => nd.ID_TaiKhoan,
                        tk => tk.ID_TaiKhoan,
                        (nd, tk) => new { nd, tk }).Select(m => new NguoiDung_Model()
                        {
                            ID_NguoiDung = m.nd.ID_NguoiDung,
                            MA_NguoiDung = m.nd.MA_NguoiDung,
                            TEN_NguoiDung = m.nd.TEN_NguoiDung,
                            NSINH_NguoiDung = m.nd.NSINH_NguoiDung,
                            MAIL_NguoiDung = m.nd.MAIL_NguoiDung,
                            ANH_NguoiDung = m.nd.ANH_NguoiDung,
                            ID_TaiKhoan = m.tk.ID_TaiKhoan,
                            USER_TaiKhoan = m.tk.USER_TaiKhoan,
                            PASS_TaiKhoan = m.tk.PASS_TaiKhoan,
                            NOTE_TaiKhoan = m.tk.NOTE_TaiKhoan,
                            IS_Locked = m.tk.IS_Locked
                        });


                    return View(obj_NguoiDung);
                }

            }
            catch (Exception)
            {

                return View();
            }
        }

        [HttpGet]
        public ActionResult INSERT_NguoiDung()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
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
        public ActionResult INSERT_NguoiDung(NguoiDung_Model pNguoiDung, HttpPostedFileBase fileLoad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    byte[] img_Upload = null;
                    if (fileLoad != null)
                    {
                        img_Upload = new byte[fileLoad.InputStream.Length];
                        fileLoad.InputStream.Read(img_Upload, 0, img_Upload.Length);

                    }
                    else
                    {
                        img_Upload = System.IO.File.ReadAllBytes(Server.MapPath("~/Images/avatar-none.jpg"));
                    }

                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        int id_NguoiDung = 1, id_TaiKhoan = 1;

                        if (entities.NGUOI_DUNG.Count() > 0) id_NguoiDung = entities.NGUOI_DUNG.Max(p => p.ID_NguoiDung) + 1;
                        if (entities.TAI_KHOAN.Count() > 0) id_TaiKhoan = entities.TAI_KHOAN.Max(p => p.ID_TaiKhoan) + 1;

                        entities.NGUOI_DUNG.Add(new NGUOI_DUNG()
                        {
                            ID_NguoiDung = id_NguoiDung,
                            MA_NguoiDung = pNguoiDung.MA_NguoiDung,
                            TEN_NguoiDung = pNguoiDung.TEN_NguoiDung,
                            ANH_NguoiDung = img_Upload,
                            MAIL_NguoiDung = pNguoiDung.MAIL_NguoiDung,
                            NSINH_NguoiDung = pNguoiDung.NSINH_NguoiDung,
                            ID_TaiKhoan = id_TaiKhoan
                        });

                        entities.TAI_KHOAN.Add(new TAI_KHOAN()
                        {
                            ID_TaiKhoan = id_TaiKhoan,
                            USER_TaiKhoan = pNguoiDung.USER_TaiKhoan,
                            IS_Deleted = 0,
                            IS_Locked = 0,
                            NOTE_TaiKhoan = "None",
                            PASS_TaiKhoan = UI_DATN_QS.Models.HashCodes.Hash_MD5.GetHash_MD5("123456"),
                            TIME_Create = DateTime.Today,
                            TIME_Update = DateTime.Today,
                            LOAI_TaiKhoan = 2
                        });

                        entities.SaveChanges();

                        return RedirectToAction("GET_NguoiDung", "NguoiDung", new { area = "NguoiDung" });
                    }
                }

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult UPDATE_NguoiDung(int pID_NguoiDung)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    var obj_NguoiDung = entities.NGUOI_DUNG.Where(p => p.ID_NguoiDung == pID_NguoiDung).ToList().Join(entities.TAI_KHOAN.Where(p => p.IS_Deleted == 0).ToList(),
                       nd => nd.ID_TaiKhoan,
                       tk => tk.ID_TaiKhoan,
                       (nd, tk) => new { nd, tk }).Select(m => new NguoiDung_Model()
                       {
                           ID_NguoiDung = m.nd.ID_NguoiDung,
                           MA_NguoiDung = m.nd.MA_NguoiDung,
                           TEN_NguoiDung = m.nd.TEN_NguoiDung,
                           NSINH_NguoiDung = m.nd.NSINH_NguoiDung,
                           MAIL_NguoiDung = m.nd.MAIL_NguoiDung,
                           ANH_NguoiDung = m.nd.ANH_NguoiDung,
                           ID_TaiKhoan = m.tk.ID_TaiKhoan,
                           USER_TaiKhoan = m.tk.USER_TaiKhoan,
                           PASS_TaiKhoan = m.tk.PASS_TaiKhoan,
                           NOTE_TaiKhoan = m.tk.NOTE_TaiKhoan,
                           IS_Locked = m.tk.IS_Locked
                       }).FirstOrDefault();

                    return View(obj_NguoiDung);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UPDATE_NguoiDung(NguoiDung_Model pNguoiDung, HttpPostedFileBase fileLoad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    byte[] img_Upload = null;
                    if (fileLoad != null)
                    {
                        img_Upload = new byte[fileLoad.InputStream.Length];
                        fileLoad.InputStream.Read(img_Upload, 0, img_Upload.Length);
                    }

                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        NGUOI_DUNG NguoiDung = entities.NGUOI_DUNG.Where(p => p.ID_NguoiDung == pNguoiDung.ID_NguoiDung).FirstOrDefault();
                        TAI_KHOAN TaiKhoan = entities.TAI_KHOAN.Where(p => p.ID_TaiKhoan == pNguoiDung.ID_TaiKhoan).FirstOrDefault();

                        NguoiDung.MA_NguoiDung = pNguoiDung.MA_NguoiDung;
                        NguoiDung.TEN_NguoiDung = pNguoiDung.TEN_NguoiDung;
                        NguoiDung.NSINH_NguoiDung = pNguoiDung.NSINH_NguoiDung;
                        NguoiDung.MAIL_NguoiDung = pNguoiDung.MAIL_NguoiDung;

                        if (fileLoad != null) NguoiDung.ANH_NguoiDung = img_Upload;

                        TaiKhoan.USER_TaiKhoan = pNguoiDung.USER_TaiKhoan;
                        TaiKhoan.TIME_Update = DateTime.Today;

                        entities.SaveChanges();

                        return RedirectToAction("GET_NguoiDung", "NguoiDung", new { area = "NguoiDung" });
                    }
                }

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult IsLock_NguoiDung(int pID_TaiKhoan)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    TAI_KHOAN TaiKhoan = entities.TAI_KHOAN.Where(p => p.ID_TaiKhoan == pID_TaiKhoan).FirstOrDefault();

                    if (TaiKhoan.IS_Locked == 1) TaiKhoan.IS_Locked = 0;
                    else TaiKhoan.IS_Locked = 1;

                    entities.SaveChanges();
                }

                return Redirect(Request.UrlReferrer.ToString());
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Reset_NguoiDung(int pID_TaiKhoan)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    TAI_KHOAN TaiKhoan = entities.TAI_KHOAN.Where(p => p.ID_TaiKhoan == pID_TaiKhoan).FirstOrDefault();
                    TaiKhoan.PASS_TaiKhoan = UI_DATN_QS.Models.HashCodes.Hash_MD5.GetHash_MD5("123456");

                    entities.SaveChanges();
                }

                return RedirectToAction("GET_NguoiDung", "NguoiDung", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DELETE_NguoiDung(FormCollection formCollection)
        {
            try
            {
                if (formCollection.Count > 1)
                {
                    string[] ids = formCollection["ID_TaiKhoan"].Split(new char[] { ',' });

                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        foreach (string item in ids)
                        {
                            int id = int.Parse(item);
                            entities.TAI_KHOAN.Where(p => p.ID_TaiKhoan == id).FirstOrDefault().IS_Deleted = 1;
                            entities.SaveChanges();

                        }
                    }
                }

                return RedirectToAction("GET_NguoiDung", "NguoiDung", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DELETE_NguoiDung(int pID_TaiKhoan)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    entities.TAI_KHOAN.Where(p => p.ID_TaiKhoan == pID_TaiKhoan).FirstOrDefault().IS_Deleted = 1;
                    entities.SaveChanges();
                }

                return RedirectToAction("GET_NguoiDung", "NguoiDung", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult CHANGE_MatKhau()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
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
            if (SessionHelper.Get_SessionND() == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });

            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        int a = SessionHelper.Get_SessionND().ID_TaiKhoan;
                        TAI_KHOAN TaiKhoan = entities.TAI_KHOAN.Where(p => p.ID_TaiKhoan == a).FirstOrDefault();

                        if (TaiKhoan.PASS_TaiKhoan.Equals(Models.HashCodes.Hash_MD5.GetHash_MD5(pMatKhau.OLD_Password)))
                        {
                            TaiKhoan.PASS_TaiKhoan = Models.HashCodes.Hash_MD5.GetHash_MD5(pMatKhau.NEW_Password);

                            entities.SaveChanges();
                            return RedirectToAction("GET_TrangChu", "TrangChu", new { area = "NguoiDung" });
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

        [HttpGet]
        public ActionResult LOGOUT_NguoiDung()
        {
            SessionHelper.Remove_SessionND();
            return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
        }
        #endregion
    }
}