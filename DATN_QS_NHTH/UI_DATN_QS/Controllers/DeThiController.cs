using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_DATN_QS.Models.DB_Entities;
using UI_DATN_QS.Models.DB_Models;
using UI_DATN_QS.Models.Sessions;
using UI_DATN_QS.ViewModels;

namespace UI_DATN_QS.Controllers
{
    public class DeThiController : Controller
    {
        [HttpGet]
        public ActionResult GET_DeThi(int pID_MonHoc = 0)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionHV();
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
        public ActionResult GET_XacNhanThi(int pID_DeThi)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionHV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    DeThi_Model deTHi = entities.DE_THI.Where(p => p.ID_DeThi == pID_DeThi).ToList().
                                Join(entities.MON_HOC.ToList(), dt => dt.ID_MonHoc, mh => mh.ID_MonHoc, (dt, mh) => new { dt, mh }).
                                Join(entities.LOAI_BAI_THI.ToList(), tb1 => tb1.dt.ID_LBaiThi, lb => lb.ID_LBaiThi, (tb1, lb) => new { tb1, lb }).
                                Select(tb2 => new DeThi_Model()
                                {
                                    ID_DeThi = tb2.tb1.dt.ID_DeThi,
                                    MA_DeThi = tb2.tb1.dt.MA_DeThi,
                                    PASS_DeThi = null,
                                    TGIAN_DeThi = tb2.tb1.dt.TGIAN_DeThi,
                                    ID_LBaiThi = tb2.lb.ID_LBaiThi,
                                    TEN_LBaiThi = tb2.lb.TEN_LBaiThi,
                                    ID_MonHoc = tb2.tb1.mh.ID_MonHoc,
                                    TEN_MonHoc = tb2.tb1.mh.TEN_MonHoc,
                                    TIME_Create = tb2.tb1.dt.TIME_Create,
                                    IS_Locked = tb2.tb1.dt.IS_Locked
                                }).FirstOrDefault();

                    return View(deTHi);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult GET_XacNhanThi(DeThi_Model pDeThi)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionHV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    int id = entities.HOC_VIEN.Where(p => p.ID_HocVien == user_Session.ID_TaiKhoan).FirstOrDefault().ID_HocVien;
                    if (entities.BANG_DIEM.Where(p => p.ID_HocVien == id && p.ID_DeThi == pDeThi.ID_DeThi && p.IS_Deleted == 0).Count() > 0)
                    {
                        ViewBag.StatusPassword = 2;
                    }
                    else if (pDeThi.PASS_DeThi.Equals(entities.DE_THI.Where(p => p.ID_DeThi == pDeThi.ID_DeThi).FirstOrDefault().PASS_DeThi.Trim()))
                    {
                        return RedirectToAction("GET_CauHoi", "DeThi", new { area = "", pID_DeThi = pDeThi.ID_DeThi });
                    }
                    else
                    {
                        ViewBag.StatusPassword = 1;
                    }

                    return View(pDeThi);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult GET_CauHoi(int pID_DeThi)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionHV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;


