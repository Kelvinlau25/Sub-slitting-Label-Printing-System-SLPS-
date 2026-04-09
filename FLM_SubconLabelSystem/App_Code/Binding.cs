using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Binder = Library.Root.Objects.Binder;

namespace Control
{
    /// <summary>
    /// Component Binding 
    /// ----------------------------------------------
    /// C.C.Yeon    16 April 2012   initial version
    /// </summary>
    public class Binding
    {
        public static void BindDropDownListResource(DropDownList ddl, string resourceName, string text = "", string value = "")
        {
            var resource = HttpContext.GetGlobalResourceObject("SearchSource", resourceName);
            ddl.DataSource = Library.Root.Control.Convertion<Binder>.Deserializer(resource != null ? resource.ToString() : string.Empty);
            ddl.DataTextField = "Text";
            ddl.DataValueField = "Value";
            ddl.DataBind();
            AddList(ddl, text, value);
        }

        public static void BindDropDownList(DropDownList ddl, List<Binder> list, string text = "", string value = "")
        {
            if (list.Count > 0)
            {
                ddl.DataSource = list;
                ddl.DataTextField = "Text";
                ddl.DataValueField = "Value";
                ddl.DataBind();
            }
            AddList(ddl, text, value);
        }

        private static void AddList(DropDownList ddl, string text, string value)
        {
            if (value != string.Empty)
            {
                ddl.Items.Insert(0, new ListItem(text, value));
            }
        }
    }
}