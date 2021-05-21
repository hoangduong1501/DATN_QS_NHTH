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
            Random rng = new Random();

            int n = pList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                CAU_HOI value = pList[k];
                pList[k] = pList[n];
                pList[n] = value;
            }

            for(int i = 0; i < pList.Count; i++)
            {
                string[] result = Shuffle(new string[] { pList[i].LCHON_1, pList[i].LCHON_2, pList[i].LCHON_3, pList[i].LCHON_4 });
                pList[i].LCHON_1 = result[0];
                pList[i].LCHON_2 = result[1];
                pList[i].LCHON_3 = result[2];
                pList[i].LCHON_4 = result[3];
            }

            return pList;
        }

        private string[] Shuffle(string[] wordArray)
        {
            Random random = new Random();
            for (int i = wordArray.Length - 1; i > 0; i--)
            {
                int swapIndex = random.Next(i + 1);
                string temp = wordArray[i];
                wordArray[i] = wordArray[swapIndex];
                wordArray[swapIndex] = temp;
            }
            return wordArray;
        }

        [HttpPost]
        public ActionResult GET_CauHoi(CTDeThi_ViewModel pCT_DeThi)
        {
            return View();
        }
    }
}