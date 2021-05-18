using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_DATN_QS.Models.DB_Models;

namespace UI_DATN_QS.Models.Sessions
{
    public class SessionHelper
    {
        public static void Set_Session(UserSession_Model session)
        {
            HttpContext.Current.Session["USER_SESSION"] = session;
        }

        public static void Remove_Session()
        {
            HttpContext.Current.Session.Remove("USER_SESSION");
        }

        public static UserSession_Model Get_Session()
        {
            var session = HttpContext.Current.Session["USER_SESSION"];
            if (session == null) return null;
            else return session as UserSession_Model;
        }
    }
}