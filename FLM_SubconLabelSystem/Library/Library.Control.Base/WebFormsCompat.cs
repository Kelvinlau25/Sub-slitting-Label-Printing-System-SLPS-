using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

// Compatibility stubs for System.Web.UI and System.Web.UI.WebControls
// These minimal implementations allow code originally written for ASP.NET Web Forms
// to compile against .NET 8 without the System.Web assemblies.

namespace System.Web.UI
{
    public class Control
    {
        public string ID { get; set; }
        public bool Visible { get; set; } = true;
        public bool EnableViewState { get; set; } = true;
        public Control NamingContainer { get; set; }
        public ControlCollection Controls { get; }

        public Control()
        {
            Controls = new ControlCollection(this);
        }

        public virtual Control FindControl(string id)
        {
            foreach (var c in Controls)
            {
                if (c is Control ctrl && ctrl.ID == id)
                    return ctrl;
            }
            return null;
        }
    }

    public class ControlCollection : IEnumerable
    {
        private readonly List<object> _controls = new List<object>();
        private readonly Control _owner;

        public ControlCollection(Control owner) { _owner = owner; }

        public void Add(Control child)
        {
            if (child != null) child.NamingContainer = _owner;
            _controls.Add(child);
        }

        public int Count => _controls.Count;

        public IEnumerator GetEnumerator() => _controls.GetEnumerator();
    }

    public interface ITemplate
    {
        void InstantiateIn(Control container);
    }

    public class Page : Control
    {
        public string Title { get; set; }
        public bool IsPostBack { get; set; }
        public virtual HttpSessionStateBase Session => _session;
        public virtual HttpRequestBase Request => _request;
        public virtual HttpResponseBase Response => _response;
        public virtual HttpServerUtilityBase Server => _server;
        public IDictionary ViewState { get; } = new Hashtable();
        public ClientScriptManager ClientScript { get; } = new ClientScriptManager();

        private HttpSessionStateBase _session = new StubSessionState();
        private HttpRequestBase _request = new StubHttpRequest();
        private HttpResponseBase _response = new StubHttpResponse();
        private HttpServerUtilityBase _server = new StubHttpServerUtility();

        protected virtual void OnInit(EventArgs e) { }
        protected virtual void OnLoad(EventArgs e) { }

        public string ResolveUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl)) return relativeUrl;
            if (relativeUrl.StartsWith("~/"))
                return "/" + relativeUrl.Substring(2);
            return relativeUrl;
        }
    }

    public class UserControl : Control
    {
    }

    public class ScriptManager
    {
        public static void RegisterStartupScript(Page page, Type type, string key, string script, bool addScriptTags)
        {
        }

        public static void RegisterStartupScript(Control control, Type type, string key, string script, bool addScriptTags)
        {
        }
    }

    public class ClientScriptManager
    {
        public void RegisterStartupScript(Type type, string key, string script)
        {
        }
    }

    // Stub HTTP abstractions used by Page
    public class HttpSessionStateBase
    {
        private readonly Dictionary<string, object> _store = new Dictionary<string, object>();
        public virtual object this[string name]
        {
            get => _store.TryGetValue(name, out var v) ? v : null;
            set => _store[name] = value;
        }
    }

    public class HttpRequestBase
    {
        public virtual NameValueCollection QueryString { get; } = new NameValueCollection();
        public virtual string RawUrl { get; set; } = string.Empty;
        public virtual string UserHostAddress { get; set; } = string.Empty;
    }

    public class HttpResponseBase
    {
        public virtual void Redirect(string url) { }
    }

    public class HttpServerUtilityBase
    {
        public virtual string UrlEncode(string s) => Uri.EscapeDataString(s ?? string.Empty);
        public virtual string UrlDecode(string s) => Uri.UnescapeDataString(s ?? string.Empty);
        public virtual string HtmlEncode(string s) => System.Net.WebUtility.HtmlEncode(s);
    }

    internal class StubSessionState : HttpSessionStateBase { }
    internal class StubHttpRequest : HttpRequestBase { }
    internal class StubHttpResponse : HttpResponseBase { }
    internal class StubHttpServerUtility : HttpServerUtilityBase { }
}

namespace System.Web.UI.WebControls
{
    public enum ListItemType
    {
        Header = 0,
        Item = 1,
        AlternatingItem = 2,
        SelectedItem = 3,
        EditItem = 4,
        Separator = 5,
        Footer = 6
    }

    public enum DataControlRowType
    {
        Header = 0,
        Footer = 1,
        DataRow = 2,
        Separator = 3,
        Pager = 4,
        EmptyDataRow = 5
    }

    public enum HorizontalAlign
    {
        NotSet = 0,
        Left = 1,
        Center = 2,
        Right = 3,
        Justify = 4
    }

