using OfficeOpenXml;
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

        [HttpGet]
        public ActionResult DownloadFile()
        {
            UserSession_Model user_Session = SessionHelper.Get_SessionHV();
            if (user_Session == null) return RedirectToAction("Dang_Nhap", "DangNhap", new { area = "" });
            ViewBag.USER = user_Session;

            //string nameFile = StringRandom.GeneratePassword();
            try
            {
                Byte[] data;
                using (ExcelPackage package = new ExcelPackage())
                {
                    using (DB_DATN_QSEntities entities = new DB_DATN_QSEntities())
                    {
                        HOC_VIEN hocvien = entities.HOC_VIEN.Where(p => p.ID_TaiKhoan == user_Session.ID_TaiKhoan).FirstOrDefault();

                        var lst = entities.BANG_DIEM.Where(p => p.IS_Deleted == 0 && p.ID_HocVien == hocvien.ID_HocVien).ToList().
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

                                    }).ToList();


                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Bảng Điểm");

                        worksheet.Cells["A2:B2"].Merge = worksheet.Cells["A3:B3"].Merge = worksheet.Cells["A4:B4"].Merge = true;
                        worksheet.Cells["A2:B2"].Style.Font.Bold = worksheet.Cells["A3:B3"].Style.Font.Bold = worksheet.Cells["A4:B4"].Style.Font.Bold = worksheet.Cells["A6:F6"].Style.Font.Bold = true;
                        worksheet.Cells["A6:F6"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells["A6:F6"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                        worksheet.Cells["A6:F6"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells["D:E"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        worksheet.Cells["A2"].Value = "Mã học viên: " + hocvien.MA_HocVien;
                        worksheet.Cells["A3"].Value = "Tên học viên: " + hocvien.TEN_HocVien;
                        worksheet.Cells["A4"].Value = "Ngày xuất: " + DateTime.Today.ToString("dd/MM/yyyy");

                        worksheet.Cells["A6"].Value = "Mã đề thi";
                        worksheet.Cells["B6"].Value = "Tên tên môn học";
                        worksheet.Cells["C6"].Value = "Loại bài thi";
                        worksheet.Cells["D6"].Value = "Số câu đúng";
                        worksheet.Cells["E6"].Value = "Điểm";
                        worksheet.Cells["F6"].Value = "Ngày thi";
                        int row = 7;

                        if (lst.Count() > 0)
                        {
                            foreach (var item in lst)
                            {
                                worksheet.Cells[String.Format("A{0}", row)].Value = item.MA_DeThi;
                                worksheet.Cells[String.Format("B{0}", row)].Value = item.TEN_MonHoc;
                                worksheet.Cells[String.Format("C{0}", row)].Value = item.TEN_LBaiThi;
                                worksheet.Cells[String.Format("D{0}", row)].Value = item.DUNG_BangDiem;
                                worksheet.Cells[String.Format("E{0}", row)].Value = item.DIEM_BangDiem;
                                worksheet.Cells[String.Format("F{0}", row)].Value = item.TIME_Create.Value.ToString("dd/MM/yyyy");
                                row++;
                            }
                        }

                        worksheet.Cells["A:F"].AutoFitColumns();
                        data = package.GetAsByteArray();

                    }
                }

                return File(
                    data, System.Net.Mime.MediaTypeNames.Application.Octet, "BangDiem" + DateTime.Now.ToString() + ".xlsx");
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}