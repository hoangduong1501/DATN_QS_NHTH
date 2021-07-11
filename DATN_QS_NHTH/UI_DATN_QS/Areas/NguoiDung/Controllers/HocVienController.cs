using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_DATN_QS.Models.DB_Entities;
using UI_DATN_QS.Models.DB_Models;
using UI_DATN_QS.Models.Sessions;
using UI_DATN_QS.ViewModels;

namespace UI_DATN_QS.Areas.NguoiDung.Controllers
{
    public class HocVienController : Controller
    {
        /*HocVien*/
        #region
        [HttpGet]
        public ActionResult GET_HocVien()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    var model_HocVien = entities.HOC_VIEN.ToList().
                            Join(entities.TAI_KHOAN.Where(p => p.IS_Deleted == 0).ToList(), hv => hv.ID_TaiKhoan, tk => tk.ID_TaiKhoan, (hv, tk) => new { hv, tk }).
                            Select(m => new HocVien_Model()
                            {
                                ID_HocVien = m.hv.ID_HocVien,
                                MA_HocVien = m.hv.MA_HocVien,
                                TEN_HocVien = m.hv.TEN_HocVien,
                                NSINH_HocVien = m.hv.NSINH_HocVien,
                                GTINH_HocVien = m.hv.GTINH_HocVien,
                                ANH_HocVien = m.hv.ANH_HocVien,
                                ID_TaiKhoan = m.tk.ID_TaiKhoan,
                                USER_TaiKhoan = m.tk.USER_TaiKhoan,
                                PASS_TaiKhoan = m.tk.PASS_TaiKhoan,
                                NOTE_TaiKhoan = m.tk.NOTE_TaiKhoan,
                                IS_Locked = m.tk.IS_Locked
                            });

