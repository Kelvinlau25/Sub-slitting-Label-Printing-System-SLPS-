using System;
using System.Web;
using System.Web.UI;

namespace Library.Root.Control
{
    public class MessageCenter
    {
        public static void ShowAJAXMessageBox(Page msg_page, string ajax_msg)
        {
            if (ajax_msg == null)
            {
                ajax_msg = "";
            }
            ajax_msg = HttpContext.Current.Server.HtmlEncode(ajax_msg.Replace("'", "\""));

            string Msg = string.Format("alert('" + ajax_msg.Replace("||", "\\n") + "');");
            ScriptManager.RegisterStartupScript(msg_page, msg_page.GetType(), "Msg", Msg, true);
        }

        public static void ShowJqueryMessageBox(Page currentpage, string Str)
        {
            Str = HttpContext.Current.Server.HtmlEncode(Str.Replace("'", "\""));
            string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
            string message = string.Format(prompt, Str);
            currentpage.ClientScript.RegisterStartupScript(typeof(Page), "alert", message);
        }
    }
}
