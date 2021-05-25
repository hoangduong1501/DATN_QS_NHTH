using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_DATN_QS.Models.DB_Models;

namespace UI_DATN_QS.Models.Sessions
{
    public class SessionHelper
    {
        /* Session Thi */
        #region
        public static void Set_SessionThi()
        {
            HttpContext.Current.Session["THI_SESSION"] = "991501";
        }

        public static void Remove_SessionThi()
        {
            HttpContext.Current.Session.Remove("THI_SESSION");
        }

        public static string Get_SessionThi()
        {
            var session = HttpContext.Current.Session["THI_SESSION"];
            if (session == null) return "SIN";
            else return session.ToString();
        }
        #endregion

        /* Session Thi */
        #region
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
        #endregion

        /* Session Nguoi Dung */
        #region
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
        #endregion

        /* Session Giang Vien */
        #region
        public static void Set_SessionGV(UserSession_Model session)
        {
            HttpContext.Current.Session["USER_SESSIONGV"] = session;
        }

        public static void Remove_SessionGV()
        {
            HttpContext.Current.Session.Remove("USER_SESSIONGV");
        }

        public static UserSession_Model Get_SessionGV()
        {
            var session = HttpContext.Current.Session["USER_SESSIONGV"];
            if (session == null) return null;
            else return session as UserSession_Model;
        }
        #endregion
    }
}