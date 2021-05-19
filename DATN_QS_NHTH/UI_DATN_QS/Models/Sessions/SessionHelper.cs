using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_DATN_QS.Models.DB_Models;

namespace UI_DATN_QS.Models.Sessions
{
    public class SessionHelper
    {
        public static void Set_SessionHV(UserSession_Model session)
        {
            HttpContext.Current.Session["USER_SESSIONHV"] = session;
        }

        public static void Remove_SessionHV()
        {
            HttpContext.Current.Session.Remove("USER_SESSIONHV");
        }

        public static UserSession_Model Get_SessionHV()
        {
            var session = HttpContext.Current.Session["USER_SESSIONHV"];
            if (session == null) return null;
            else return session as UserSession_Model;
        }

        public static void Set_SessionND(UserSession_Model session)
        {
            HttpContext.Current.Session["USER_SESSIONND"] = session;
        }

        public static void Remove_SessionND()
        {
            HttpContext.Current.Session.Remove("USER_SESSIONND");
        }

        public static UserSession_Model Get_SessionND()
        {
            var session = HttpContext.Current.Session["USER_SESSIONND"];
            if (session == null) return null;
            else return session as UserSession_Model;
        }
    }
}