                    return View(model_HocVien);
                }

            }
            catch (Exception)
            {

                return View();
            }
        }

        [HttpGet]
        public ActionResult INSERT_HocVien()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    HocVien_ViewModel HocVien = new HocVien_ViewModel()
                    {
                        list_LopHoc = entities.LOP_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                        object_HocVien = null
                    };

                    return View(HocVien);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult INSERT_HocVien(HocVien_ViewModel pHocVien, HttpPostedFileBase fileLoad)
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
                        int id_HocVien = 1, id_TaiKhoan = 1, id_CTLopHoc = 1;

                        if (entities.HOC_VIEN.Count() > 0) id_HocVien = entities.HOC_VIEN.Max(p => p.ID_HocVien) + 1;
                        if (entities.TAI_KHOAN.Count() > 0) id_TaiKhoan = entities.TAI_KHOAN.Max(p => p.ID_TaiKhoan) + 1;
                        if (entities.CT_LOP_HOC.Count() > 0) id_CTLopHoc = entities.CT_LOP_HOC.Max(p => p.ID_CTLopHoc) + 1;

                        entities.HOC_VIEN.Add(new HOC_VIEN()
                        {
                            ID_HocVien = id_HocVien,
                            MA_HocVien = pHocVien.object_HocVien.MA_HocVien,
                            TEN_HocVien = pHocVien.object_HocVien.TEN_HocVien,
                            ANH_HocVien = img_Upload,
                            GTINH_HocVien = pHocVien.object_HocVien.GTINH_HocVien,
                            ID_TaiKhoan = id_TaiKhoan,
                            NSINH_HocVien = pHocVien.object_HocVien.NSINH_HocVien
                        });

                        entities.TAI_KHOAN.Add(new TAI_KHOAN()
                        {
                            ID_TaiKhoan = id_TaiKhoan,
                            USER_TaiKhoan = pHocVien.object_HocVien.USER_TaiKhoan,
                            IS_Deleted = 0,
                            IS_Locked = 0,
                            NOTE_TaiKhoan = "None",
                            PASS_TaiKhoan = UI_DATN_QS.Models.HashCodes.Hash_MD5.GetHash_MD5("12345"),
                            TIME_Create = DateTime.Today,
                            TIME_Update = DateTime.Today,
                            LOAI_TaiKhoan = 1
                        });

                        entities.CT_LOP_HOC.Add(new CT_LOP_HOC()
                        {
                            ID_CTLopHoc = id_CTLopHoc,
                            ID_HocVien = id_HocVien,
                            ID_LopHoc = pHocVien.object_HocVien.ID_LopHoc,
                            IS_Deleted = 0,
                            TIME_Create = DateTime.Today,
                            TIME_Update = DateTime.Today
                        });

                        entities.SaveChanges();

                        return RedirectToAction("GET_HocVien", "HocVien", new { area = "NguoiDung" });
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
        public ActionResult UPDATE_HocVien(int pID_HocVien)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    HocVien_ViewModel HocVien = new HocVien_ViewModel()
                    {
                        list_LopHoc = entities.LOP_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                        object_HocVien = entities.HOC_VIEN.Where(p => p.ID_HocVien == pID_HocVien).ToList().
                                                Join(entities.TAI_KHOAN.ToList(), hv => hv.ID_TaiKhoan, tk => tk.ID_TaiKhoan, (hv, tk) => new { hv, tk }).
                                                Join(entities.CT_LOP_HOC.ToList(), tb1 => tb1.hv.ID_HocVien, ct => ct.ID_HocVien, (tb1, ct) => new { tb1, ct }).
                                                Join(entities.LOP_HOC.ToList(), tb2 => tb2.ct.ID_LopHoc, lh => lh.ID_LopHoc, (tb2, lh) => new { tb2, lh }).
                                                Select(tb3 => new HocVien_Model()
                                                {
                                                    ID_HocVien = tb3.tb2.tb1.hv.ID_HocVien,
                                                    MA_HocVien = tb3.tb2.tb1.hv.MA_HocVien,
                                                    TEN_HocVien = tb3.tb2.tb1.hv.TEN_HocVien,
                                                    NSINH_HocVien = tb3.tb2.tb1.hv.NSINH_HocVien,
                                                    ANH_HocVien = tb3.tb2.tb1.hv.ANH_HocVien,
                                                    GTINH_HocVien = tb3.tb2.tb1.hv.GTINH_HocVien,
                                                    ID_LopHoc = tb3.lh.ID_LopHoc,
                                                    ID_TaiKhoan = tb3.tb2.tb1.tk.ID_TaiKhoan,
                                                    USER_TaiKhoan = tb3.tb2.tb1.tk.USER_TaiKhoan,
                                                    IS_Locked = tb3.tb2.tb1.tk.IS_Locked,
                                                    ID_CTLopHoc = tb3.tb2.ct.ID_CTLopHoc
                                                }).FirstOrDefault()
                    };

                    return View(HocVien);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UPDATE_HocVien(HocVien_ViewModel pHocVien, HttpPostedFileBase fileLoad)
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
                        HOC_VIEN HocVien = entities.HOC_VIEN.Where(p => p.ID_HocVien == pHocVien.object_HocVien.ID_HocVien).FirstOrDefault();
                        TAI_KHOAN TaiKhoan = entities.TAI_KHOAN.Where(p => p.ID_TaiKhoan == pHocVien.object_HocVien.ID_TaiKhoan).FirstOrDefault();
                        CT_LOP_HOC CTLopHoc = entities.CT_LOP_HOC.Where(p => p.ID_CTLopHoc == pHocVien.object_HocVien.ID_CTLopHoc).FirstOrDefault();

                        HocVien.MA_HocVien = pHocVien.object_HocVien.MA_HocVien;
                        HocVien.TEN_HocVien = pHocVien.object_HocVien.TEN_HocVien;
                        HocVien.GTINH_HocVien = pHocVien.object_HocVien.GTINH_HocVien;
                        HocVien.NSINH_HocVien = pHocVien.object_HocVien.NSINH_HocVien;

                        if (fileLoad != null) HocVien.ANH_HocVien = img_Upload;

                        TaiKhoan.USER_TaiKhoan = pHocVien.object_HocVien.USER_TaiKhoan;
                        TaiKhoan.TIME_Update = DateTime.Today;

                        CTLopHoc.ID_LopHoc = pHocVien.object_HocVien.ID_LopHoc;

                        entities.SaveChanges();

                        return RedirectToAction("GET_HocVien", "HocVien", new { area = "NguoiDung" });
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
        public ActionResult IsLock_HocVien(int pID_TaiKhoan)
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
        public ActionResult Reset_HocVien(int pID_TaiKhoan)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    TAI_KHOAN TaiKhoan = entities.TAI_KHOAN.Where(p => p.ID_TaiKhoan == pID_TaiKhoan).FirstOrDefault();
                    TaiKhoan.PASS_TaiKhoan = UI_DATN_QS.Models.HashCodes.Hash_MD5.GetHash_MD5("123456");

                    entities.SaveChanges();
                }

                return Redirect(Request.UrlReferrer.ToString());
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DELETE_HocVien(FormCollection formCollection)
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

                return RedirectToAction("GET_HocVien", "HocVien", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DELETE_HocVien(int pID_TaiKhoan)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    entities.TAI_KHOAN.Where(p => p.ID_TaiKhoan == pID_TaiKhoan).FirstOrDefault().IS_Deleted = 1;
                    entities.SaveChanges();
                }

                return RedirectToAction("GET_HocVien", "HocVien", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
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
                            return RedirectToAction("GET_DeThi", "DeThi", new { area = "" });
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

        public ActionResult UPLOAD_HocVien()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
            {
                HocVien_ViewModel HocVien = new HocVien_ViewModel()
                {
                    list_LopHoc = entities.LOP_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                    list_HocVien = null,

                };

                return View(HocVien);
            }
        }

        [HttpPost]
        public ActionResult UPLOAD_HocVien(HocVien_ViewModel pHocVien, HttpPostedFileBase fileLoad)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    pHocVien.list_LopHoc = entities.LOP_HOC.Where(p => p.IS_Deleted == 0).ToList();

                ExcelPackage package = new ExcelPackage(fileLoad.InputStream);
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                pHocVien.list_HocVien = new List<HocVien_Upload>();

                for (int row = 5; row <= worksheet.Dimension.End.Row; row++)
                {
                    pHocVien.list_HocVien.Add(new HocVien_Upload()
                    {
                        MA_HocVien = worksheet.Cells[row, 2].Value.ToString(),
                        TEN_HocVien = worksheet.Cells[row, 3].Value.ToString(),
                        NSINH_HocVien = worksheet.Cells[row, 4].Value.ToString(),
                        GTINH_HocVien = (worksheet.Cells[row, 5].Value.ToString() == "Nam" ? 1 : 0)

                    });
                }

                return View(pHocVien);
            }
            catch (Exception)
            {
                return View(pHocVien);
            }
        }

        [HttpPost]
        public JsonResult Save_DanhSach(HocVien_ViewModel pHocVien)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    foreach(HocVien_Upload item in pHocVien.list_HocVien)
                    {
                        int id_HocVien = 1, id_TaiKhoan = 1, id_CTLopHoc = 1;

                        if (entities.HOC_VIEN.Count() > 0) id_HocVien = entities.HOC_VIEN.Max(p => p.ID_HocVien) + 1;
                        if (entities.TAI_KHOAN.Count() > 0) id_TaiKhoan = entities.TAI_KHOAN.Max(p => p.ID_TaiKhoan) + 1;
                        if (entities.CT_LOP_HOC.Count() > 0) id_CTLopHoc = entities.CT_LOP_HOC.Max(p => p.ID_CTLopHoc) + 1;

                        entities.HOC_VIEN.Add(new HOC_VIEN()
                        {
                            ID_HocVien = id_HocVien,
                            MA_HocVien = item.MA_HocVien,
                            TEN_HocVien = item.TEN_HocVien,
                            ANH_HocVien = null,
                            GTINH_HocVien = item.GTINH_HocVien,
                            ID_TaiKhoan = id_TaiKhoan,
                            NSINH_HocVien = DateTime.Parse(item.NSINH_HocVien.ToString())
                        });

                        entities.TAI_KHOAN.Add(new TAI_KHOAN()
                        {
                            ID_TaiKhoan = id_TaiKhoan,
                            USER_TaiKhoan = item.MA_HocVien,
                            IS_Deleted = 0,
                            IS_Locked = 0,
                            NOTE_TaiKhoan = "None",
                            PASS_TaiKhoan = UI_DATN_QS.Models.HashCodes.Hash_MD5.GetHash_MD5("12345"),
                            TIME_Create = DateTime.Today,
                            TIME_Update = DateTime.Today,
                            LOAI_TaiKhoan = 1
                        });

                        entities.CT_LOP_HOC.Add(new CT_LOP_HOC()
                        {
                            ID_CTLopHoc = id_CTLopHoc,
                            ID_HocVien = id_HocVien,
                            ID_LopHoc = pHocVien.ID_LopHoc,
                            IS_Deleted = 0,
                            TIME_Create = DateTime.Today,
                            TIME_Update = DateTime.Today
                        });

                        entities.SaveChanges();
                    }
                    
                }
                return Json("Thêm học viên thành công.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("Thêm học viên không thành công.", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult DownloadFile()
        {
            string fullName = Server.MapPath("~/Templates/DANH_SACH_HOC_VIEN.xlsx");

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "DANH_SACH_HOC_VIEN.xlsx");
        }

        private byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        #endregion

        /*NganhHoc*/
        #region
        [HttpGet]
        public ActionResult GET_NganhHoc()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    List<NGANH_HOC> lst_NganhHoc = entities.NGANH_HOC.Where(p => p.IS_Deleted == 0).ToList();

                    return View(lst_NganhHoc);
                }
            }
            catch (Exception)
            {

                return View();
            }
        }

        [HttpGet]
        public ActionResult INSERT_NganhHoc()
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
        public ActionResult INSERT_NganhHoc(NGANH_HOC pNganhHoc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        if (entities.NGANH_HOC.Count() == 0) pNganhHoc.ID_NganhHoc = 1;
                        else pNganhHoc.ID_NganhHoc = entities.NGANH_HOC.Max(p => p.ID_NganhHoc) + 1;

                        pNganhHoc.IS_Deleted = 0;
                        pNganhHoc.TIME_Create = pNganhHoc.TIME_Update = DateTime.Today;

                        entities.NGANH_HOC.Add(pNganhHoc);
                        entities.SaveChanges();

                        return RedirectToAction("GET_NganhHoc", "HocVien", new { area = "NguoiDung" });
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
        public ActionResult UPDATE_NganhHoc(int pID_NganhHoc)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    return View(entities.NGANH_HOC.Where(p => p.ID_NganhHoc == pID_NganhHoc).FirstOrDefault());
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UPDATE_NganhHoc(NGANH_HOC pNganhHoc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        NGANH_HOC NganhHoc = entities.NGANH_HOC.Where(p => p.ID_NganhHoc == pNganhHoc.ID_NganhHoc).FirstOrDefault();
                        NganhHoc.MA_NganhHoc = pNganhHoc.MA_NganhHoc;
                        NganhHoc.TEN_NganhHoc = pNganhHoc.TEN_NganhHoc;
                        NganhHoc.NOTE_NganhHoc = pNganhHoc.NOTE_NganhHoc;
                        NganhHoc.TIME_Update = DateTime.Today;

                        entities.SaveChanges();

                        return RedirectToAction("GET_NganhHoc", "HocVien", new { area = "NguoiDung" });
                    }
                }

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DELETE_NganhHoc(FormCollection formCollection)
        {
            try
            {
                if (formCollection.Count > 1)
                {
                    string[] ids = formCollection["ID_NganhHoc"].Split(new char[] { ',' });

                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        foreach (string item in ids)
                        {
                            int id = int.Parse(item);
                            entities.NGANH_HOC.Where(p => p.ID_NganhHoc == id).FirstOrDefault().IS_Deleted = 1;
                            entities.SaveChanges();

                        }
                    }
                }

                return RedirectToAction("GET_NganhHoc", "HocVien", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DELETE_NganhHoc(int pID_NganhHoc)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    entities.NGANH_HOC.Where(p => p.ID_NganhHoc == pID_NganhHoc).FirstOrDefault().IS_Deleted = 1;
                    entities.SaveChanges();
                }

                return RedirectToAction("GET_NganhHoc", "HocVien", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion

        /*LopHoc*/
        #region
        [HttpGet]
        public ActionResult GET_LopHoc()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    var obj_LopHoc = entities.LOP_HOC.Where(p => p.IS_Deleted == 0).ToList().
                       Join(entities.NGANH_HOC.ToList(), lop => lop.ID_NganhHoc, nganh => nganh.ID_NganhHoc, (lop, nganh) => new { lop, nganh }).
                       Join(entities.NIEN_KHOA.ToList(), tb1 => tb1.lop.ID_NienKhoa, nam => nam.ID_NienKhoa, (tb1, nam) => new { tb1, nam }).
                       Join(entities.KHOA_HOC.ToList(), tb2 => tb2.tb1.lop.ID_KhoaHoc, khoa => khoa.ID_KhoaHoc, (tb2, khoa) => new { tb2, khoa }).
                       Join(entities.CT_LOP_HOC.ToList().GroupBy(p => p.ID_LopHoc).Select(lg => new { ID_LopHoc = lg.Key, NUM_LopHoc = lg.Count() }).ToList(),
                                tb3 => tb3.tb2.tb1.lop.ID_LopHoc, p => p.ID_LopHoc, (tb3, p) => new { tb3, p }).
                       Select(tb4 => new LopHoc_Model()
                       {
                           ID_LopHoc = tb4.tb3.tb2.tb1.lop.ID_LopHoc,
                           MA_LopHoc = tb4.tb3.tb2.tb1.lop.MA_LopHoc,
                           TEN_LopHoc = tb4.tb3.tb2.tb1.lop.TEN_LopHoc,
                           ID_NienKhoa = tb4.tb3.tb2.nam.ID_NienKhoa,
                           MA_NienKhoa = tb4.tb3.tb2.nam.MA_NienKhoa,
                           TEN_NienKhoa = tb4.tb3.tb2.nam.TEN_NienKhoa,
                           ID_NganhHoc = tb4.tb3.tb2.tb1.nganh.ID_NganhHoc,
                           MA_NganhHoc = tb4.tb3.tb2.tb1.nganh.MA_NganhHoc,
                           TEN_NganhHoc = tb4.tb3.tb2.tb1.nganh.TEN_NganhHoc,
                           ID_KhoaHoc = tb4.tb3.khoa.ID_KhoaHoc,
                           MA_KhoaHoc = tb4.tb3.khoa.MA_KhoaHoc,
                           TEN_KhoaHoc = tb4.tb3.khoa.TEN_KhoaHoc,
                           NUM_LopHoc = tb4.p.NUM_LopHoc
                       });

                    return View(obj_LopHoc);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult GET_CTLopHoc(int pID_LopHoc)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    var lst_HocVien = entities.HOC_VIEN.ToList().
                            Join(entities.TAI_KHOAN.Where(p => p.IS_Deleted == 0).ToList(), hv => hv.ID_TaiKhoan, tk => tk.ID_TaiKhoan, (hv, tk) => new { hv, tk }).
                            Join(entities.CT_LOP_HOC.Where(p => p.ID_LopHoc == pID_LopHoc).ToList(), tb1 => tb1.hv.ID_HocVien, ct => ct.ID_HocVien, (tb1, ct) => new { tb1, ct }).
                             Select(tb2 => new HocVien_Model()
                             {
                                 ID_HocVien = tb2.tb1.hv.ID_HocVien,
                                 MA_HocVien = tb2.tb1.hv.MA_HocVien,
                                 TEN_HocVien = tb2.tb1.hv.TEN_HocVien,
                                 NSINH_HocVien = tb2.tb1.hv.NSINH_HocVien,
                                 GTINH_HocVien = tb2.tb1.hv.GTINH_HocVien,
                                 ANH_HocVien = tb2.tb1.hv.ANH_HocVien,
                                 ID_TaiKhoan = tb2.tb1.tk.ID_TaiKhoan,
                                 USER_TaiKhoan = tb2.tb1.tk.USER_TaiKhoan,
                                 PASS_TaiKhoan = tb2.tb1.tk.PASS_TaiKhoan,
                                 NOTE_TaiKhoan = tb2.tb1.tk.NOTE_TaiKhoan,
                                 IS_Locked = tb2.tb1.tk.IS_Locked
                             }).ToList();

                    return View(lst_HocVien);
                }

            }
            catch (Exception)
            {

                return View();
            }
        }

        [HttpGet]
        public ActionResult INSERT_LopHoc()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    LopHoc_ViewModel LopHoc = new LopHoc_ViewModel()
                    {
                        list_KhoaHoc = entities.KHOA_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                        list_NganhHoc = entities.NGANH_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                        list_NienKhoa = entities.NIEN_KHOA.Where(p => p.IS_Deleted == 0).ToList(),
                        object_LopHoc = null
                    };
                    return View(LopHoc);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult INSERT_LopHoc(LopHoc_ViewModel pLopHoc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        entities.LOP_HOC.Add(new LOP_HOC()
                        {
                            ID_LopHoc = entities.LOP_HOC.Max(p => p.ID_LopHoc) + 1,
                            MA_LopHoc = pLopHoc.object_LopHoc.MA_LopHoc,
                            TEN_LopHoc = pLopHoc.object_LopHoc.TEN_LopHoc,
                            ID_KhoaHoc = pLopHoc.object_LopHoc.ID_KhoaHoc,
                            ID_NganhHoc = pLopHoc.object_LopHoc.ID_NganhHoc,
                            ID_NienKhoa = pLopHoc.object_LopHoc.ID_NienKhoa,
                            IS_Deleted = 0,
                            TIME_Create = DateTime.Today,
                            TIME_Update = DateTime.Today
                        }); ;

                        entities.SaveChanges();

                        return RedirectToAction("GET_LopHoc", "HocVien", new { area = "NguoiDung" });
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
        public ActionResult UPDATE_LopHoc(int pID_LopHoc)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    LOP_HOC LopHoc = entities.LOP_HOC.Where(p => p.ID_LopHoc == pID_LopHoc).FirstOrDefault();

                    LopHoc_ViewModel LopHoc_VM = new LopHoc_ViewModel()
                    {
                        list_KhoaHoc = entities.KHOA_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                        list_NganhHoc = entities.NGANH_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                        list_NienKhoa = entities.NIEN_KHOA.Where(p => p.IS_Deleted == 0).ToList(),
                        object_LopHoc = new LopHoc_Model()
                        {
                            ID_LopHoc = LopHoc.ID_LopHoc,
                            MA_LopHoc = LopHoc.MA_LopHoc,
                            TEN_LopHoc = LopHoc.TEN_LopHoc,
                            ID_KhoaHoc = (int)LopHoc.ID_KhoaHoc,
                            ID_NganhHoc = (int)LopHoc.ID_NganhHoc,
                            ID_NienKhoa = (int)LopHoc.ID_NienKhoa,

                        }
                    };
                    return View(LopHoc_VM);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UPDATE_LopHoc(LopHoc_ViewModel pLopHoc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        LOP_HOC LopHoc = entities.LOP_HOC.Where(p => p.ID_LopHoc == pLopHoc.object_LopHoc.ID_LopHoc).FirstOrDefault();

                        LopHoc.MA_LopHoc = pLopHoc.object_LopHoc.MA_LopHoc;
                        LopHoc.TEN_LopHoc = pLopHoc.object_LopHoc.TEN_LopHoc;
                        LopHoc.ID_KhoaHoc = pLopHoc.object_LopHoc.ID_KhoaHoc;
                        LopHoc.ID_NganhHoc = pLopHoc.object_LopHoc.ID_NganhHoc;
                        LopHoc.ID_NienKhoa = pLopHoc.object_LopHoc.ID_NienKhoa;

                        entities.SaveChanges();

                        return RedirectToAction("GET_LopHoc", "HocVien", new { area = "NguoiDung" });
                    }
                }

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DELETE_LopHoc(FormCollection formCollection)
        {
            try
            {
                if (formCollection.Count > 1)
                {
                    string[] ids = formCollection["ID_LopHoc"].Split(new char[] { ',' });

                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        foreach (string item in ids)
                        {
                            int id = int.Parse(item);
                            entities.LOP_HOC.Where(p => p.ID_LopHoc == id).FirstOrDefault().IS_Deleted = 1;
                            entities.SaveChanges();

                        }
                    }
                }

                return RedirectToAction("GET_LopHoc", "HocVien", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DELETE_LopHoc(int pID_LopHoc)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    entities.LOP_HOC.Where(p => p.ID_LopHoc == pID_LopHoc).FirstOrDefault().IS_Deleted = 1;
                    entities.SaveChanges();
                }

                return RedirectToAction("GET_LopHoc", "HocVien", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion

    }
}