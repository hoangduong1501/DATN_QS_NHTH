using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_DATN_QS.Models.DB_Entities;
using UI_DATN_QS.Models.DB_Models;
using UI_DATN_QS.Models.HashCodes;
using UI_DATN_QS.Models.Sessions;
using UI_DATN_QS.ViewModels;

namespace UI_DATN_QS.Areas.GiangVien.Controllers
{
    public class DeThiGVController : Controller
    {
        /*De Thi*/
        #region
        [HttpGet]
        public ActionResult GET_DeThi(int pID_MonHoc = 0)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionGV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

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
        public ActionResult INSERT_DeThi(int pID_MonHoc = 0)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionGV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    if (pID_MonHoc == 0) pID_MonHoc = entities.MON_HOC.Where(p => p.IS_Deleted == 0).Min(c => c.ID_MonHoc);

                    string MA_DeThi_Tmp = StringRandom.GeneratePassword();

                    while (entities.DE_THI.Where(p => p.MA_DeThi == MA_DeThi_Tmp).Count() > 0)
                    {
                        MA_DeThi_Tmp = StringRandom.GeneratePassword();
                    }

                    DeThi_ViewModel CauHoi = new DeThi_ViewModel()
                    {
                        list_MonHoc = entities.MON_HOC.Where(p => p.IS_Deleted == 0).ToList(),
                        list_LBaiThi = entities.LOAI_BAI_THI.Where(p => p.IS_Deleted == 0).ToList(),
                        ID_MonHoc = pID_MonHoc,
                        object_DeThi = new DeThi_Model()
                        {
                            ID_MonHoc = pID_MonHoc,
                            MA_DeThi = MA_DeThi_Tmp
                        },
                        list_CauHoi = entities.CAU_HOI.Where(p => p.IS_Deleted == 0 && p.ID_MonHoc == pID_MonHoc).ToList().GroupBy(p => p.ID_Chuong).
                                                Select(tb1 => new { ID_Chuong = tb1.Key, NUM_CauHoi = tb1.Count() }).
                                                Select(tb2 => new CauHoi_Model() { ID_Chuong = (int)tb2.ID_Chuong, NUM_CauHoi = tb2.NUM_CauHoi }).ToList()
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
        public ActionResult INSERT_DeThi(DeThi_ViewModel pDeThi)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    if (entities.DE_THI.Count() == 0) pDeThi.object_DeThi.ID_DeThi = 1;
                    else pDeThi.object_DeThi.ID_DeThi = entities.DE_THI.Max(p => p.ID_DeThi) + 1;

                    entities.DE_THI.Add(new DE_THI()
                    {
                        ID_DeThi = pDeThi.object_DeThi.ID_DeThi,
                        MA_DeThi = pDeThi.object_DeThi.MA_DeThi,
                        IS_Deleted = 0,
                        IS_Locked = 0,
                        ID_LBaiThi = pDeThi.object_DeThi.ID_LBaiThi,
                        ID_MonHoc = pDeThi.object_DeThi.ID_MonHoc,
                        PASS_DeThi = pDeThi.object_DeThi.PASS_DeThi,
                        TIME_Create = DateTime.Today,
                        TGIAN_DeThi = pDeThi.object_DeThi.TGIAN_DeThi
                    });

                    entities.SaveChanges();

                    foreach (var item in pDeThi.list_CauHoi)
                    {
                        if (item.NUM_CauHoi > 0)
                        {
                            foreach (CAU_HOI item_CauHoi in entities.CAU_HOI.Where(p => p.ID_Chuong == item.ID_Chuong && p.ID_MonHoc == pDeThi.object_DeThi.ID_MonHoc).ToList().
                            OrderBy(p => Guid.NewGuid()).Take(item.NUM_CauHoi).ToList())
                            {
                                int id = 1;

                                if (entities.CT_DE_THI.Count() > 0) id = entities.CT_DE_THI.Max(p => p.ID_CTDeThi) + 1;

                                entities.CT_DE_THI.Add(new CT_DE_THI()
                                {
                                    ID_CTDeThi = id,
                                    ID_CauHoi = item_CauHoi.ID_CauHoi,
                                    ID_DeThi = pDeThi.object_DeThi.ID_DeThi,
                                    IS_Deleted = 0,
                                    TIME_Create = DateTime.Today,
                                    TIME_Update = DateTime.Today
                                });

                                entities.SaveChanges();
                            }
                        }
                    }

                }
                return RedirectToAction("GET_DeThi", "DeThiGV", new { area = "GiangVien" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DELETE_DeThi(int pID_DeThi)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    entities.DE_THI.Where(p => p.ID_DeThi == pID_DeThi).FirstOrDefault().IS_Deleted = 1;
                    entities.SaveChanges();
                }

                return RedirectToAction("GET_DeThi", "DeThiGV", new { area = "GiangVien" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DELETE_DeThi(FormCollection formCollection)
        {
            try
            {
                if (formCollection.Count > 1)
                {
                    string[] ids = formCollection["ID_DeThi"].Split(new char[] { ',' });

                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        foreach (string item in ids)
                        {
                            int id = int.Parse(item);
                            entities.DE_THI.Where(p => p.ID_DeThi == id).FirstOrDefault().IS_Deleted = 1;
                            entities.SaveChanges();

                        }
                    }

                }
                return RedirectToAction("GET_DeThi", "DeThiGV", new { area = "GiangVien" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult IsLock_DeThi(int pID_DeThi)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    DE_THI DeThi = entities.DE_THI.Where(p => p.ID_DeThi == pID_DeThi).FirstOrDefault();

                    if (DeThi.IS_Locked == 1) DeThi.IS_Locked = 0;
                    else DeThi.IS_Locked = 1;

                    entities.SaveChanges();
                }

                return Redirect(Request.UrlReferrer.ToString());
            }
            catch (Exception)
            {
                return View();
            }
        }
        #endregion

        /*Cau Hoi*/
        #region
        [HttpGet]
        public ActionResult GET_CauHoi(int pID_MonHoc = 0)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionGV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

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
            UserSession_Model user_Session = SessionHelper.Get_SessionGV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

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

                        return RedirectToAction("GET_CauHoi", "DeThiGV", new { area = "GiangVien" });
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
            UserSession_Model user_Session = SessionHelper.Get_SessionGV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

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
                        CauHoi.ID_MonHoc = pCauHoi.object_CauHoi.ID_MonHoc;

                        if (fileLoad != null)
                        {
                            CauHoi.ANH_CauHoi = img_Upload;
                        }

                        entities.SaveChanges();

                        return RedirectToAction("GET_CauHoi", "DeThiGV", new { area = "GiangVien" });
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

                return RedirectToAction("GET_CauHoi", "DeThiGV", new { area = "GiangVien" });
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
                return RedirectToAction("GET_CauHoi", "DeThiGV", new { area = "GiangVien" });
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion
    }
}