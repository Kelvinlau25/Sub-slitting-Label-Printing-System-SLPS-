using System;
using System.Collections.Generic;
using System.Net;

namespace Library.Root.Control
{
    public class MessageCenter
    {
        [ThreadStatic]
        private static List<string> _pendingScripts;

        public static List<string> PendingScripts
        {
            get
            {
                if (_pendingScripts == null)
                    _pendingScripts = new List<string>();
                return _pendingScripts;
            }
        }

        public static void ShowAJAXMessageBox(object msg_page, string ajax_msg)
        {
            if (ajax_msg == null)
            {
                ajax_msg = "";
            }
            ajax_msg = WebUtility.HtmlEncode(ajax_msg.Replace("'", "\""));

            string Msg = string.Format("alert('" + ajax_msg.Replace("||", "\\n") + "');");
            PendingScripts.Add(Msg);
        }

        public static void ShowJqueryMessageBox(object currentpage, string Str)
        {
            Str = WebUtility.HtmlEncode(Str.Replace("'", "\""));
            string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
            string message = string.Format(prompt, Str);
            PendingScripts.Add(message);
        }
    }
}