    public class Style
    {
        public HorizontalAlign HorizontalAlign { get; set; }
        public int Width { get; set; }
        public string CssClass { get; set; }
    }

    public class PlaceHolder : System.Web.UI.Control
    {
    }

    public class WebControl : System.Web.UI.Control
    {
        public string Text { get; set; }
        public Style ControlStyle { get; } = new Style();
    }

    public class Label : WebControl
    {
    }

    public class Literal : WebControl
    {
    }

    public class CheckBox : WebControl
    {
        public bool Checked { get; set; }
        public bool AutoPostBack { get; set; }
        public event EventHandler CheckedChanged;
        public void OnCheckedChanged(EventArgs e) => CheckedChanged?.Invoke(this, e);
    }

    public class RadioButton : WebControl
    {
        public bool Checked { get; set; }
        public bool AutoPostBack { get; set; }
        public event EventHandler CheckedChanged;
        public void OnCheckedChanged(EventArgs e) => CheckedChanged?.Invoke(this, e);
    }

    public class DataGrid : WebControl
    {
        public object DataSource { get; set; }
        public void DataBind() { }
        public void RenderControl(System.IO.TextWriter writer) { }
    }

    public class DataKey
    {
        private readonly object[] _values;
        public DataKey(object[] values) { _values = values ?? Array.Empty<object>(); }
        public object this[int index] => index < _values.Length ? _values[index] : null;
        public object Value => _values.Length > 0 ? _values[0] : null;
    }

    public class DataKeyArray : IEnumerable
    {
        private readonly List<DataKey> _keys = new List<DataKey>();
        public DataKey this[int index] => _keys[index];
        public int Count => _keys.Count;
        public void Add(DataKey key) => _keys.Add(key);
        public IEnumerator GetEnumerator() => _keys.GetEnumerator();
    }

    public class TableCellCollection : List<TableCell>
    {
    }

    public class TableCell
    {
        public string Text { get; set; }
    }

    public class AttributeCollection
    {
        private readonly Dictionary<string, string> _attrs = new Dictionary<string, string>();
        public void Add(string key, string value) => _attrs[key] = value;
        public string this[string key]
        {
            get => _attrs.TryGetValue(key, out var v) ? v : null;
            set => _attrs[key] = value;
        }
    }

    public class GridViewRow : System.Web.UI.Control
    {
        public DataControlRowType RowType { get; set; }
        public int RowIndex { get; set; }
        public TableCellCollection Cells { get; } = new TableCellCollection();
        public AttributeCollection Attributes { get; } = new AttributeCollection();

        public override System.Web.UI.Control FindControl(string id)
        {
            return base.FindControl(id);
        }
    }

    public class GridViewRowCollection : IEnumerable<GridViewRow>
    {
        private readonly List<GridViewRow> _rows = new List<GridViewRow>();
        public GridViewRow this[int index] => _rows[index];
        public int Count => _rows.Count;
        public void Add(GridViewRow row) => _rows.Add(row);
        public IEnumerator<GridViewRow> GetEnumerator() => _rows.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _rows.GetEnumerator();
    }

    public class GridViewSortEventArgs : EventArgs
    {
        public string SortExpression { get; set; }
        public GridViewSortEventArgs(string sortExpression) { SortExpression = sortExpression; }
    }

    public class GridViewRowEventArgs : EventArgs
    {
        public GridViewRow Row { get; set; }
        public GridViewRowEventArgs(GridViewRow row) { Row = row; }
    }

    public class DataControlFieldCollection : List<DataControlField>
    {
        public void Insert(int index, DataControlField field)
        {
            base.Insert(index, field);
        }
    }

    public class DataControlField
    {
        public System.Web.UI.ITemplate ItemTemplate { get; set; }
        public System.Web.UI.ITemplate HeaderTemplate { get; set; }
        public Style ItemStyle { get; } = new Style();
        public Style HeaderStyle { get; } = new Style();
        public Style ControlStyle { get; } = new Style();
    }

    public class TemplateField : DataControlField
    {
    }

    public class GridView : WebControl
    {
        public DataKeyArray DataKeys { get; } = new DataKeyArray();
        public GridViewRowCollection Rows { get; } = new GridViewRowCollection();
        public DataControlFieldCollection Columns { get; } = new DataControlFieldCollection();
        public object DataSource { get; set; }

        public event EventHandler<GridViewSortEventArgs> Sorting;
        public event EventHandler<GridViewRowEventArgs> RowCreated;
        public event EventHandler<GridViewRowEventArgs> RowDataBound;

        public void DataBind() { }

        public void OnSorting(GridViewSortEventArgs e) => Sorting?.Invoke(this, e);
        public void OnRowCreated(GridViewRowEventArgs e) => RowCreated?.Invoke(this, e);
        public void OnRowDataBound(GridViewRowEventArgs e) => RowDataBound?.Invoke(this, e);
    }
}
