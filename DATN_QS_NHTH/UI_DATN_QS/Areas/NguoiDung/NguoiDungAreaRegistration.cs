using System.Web.Mvc;

namespace UI_DATN_QS.Areas.NguoiDung
{
    public class NguoiDungAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NguoiDung";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NguoiDung_default",
                "NguoiDung/{controller}/{action}/{id}",
                new { action = "GET_HocVien", id = UrlParameter.Optional }
            );
        }
    }
}