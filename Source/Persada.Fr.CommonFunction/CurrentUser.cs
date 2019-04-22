using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using System;
using Persada.Fr.ModelView;
using System.Collections.Generic;

namespace Persada.Fr.CommonFunction
{
   public class CurrentUser : AuthorizeAttribute
    {
        public static int GetCurrentUserIdId()
        {
            if (HttpContext.Current.Session["USER_ID_ID"] != null)
            {
                return (int)HttpContext.Current.Session["USER_ID_ID"];
            }
            else
            {
                return 0;
            }
        }

        public static string GetCurrentUserId()
        {
            if (HttpContext.Current.Session["USER_ID"] != null)
            {
                return HttpContext.Current.Session["USER_ID"].ToString();
            }
            else
            {
                return "";
            }            
        }

        public static bool IsSa()
        {
            if (HttpContext.Current.Session["IS_SUPER_ADMIN"] != null)
            {
                return (bool)HttpContext.Current.Session["IS_SUPER_ADMIN"];
            }
            else
            {
                return false;
            }
        }

        public static string GetCurrentEmail()
        {
            if (HttpContext.Current.Session["USER_EMAIL"] != null)
            {
                return HttpContext.Current.Session["USER_EMAIL"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string GetCurrentName()
        {
            if (HttpContext.Current.Session["USER_NAME"] != null)
            {
                return HttpContext.Current.Session["USER_NAME"].ToString();
            }
            else
            {
                return "";
            }
        }
        
        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public static string GetCurrentAppMap()
        {
            return HttpContext.Current.Server.MapPath("~/Attachments");
        }

        public static List<TOURIS_TV_SOSMED> GetSosmedList()
        {
            List<TOURIS_TV_SOSMED> sosmedList = new List<TOURIS_TV_SOSMED>();
            if (HttpContext.Current.Session["GetSosmedList"] != null)
            {
                sosmedList = (List<TOURIS_TV_SOSMED>)HttpContext.Current.Session["GetSosmedList"];
            }

           return sosmedList;
        }
    }
}