            try
            {
                using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                {
                    List<CAU_HOI> list = entities.CT_DE_THI.Where(p => p.ID_DeThi == pID_DeThi).ToList().
                                            Join(entities.CAU_HOI.Where(p => p.IS_Deleted == 0).ToList(), ct => ct.ID_CauHoi, ch => ch.ID_CauHoi, (ct, ch) => new { ct, ch }).
                                            Select(tb => new CAU_HOI
                                            {
                                                NDUNG_CauHoi = tb.ch.NDUNG_CauHoi,
                                                ANH_CauHoi = tb.ch.ANH_CauHoi,
                                                ID_CauHoi = tb.ch.ID_CauHoi,
                                                LCHON_1 = tb.ch.LCHON_1,
                                                LCHON_2 = tb.ch.LCHON_2,
                                                LCHON_3 = tb.ch.LCHON_3,
                                                LCHON_4 = tb.ch.LCHON_4
                                            }).ToList();

                    CTDeThi_ViewModel CauHoi = new CTDeThi_ViewModel()
                    {
                        ID_DeThi = pID_DeThi,
                        TGIAN_DeThi = (int)entities.DE_THI.Where(p => p.ID_DeThi == pID_DeThi).FirstOrDefault().TGIAN_DeThi,
                        list_CauHoi = Shuffle_FisherYates(list)
                    };

                    return View(CauHoi);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        private List<CAU_HOI> Shuffle_FisherYates(List<CAU_HOI> pList)
        {
            Random rand = new Random();

            int n = pList.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                CAU_HOI value = pList[k];
                pList[k] = pList[n];
                pList[n] = value;

                string[] str_Array_K = new string[] { pList[k].LCHON_1, pList[k].LCHON_2, pList[k].LCHON_3, pList[k].LCHON_4 };
                List<int> lst = GeneratePassword();
                pList[k].LCHON_1 = str_Array_K[lst[0]];
                pList[k].LCHON_2 = str_Array_K[lst[1]];
                pList[k].LCHON_3 = str_Array_K[lst[2]];
                pList[k].LCHON_4 = str_Array_K[lst[3]];
            }

            return pList;
        }

        public List<int> GeneratePassword()
        {
            System.Random _random = new Random();
            List<int> lst = new List<int>();
            while (lst.Count() < 4)
            {
                int a = _random.Next(0, 4);
                if (lst.Where(item => item == a).Count() == 0)
                {
                    lst.Add(a);
                    Console.Write("" + a + ",");
                }
            }
            Console.WriteLine("");
            System.Threading.Thread.Sleep(100);
            return lst;
        }

        //private string[] Shuffle(string[] wordArray)
        //{
        //    Random random = new Random();
        //    for (int i = wordArray.Length - 1; i > 0; i--)
        //    {
        //        int swapIndex = random.Next(i + 1);
        //        string temp = wordArray[i];
        //        wordArray[i] = wordArray[swapIndex];
        //        wordArray[swapIndex] = temp;
        //    }

        //    return wordArray;
        //}

        [HttpPost]
        public ActionResult GET_CauHoi(CTDeThi_ViewModel pCT_DeThi)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionHV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            BANG_DIEM BangDiem = new BANG_DIEM()
            {
                ID_DeThi = pCT_DeThi.ID_DeThi,
            };

            int sum_CauHoi = pCT_DeThi.list_CauHoi.Count();
            int sum_CauDung = 0;
            float diem_1_cau = 10 / (float)sum_CauHoi;

            using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
            {
                foreach (CAU_HOI item in pCT_DeThi.list_CauHoi)
                {
                    if (item.LCHON_Dung == entities.CAU_HOI.Where(p => p.ID_CauHoi == item.ID_CauHoi).FirstOrDefault().LCHON_Dung)
                    {
                        sum_CauDung++;
                    }
                }

                BangDiem.DUNG_BangDiem = sum_CauDung;
                BangDiem.DIEM_BangDiem = Math.Round(diem_1_cau * sum_CauDung, 1);
                BangDiem.IS_Deleted = 0;
                BangDiem.TIME_Create = DateTime.Today;

                if (BangDiem.DIEM_BangDiem > 10.0f) BangDiem.DIEM_BangDiem = 10.0f;
                BangDiem.ID_HocVien = entities.HOC_VIEN.Where(p => p.ID_HocVien == user_Session.ID_TaiKhoan).FirstOrDefault().ID_HocVien;

                if (entities.BANG_DIEM.Count() == 0) BangDiem.ID_BangDiem = 1;
                else BangDiem.ID_BangDiem = entities.BANG_DIEM.Max(p => p.ID_BangDiem) + 1;

                entities.BANG_DIEM.Add(BangDiem);
                entities.SaveChanges();

                return RedirectToAction("GET_KetQua", "DeThi", new { area = "", pID_BangDiem = BangDiem.ID_BangDiem });
            }
        }

        public ActionResult GET_KetQua(int pID_BangDiem)
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionHV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
            {
                return View(entities.BANG_DIEM.Where(p => p.ID_BangDiem == pID_BangDiem).FirstOrDefault());
            }
        }
    }
}