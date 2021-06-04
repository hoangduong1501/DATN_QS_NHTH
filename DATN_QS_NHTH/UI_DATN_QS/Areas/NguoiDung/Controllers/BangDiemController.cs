using OfficeOpenXml;
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

        [HttpPost]
        public JsonResult Export_BangDiem(BangDiemLop_Model pInput)
        {
            string nameFile = DateTime.Today.Ticks.ToString() + ".xlsx";
            try
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        var lst = entities.CT_LOP_HOC.Where(p => p.IS_Deleted == 0 && p.ID_LopHoc == pInput.ID_LopHoc).ToList().
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
                                            }).ToList();


                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Bảng Điểm");
                        worksheet.Cells["A1"].Value = "Mã học viên";
                        worksheet.Cells["B1"].Value = "Tên học viên";
                        worksheet.Cells["C1"].Value = "Đề thi";
                        worksheet.Cells["D1"].Value = "Số câu đúng";
                        worksheet.Cells["E1"].Value = "Điểm";
                        worksheet.Cells["F1"].Value = "Ngày thi";
                        int row = 2;

                        if (lst.Count() > 0)
                        {
                            foreach (var item in lst)
                            {
                                worksheet.Cells[String.Format("A{0}", row)].Value = item.MA_HocVien;
                                worksheet.Cells[String.Format("B{0}", row)].Value = item.TEN_HocVien;
                                worksheet.Cells[String.Format("C{0}", row)].Value = item.MA_DeThi;
                                worksheet.Cells[String.Format("D{0}", row)].Value = item.DUNG_BangDiem;
                                worksheet.Cells[String.Format("E{0}", row)].Value = item.DIEM_BangDiem;
                                worksheet.Cells[String.Format("F{0}", row)].Value = item.TIME_Create;
                                row++;
                            }
                        }

                        worksheet.Cells["A:F"].AutoFitColumns();
                        //Byte[] bin = package.GetAsByteArray();
                        System.IO.File.WriteAllBytes(Server.MapPath("~/Templates/" + nameFile), package.GetAsByteArray());

                        return Json(nameFile, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                return Json("FAIL!", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult DownloadFile(string file)
        {
            try
            {
                string fullName = Server.MapPath("~/Templates/" + file);

                System.IO.FileStream fileStream = System.IO.File.OpenRead(fullName);
                byte[] data = new byte[fileStream.Length];
                int br = fileStream.Read(data, 0, data.Length);
               

                if (br != fileStream.Length)
                    throw new System.IO.IOException(fullName);
                fileStream.Close();

                System.IO.File.Delete(fullName);

                return File(
                    data, System.Net.Mime.MediaTypeNames.Application.Octet, file);
            }
            catch (Exception)
            {
                return View();
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