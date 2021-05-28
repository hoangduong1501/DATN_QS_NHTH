using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_DATN_QS.Models.DB_Entities;
using UI_DATN_QS.Models.DB_Models;
using UI_DATN_QS.Models.Sessions;

namespace UI_DATN_QS.Areas.NguoiDung.Controllers
{
    public class BangDiemController : Controller
    {
        [HttpGet]
        public ActionResult GET_BangDiemLop()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionND();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
            {
                return View(entities.KHOA_HOC.Where(p => p.IS_Deleted == 0).ToList());
            }
        }

        [HttpGet]
        public JsonResult JSON_KhoaHoc()
        {
            using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
            {
                return Json(entities.KHOA_HOC.Where(p => p.IS_Deleted == 0).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult JSON_LopHoc(int pID_LopHoc)
        {
            using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
            {
                return Json(entities.LOP_HOC.Where(p => p.IS_Deleted == 0 && p.ID_KhoaHoc == pID_LopHoc).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult JSON_MonHoc()
        {
            using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
            {
                return Json(entities.MON_HOC.Where(p => p.IS_Deleted == 0).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult JSON_BangDiem(BangDiemLop_Model pInput)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    return Json(
                                    entities.CT_LOP_HOC.Where(p => p.IS_Deleted == 0 && p.ID_LopHoc == pInput.ID_LopHoc).ToList().
                                        Join(entities.HOC_VIEN.ToList(), ctl => ctl.ID_HocVien, hv => hv.ID_HocVien, (ctl, hv) => new { ctl, hv }).
                                        Join(entities.BANG_DIEM.Where(p => p.IS_Deleted == 0).ToList(), tb1 => tb1.hv.ID_HocVien, bd => bd.ID_HocVien, (tb1, bd) => new { tb1, bd }).
                                        Join(entities.DE_THI.Where(p => p.ID_MonHoc == pInput.ID_MonHoc).ToList(), tb2 => tb2.bd.ID_DeThi, dt => dt.ID_DeThi, (tb2, dt) => new { tb2, dt }).
                                        Select(tb3 => new
                                        {
                                            MA_HocVien = tb3.tb2.tb1.hv.MA_HocVien,
                                            TEN_HocVien = tb3.tb2.tb1.hv.TEN_HocVien,
                                            MA_DeThi = tb3.dt.MA_DeThi,
                                            DUNG_BangDiem = tb3.tb2.bd.DUNG_BangDiem,
                                            DIEM_BangDiem = tb3.tb2.bd.DIEM_BangDiem,
                                            TIME_Create = tb3.tb2.bd.TIME_Create.Value.ToString("dd/MM/yyyy"),
                                            ID_BangDiem = tb3.tb2.bd.ID_BangDiem
                                        }).ToList(),
                                    JsonRequestBehavior.AllowGet
                                );
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        public JsonResult JSON_Delete_BangDiem(int pID_BangDiem)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    entities.BANG_DIEM.Where(p => p.ID_BangDiem == pID_BangDiem).FirstOrDefault().IS_Deleted = 1;
                    entities.SaveChanges();

                    return Json("Xóa thành công!", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("Lỗi, Xóa không thành công!", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult JSON_Delete_BangDiem(int[] pArray)
        {
            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    foreach (int item in pArray)
                    {
                        entities.BANG_DIEM.Where(p => p.ID_BangDiem == item).FirstOrDefault().IS_Deleted = 1;
                        entities.SaveChanges();
                    }

                    return Json("Xóa thành công!", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("Lỗi, Xóa không thành công!", JsonRequestBehavior.AllowGet);
            }
        }

    }
}