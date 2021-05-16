﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_DATN_QS.Models.DB_Entities;
using UI_DATN_QS.Models.DB_Models;
using UI_DATN_QS.ViewModels;

namespace UI_DATN_QS.Areas.NguoiDung.Controllers
{
    public class KyThiController : Controller
    {
        /*NienKhoa*/
        #region
        [HttpGet]
        public ActionResult GET_NienKhoa()
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    List<NIEN_KHOA> lst_NienKhoa = entities.NIEN_KHOA.Where(p => p.IS_Deleted == 0).ToList();

                    return View(lst_NienKhoa);
                }
            }
            catch (Exception)
            {

                return View();
            }
        }

        [HttpGet]
        public ActionResult INSERT_NienKhoa()
        {
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
        public ActionResult INSERT_NienKhoa(NIEN_KHOA pNienKhoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        if (entities.NIEN_KHOA.Count() == 0) pNienKhoa.ID_NienKhoa = 1;
                        else pNienKhoa.ID_NienKhoa = entities.NIEN_KHOA.Max(p => p.ID_NienKhoa) + 1;

                        pNienKhoa.IS_Deleted = 0;
                        pNienKhoa.TIME_Create = pNienKhoa.TIME_Update = DateTime.Today;

                        entities.NIEN_KHOA.Add(pNienKhoa);
                        entities.SaveChanges();

                        return RedirectToAction("GET_NienKhoa", "KyThi", new { area = "NguoiDung" });
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
        public ActionResult UPDATE_NienKhoa(int pID_NienKhoa)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    return View(entities.NIEN_KHOA.Where(p => p.ID_NienKhoa == pID_NienKhoa).FirstOrDefault());
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UPDATE_NienKhoa(NIEN_KHOA pNienKhoa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        NIEN_KHOA NienKhoa = entities.NIEN_KHOA.Where(p => p.ID_NienKhoa == pNienKhoa.ID_NienKhoa).FirstOrDefault();
                        NienKhoa.MA_NienKhoa = pNienKhoa.MA_NienKhoa;
                        NienKhoa.TEN_NienKhoa = pNienKhoa.TEN_NienKhoa;
                        NienKhoa.NOTE_NienKhoa = pNienKhoa.NOTE_NienKhoa;
                        NienKhoa.TIME_Update = DateTime.Today;

                        entities.SaveChanges();

                        return RedirectToAction("GET_NienKhoa", "KyThi", new { area = "NguoiDung" });
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
        public ActionResult DELETE_NienKhoa(int pID_NienKhoa)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    entities.NIEN_KHOA.Where(p => p.ID_NienKhoa == pID_NienKhoa).FirstOrDefault().IS_Deleted = 1;
                    entities.SaveChanges();
                }

                return RedirectToAction("GET_NienKhoa", "KyThi", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DELETE_NienKhoa(FormCollection formCollection)
        {
            try
            {
                if (formCollection.Count > 1)
                {
                    string[] ids = formCollection["ID_NienKhoa"].Split(new char[] { ',' });

                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        foreach (string item in ids)
                        {
                            int id = int.Parse(item);
                            entities.NIEN_KHOA.Where(p => p.ID_NienKhoa == id).FirstOrDefault().IS_Deleted = 1;
                            entities.SaveChanges();

                        }
                    }
                }

                return RedirectToAction("GET_NienKhoa", "KyThi", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion

        /*LoaiBaiThi*/
        #region
        [HttpGet]
        public ActionResult GET_LoaiBaiThi()
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    List<LOAI_BAI_THI> lst_LBaiThi = entities.LOAI_BAI_THI.Where(p => p.IS_Deleted == 0).ToList();

                    return View(lst_LBaiThi);
                }
            }
            catch (Exception)
            {

                return View();
            }
        }

        [HttpGet]
        public ActionResult INSERT_LoaiBaiThi()
        {
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
        public ActionResult INSERT_LoaiBaiThi(LOAI_BAI_THI pLoaiBaiThi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        if (entities.LOAI_BAI_THI.Count() == 0) pLoaiBaiThi.ID_LBaiThi = 1;
                        else pLoaiBaiThi.ID_LBaiThi = entities.LOAI_BAI_THI.Max(p => p.ID_LBaiThi) + 1;

                        pLoaiBaiThi.IS_Deleted = 0;
                        pLoaiBaiThi.TIME_Create = pLoaiBaiThi.TIME_Update = DateTime.Today;

                        entities.LOAI_BAI_THI.Add(pLoaiBaiThi);
                        entities.SaveChanges();

                        return RedirectToAction("GET_NienKhoa", "KyThi", new { area = "NguoiDung" });
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
        public ActionResult UPDATE_LoaiBaiThi(int pID_LBaiThi)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    return View(entities.LOAI_BAI_THI.Where(p => p.ID_LBaiThi == pID_LBaiThi).FirstOrDefault());
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UPDATE_LoaiBaiThi(LOAI_BAI_THI pLoaiBaiThi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        LOAI_BAI_THI LoaiBaiThi = entities.LOAI_BAI_THI.Where(p => p.ID_LBaiThi == pLoaiBaiThi.ID_LBaiThi).FirstOrDefault();
                        LoaiBaiThi.MA_LBaiThi = pLoaiBaiThi.MA_LBaiThi;
                        LoaiBaiThi.TEN_LBaiThi = pLoaiBaiThi.TEN_LBaiThi;
                        LoaiBaiThi.NOTE_LBaiThi = pLoaiBaiThi.NOTE_LBaiThi;
                        LoaiBaiThi.TIME_Update = DateTime.Today;

                        entities.SaveChanges();

                        return RedirectToAction("GET_LoaiBaiThi", "KyThi", new { area = "NguoiDung" });
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
        public ActionResult DELETE_LoaiBaiThi(int pID_LBaiThi)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    entities.LOAI_BAI_THI.Where(p => p.ID_LBaiThi == pID_LBaiThi).FirstOrDefault().IS_Deleted = 1;
                    entities.SaveChanges();
                }

                return RedirectToAction("GET_LoaiBaiThi", "KyThi", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DELETE_LoaiBaiThi(FormCollection formCollection)
        {
            try
            {
                if (formCollection.Count > 1)
                {
                    string[] ids = formCollection["ID_LBaiThi"].Split(new char[] { ',' });

                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        foreach (string item in ids)
                        {
                            entities.LOAI_BAI_THI.Where(p => p.ID_LBaiThi == int.Parse(item)).FirstOrDefault().IS_Deleted = 1;
                            entities.SaveChanges();

                        }
                    }

                }
                return RedirectToAction("GET_LoaiBaiThi", "KyThi", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion

        /*KhoaHoc*/
        #region
        [HttpGet]
        public ActionResult GET_KhoaHoc()
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    List<KHOA_HOC> lst_KhoaHoc = entities.KHOA_HOC.Where(p => p.IS_Deleted == 0).ToList();

                    return View(lst_KhoaHoc);
                }
            }
            catch (Exception)
            {

                return View();
            }
        }

        [HttpGet]
        public ActionResult INSERT_KhoaHoc()
        {
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
        public ActionResult INSERT_KhoaHoc(KHOA_HOC pKhoaHoc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        if (entities.KHOA_HOC.Count() == 0) pKhoaHoc.ID_KhoaHoc = 1;
                        else pKhoaHoc.ID_KhoaHoc = entities.KHOA_HOC.Max(p => p.ID_KhoaHoc) + 1;

                        pKhoaHoc.IS_Deleted = 0;
                        pKhoaHoc.TIME_Create = pKhoaHoc.TIME_Update = DateTime.Today;

                        entities.KHOA_HOC.Add(pKhoaHoc);
                        entities.SaveChanges();

                        return RedirectToAction("GET_KhoaHoc", "KyThi", new { area = "NguoiDung" });
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
        public ActionResult UPDATE_KhoaHoc(int pID_KhoaHoc)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    return View(entities.KHOA_HOC.Where(p => p.ID_KhoaHoc == pID_KhoaHoc).FirstOrDefault());
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UPDATE_KhoaHoc(KHOA_HOC pKhoaHoc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        KHOA_HOC KhoaHoc = entities.KHOA_HOC.Where(p => p.ID_KhoaHoc == pKhoaHoc.ID_KhoaHoc).FirstOrDefault();
                        KhoaHoc.MA_KhoaHoc = pKhoaHoc.MA_KhoaHoc;
                        KhoaHoc.TEN_KhoaHoc = pKhoaHoc.TEN_KhoaHoc;
                        KhoaHoc.NOTE_KhoaHoc = pKhoaHoc.NOTE_KhoaHoc;
                        KhoaHoc.TIME_Update = DateTime.Today;

                        entities.SaveChanges();

                        return RedirectToAction("GET_KhoaHoc", "KyThi", new { area = "NguoiDung" });
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
        public ActionResult DELETE_KhoaHoc(int pID_KhoaHoc)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    entities.KHOA_HOC.Where(p => p.ID_KhoaHoc == pID_KhoaHoc).FirstOrDefault().IS_Deleted = 1;
                    entities.SaveChanges();
                }

                return RedirectToAction("GET_KhoaHoc", "KyThi", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DELETE_KhoaHoc(FormCollection formCollection)
        {
            try
            {
                if (formCollection.Count > 1)
                {
                    string[] ids = formCollection["ID_KhoaHoc"].Split(new char[] { ',' });

                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        foreach (string item in ids)
                        {
                            int id = int.Parse(item);
                            entities.KHOA_HOC.Where(p => p.ID_KhoaHoc == id).FirstOrDefault().IS_Deleted = 1;
                            entities.SaveChanges();

                        }
                    }

                }
                return RedirectToAction("GET_KhoaHoc", "KyThi", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion

        /*MonHoc*/
        #region
        [HttpGet]
        public ActionResult GET_MonHoc()
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    List<MON_HOC> lst_MonHoc = entities.MON_HOC.Where(p => p.IS_Deleted == 0).ToList();

                    return View(lst_MonHoc);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult INSERT_MonHoc()
        {
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
        public ActionResult INSERT_MonHoc(MON_HOC pMonHoc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        if (entities.MON_HOC.Count() == 0) pMonHoc.ID_MonHoc = 1;
                        else pMonHoc.ID_MonHoc = entities.MON_HOC.Max(p => p.ID_MonHoc) + 1;

                        pMonHoc.IS_Deleted = 0;
                        pMonHoc.TIME_Create = pMonHoc.TIME_Update = DateTime.Today;

                        entities.MON_HOC.Add(pMonHoc);
                        entities.SaveChanges();

                        return RedirectToAction("GET_MonHoc", "KyThi", new { area = "NguoiDung" });
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
        public ActionResult UPDATE_MonHoc(int pID_MonHoc)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    return View(entities.MON_HOC.Where(p => p.ID_MonHoc == pID_MonHoc).FirstOrDefault());
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UPDATE_MonHoc(MON_HOC pMonHoc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        MON_HOC MonHoc = entities.MON_HOC.Where(p => p.ID_MonHoc == pMonHoc.ID_MonHoc).FirstOrDefault();
                        MonHoc.MA_MonHoc = pMonHoc.MA_MonHoc;
                        MonHoc.TEN_MonHoc = pMonHoc.TEN_MonHoc;
                        MonHoc.NOTE_MonHoc = pMonHoc.NOTE_MonHoc;
                        MonHoc.TIME_Update = DateTime.Today;

                        entities.SaveChanges();

                        return RedirectToAction("GET_MonHoc", "KyThi", new { area = "NguoiDung" });
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
        public ActionResult DELETE_MonHoc(int pID_MonHoc)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    entities.MON_HOC.Where(p => p.ID_MonHoc == pID_MonHoc).FirstOrDefault().IS_Deleted = 1;
                    entities.SaveChanges();
                }

                return RedirectToAction("GET_MonHoc", "KyThi", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DELETE_MonHoc(FormCollection formCollection)
        {
            try
            {
                if (formCollection.Count > 1)
                {
                    string[] ids = formCollection["ID_MonHoc"].Split(new char[] { ',' });

                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        foreach (string item in ids)
                        {
                            int id = int.Parse(item);
                            entities.MON_HOC.Where(p => p.ID_MonHoc == id).FirstOrDefault().IS_Deleted = 1;
                            entities.SaveChanges();

                        }
                    }

                }
                return RedirectToAction("GET_MonHoc", "KyThi", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion

        /*CauHoi*/
        #region
        [HttpGet]
        public ActionResult GET_CauHoi(int pID_MonHoc = 0)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    CauHoi_ViewModel CauHoi;

                    if (pID_MonHoc == 0)
                    {
                        CauHoi = new CauHoi_ViewModel()
                        {
                            list_MonHoc = entities.MON_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                            list_CauHoi = entities.CAU_HOI.Where(p => p.IS_Deleted == 0).ToList(),
                            ID_MonHoc = pID_MonHoc
                        };
                    }
                    else
                    {
                        CauHoi = new CauHoi_ViewModel()
                        {
                            list_MonHoc = entities.MON_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                            list_CauHoi = entities.CAU_HOI.Where(p => p.IS_Deleted == 0 && p.ID_MonHoc == pID_MonHoc).ToList(),
                            ID_MonHoc = pID_MonHoc
                        };
                    }

                    CauHoi.list_MonHoc.Add(new MON_HOC()
                    {
                        ID_MonHoc = 0,
                        TEN_MonHoc = "Tất cả"
                    });

                    return View(CauHoi);
                }
            }
            catch (Exception) { }
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult INSERT_CauHoi()
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    CauHoi_ViewModel CauHoi = new CauHoi_ViewModel()
                    {
                        list_MonHoc = entities.MON_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                    };

                    return View(CauHoi);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult INSERT_CauHoi(CauHoi_ViewModel pCauHoi, HttpPostedFileBase fileLoad)
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
                        if (entities.CAU_HOI.Count() == 0) pCauHoi.object_CauHoi.ID_CauHoi = 1;
                        else pCauHoi.object_CauHoi.ID_CauHoi = entities.CAU_HOI.Max(p => p.ID_CauHoi) + 1;

                        pCauHoi.object_CauHoi.ANH_CauHoi = img_Upload;
                        pCauHoi.object_CauHoi.LCHON_4 = pCauHoi.object_CauHoi.LCHON_Dung;
                        pCauHoi.object_CauHoi.TIME_Create = pCauHoi.object_CauHoi.TIME_Update = DateTime.Today;
                        pCauHoi.object_CauHoi.IS_Deleted = 0;

                        entities.CAU_HOI.Add(pCauHoi.object_CauHoi);

                        entities.SaveChanges();

                        return RedirectToAction("GET_CauHoi", "KyThi", new { area = "NguoiDung" });
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
        public ActionResult UPDATE_CauHoi(int pID_CauHoi)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    CauHoi_ViewModel CauHoi = new CauHoi_ViewModel()
                    {
                        list_MonHoc = entities.MON_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                        object_CauHoi = entities.CAU_HOI.Where(p => p.IS_Deleted == 0 && p.ID_CauHoi == pID_CauHoi).FirstOrDefault()
                    };

                    return View(CauHoi);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UPDATE_CauHoi(CauHoi_ViewModel pCauHoi, HttpPostedFileBase fileLoad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        byte[] img_Upload = null;
                        if (fileLoad != null)
                        {
                            img_Upload = new byte[fileLoad.InputStream.Length];
                            fileLoad.InputStream.Read(img_Upload, 0, img_Upload.Length);
                        }


                        CAU_HOI CauHoi = entities.CAU_HOI.Where(p => p.ID_CauHoi == pCauHoi.object_CauHoi.ID_CauHoi).FirstOrDefault();
                        CauHoi.ID_CauHoi = pCauHoi.object_CauHoi.ID_CauHoi;
                        CauHoi.NDUNG_CauHoi = pCauHoi.object_CauHoi.NDUNG_CauHoi;
                        CauHoi.LCHON_1 = pCauHoi.object_CauHoi.LCHON_1;
                        CauHoi.LCHON_2 = pCauHoi.object_CauHoi.LCHON_2;
                        CauHoi.LCHON_3 = pCauHoi.object_CauHoi.LCHON_3;
                        CauHoi.LCHON_4 = CauHoi.LCHON_Dung = pCauHoi.object_CauHoi.LCHON_Dung;
                        CauHoi.ID_Chuong = pCauHoi.object_CauHoi.ID_Chuong;
                        CauHoi.TIME_Update = DateTime.Today;

                        if (fileLoad != null)
                        {
                            CauHoi.ANH_CauHoi = img_Upload;
                        }

                        entities.SaveChanges();

                        return RedirectToAction("GET_CauHoi", "KyThi", new { area = "NguoiDung" });
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
        public ActionResult DELETE_CauHoi(int pID_CauHoi)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    entities.CAU_HOI.Where(p => p.ID_CauHoi == pID_CauHoi).FirstOrDefault().IS_Deleted = 1;
                    entities.SaveChanges();
                }

                return RedirectToAction("GET_CauHoi", "KyThi", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DELETE_CauHoi(FormCollection formCollection)
        {
            try
            {
                if (formCollection.Count > 1)
                {
                    string[] ids = formCollection["ID_CauHoi"].Split(new char[] { ',' });

                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        foreach (string item in ids)
                        {
                            int id = int.Parse(item);
                            entities.CAU_HOI.Where(p => p.ID_CauHoi == id).FirstOrDefault().IS_Deleted = 1;
                            entities.SaveChanges();

                        }
                    }

                }
                return RedirectToAction("GET_CauHoi", "KyThi", new { area = "NguoiDung" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion

        /*DeThi*/
        #region
        [HttpGet]
        public ActionResult GET_DeThi(int pID_MonHoc = 0)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    DeThi_ViewModel DeThi;

                    if (pID_MonHoc == 0)
                    {
                        DeThi = new DeThi_ViewModel()
                        {
                            list_MonHoc = entities.MON_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                            ID_MonHoc = pID_MonHoc,
                            list_DeThi = (List<DeThi_Model>)entities.DE_THI.Where(p => p.IS_Deleted == 0).ToList().
                                Join(entities.MON_HOC.ToList(), dt => dt.ID_MonHoc, mh => mh.ID_MonHoc, (dt, mh) => new { dt, mh }).
                                Join(entities.LOAI_BAI_THI.ToList(), tb1 => tb1.dt.ID_LBaiThi, lb => lb.ID_LBaiThi, (tb1, lb) => new { tb1, lb }).
                                Select(tb2 => new DeThi_Model()
                                {
                                    ID_DeThi = tb2.tb1.dt.ID_DeThi,
                                    MA_DeThi = tb2.tb1.dt.MA_DeThi,
                                    PASS_DeThi = tb2.tb1.dt.PASS_DeThi,
                                    TGIAN_DeThi = tb2.tb1.dt.TGIAN_DeThi,
                                    ID_LBaiThi = tb2.lb.ID_LBaiThi,
                                    TEN_LBaiThi = tb2.lb.TEN_LBaiThi,
                                    ID_MonHoc = tb2.tb1.mh.ID_MonHoc,
                                    TEN_MonHoc = tb2.tb1.mh.TEN_MonHoc,
                                    TIME_Create = tb2.tb1.dt.TIME_Create,
                                    IS_Locked = tb2.tb1.dt.IS_Locked
                                }).ToList()
                        };
                    }
                    else
                    {
                        DeThi = new DeThi_ViewModel()
                        {
                            list_MonHoc = entities.MON_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                            ID_MonHoc = pID_MonHoc,
                            list_DeThi = (List<DeThi_Model>)entities.DE_THI.Where(p => p.IS_Deleted == 0).ToList().
                                Join(entities.MON_HOC.Where(p => p.ID_MonHoc == pID_MonHoc).ToList(), dt => dt.ID_MonHoc, mh => mh.ID_MonHoc, (dt, mh) => new { dt, mh }).
                                Join(entities.LOAI_BAI_THI.ToList(), tb1 => tb1.dt.ID_LBaiThi, lb => lb.ID_LBaiThi, (tb1, lb) => new { tb1, lb }).
                                Select(tb2 => new DeThi_Model()
                                {
                                    ID_DeThi = tb2.tb1.dt.ID_DeThi,
                                    MA_DeThi = tb2.tb1.dt.MA_DeThi,
                                    PASS_DeThi = tb2.tb1.dt.PASS_DeThi,
                                    TGIAN_DeThi = tb2.tb1.dt.TGIAN_DeThi,
                                    IS_Locked = tb2.tb1.dt.IS_Locked,
                                    TIME_Create = tb2.tb1.dt.TIME_Create,
                                    ID_LBaiThi = tb2.lb.ID_LBaiThi,
                                    TEN_LBaiThi = tb2.lb.TEN_LBaiThi,
                                    ID_MonHoc = tb2.tb1.mh.ID_MonHoc,
                                    TEN_MonHoc = tb2.tb1.mh.TEN_MonHoc,
                                }).ToList()
                        };
                    }

                    DeThi.list_MonHoc.Add(new MON_HOC()
                    {
                        ID_MonHoc = 0,
                        TEN_MonHoc = "Tất cả"
                    });

                    return View(DeThi);
                }
            }
            catch (Exception) { }
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult INSERT_DeThi()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion
    }
}