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
    public class BangDiemHVController : Controller
    {
        // GET: BangDiem
        public ActionResult GET_BangDiem()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionHV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    int id = entities.HOC_VIEN.Where(p => p.ID_TaiKhoan == user_Session.ID_TaiKhoan).FirstOrDefault().ID_HocVien;

                    return View(entities.BANG_DIEM.Where(p => p.IS_Deleted == 0 && p.ID_HocVien == id).ToList().
                                Join(entities.DE_THI.ToList(), bd => bd.ID_DeThi, dt => dt.ID_DeThi, (bd, dt) => new { bd, dt }).
                                Join(entities.MON_HOC.ToList(), tb1 => tb1.dt.ID_MonHoc, mh => mh.ID_MonHoc, (tb1, mh) => new { tb1, mh }).
                                Join(entities.LOAI_BAI_THI.ToList(), tb2 => tb2.tb1.dt.ID_LBaiThi, lb => lb.ID_LBaiThi, (tb2, lb) => new { tb2, lb }).
                                Select(tb3 => new BangDiem_Model()
                                {
                                    ID_BangDiem = tb3.tb2.tb1.bd.ID_BangDiem,
                                    ID_DeThi = tb3.tb2.tb1.dt.ID_DeThi,
                                    MA_DeThi = tb3.tb2.tb1.dt.MA_DeThi,
                                    DIEM_BangDiem = tb3.tb2.tb1.bd.DIEM_BangDiem,
                                    DUNG_BangDiem = tb3.tb2.tb1.bd.DUNG_BangDiem,
                                    TEN_MonHoc = tb3.tb2.mh.TEN_MonHoc,
                                    TEN_LBaiThi = tb3.lb.TEN_LBaiThi,
                                    TIME_Create = tb3.tb2.tb1.bd.TIME_Create

                                }).ToList());
                }
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}