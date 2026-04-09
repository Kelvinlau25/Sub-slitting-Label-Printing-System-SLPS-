using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;

// Compatibility stubs for System.Web.UI, System.Web.UI.WebControls,
// System.Web.UI.HtmlControls, and System.Web.Services.
// These minimal implementations allow code originally written for ASP.NET Web Forms
// to compile against .NET 8 without the System.Web assemblies.

// ---------------------------------------------------------------------------
//  System.Web - HttpContext and HttpUtility are provided by
//  Microsoft.AspNetCore.SystemWebAdapters; no stubs needed here.
// ---------------------------------------------------------------------------

// ---------------------------------------------------------------------------
//  System.Web.UI
// ---------------------------------------------------------------------------
namespace System.Web.UI
{
    public class Control
    {
        public string ID { get; set; }
        public bool Visible { get; set; } = true;
        public bool EnableViewState { get; set; } = true;
        public bool Enabled { get; set; } = true;
        public Control NamingContainer { get; set; }
        public Control Parent { get; set; }
        public ControlCollection Controls { get; }
        public Page Page { get; set; }

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
            if (child != null) { child.NamingContainer = _owner; child.Parent = _owner; }
            _controls.Add(child);
        }

        public void AddAt(int index, Control child)
        {
            if (child != null) { child.NamingContainer = _owner; child.Parent = _owner; }
            _controls.Insert(index, child);
        }

        public int Count => _controls.Count;

        public Control this[int index] => _controls[index] as Control;

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
        public bool IsValid { get; set; } = true;
        public virtual HttpSessionStateBase Session => _session;
        public virtual HttpRequestBase Request => _request;
        public virtual HttpResponseBase Response => _response;
        public virtual HttpServerUtilityBase Server => _server;
        public IDictionary ViewState { get; } = new Hashtable();
        public ClientScriptManager ClientScript { get; } = new ClientScriptManager();
        public Control Form { get; set; } = new Control();

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

        public object GetGlobalResourceObject(string classKey, string resourceKey)
        {
            return null;
        }
    }

    public class MasterPage : Page
    {
    }

    public class UserControl : Control
    {
        public bool IsPostBack { get; set; }
        public virtual HttpSessionStateBase Session => _session;
        public virtual HttpRequestBase Request => _request;
        public virtual HttpResponseBase Response => _response;
        public virtual HttpServerUtilityBase Server => _server;
        public ClientScriptManager ClientScript { get; } = new ClientScriptManager();

        private HttpSessionStateBase _session = new StubSessionState();
        private HttpRequestBase _request = new StubHttpRequest();
        private HttpResponseBase _response = new StubHttpResponse();
        private HttpServerUtilityBase _server = new StubHttpServerUtility();

        public string ResolveUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl)) return relativeUrl;
            if (relativeUrl.StartsWith("~/"))
                return "/" + relativeUrl.Substring(2);
            return relativeUrl;
        }

        public object GetGlobalResourceObject(string classKey, string resourceKey)
        {
            return null;
        }
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

    public class ScriptManagerProxy : Control
    {
    }

    public class UpdateProgress : Control
    {
        public string AssociatedUpdatePanelID { get; set; }
        public ITemplate ProgressTemplate { get; set; }
    }

    public class ClientScriptManager
    {
        public void RegisterStartupScript(Type type, string key, string script)
        {
        }

        public void RegisterStartupScript(Type type, string key, string script, bool addScriptTags)
        {
        }

        public void RegisterClientScriptBlock(Type type, string key, string script)
        {
        }

        public void RegisterClientScriptBlock(Type type, string key, string script, bool addScriptTags)
        {
        }

        public string GetPostBackClientHyperlink(Control control, string argument)
        {
            return string.Empty;
        }
    }

    public enum HtmlTextWriterTag
    {
        Unknown, A, Div, Span, Table, Tr, Td, Th, P, Br, Hr, Img, Input, Form, Select, Option, Textarea, Button, Label, Li, Ul, Ol, H1, H2, H3, H4, H5, H6, Script, Style, Link, Meta, Head, Body, Html
    }

    public class HtmlTextWriter : TextWriter
    {
        private readonly TextWriter _writer;
        public HtmlTextWriter(TextWriter writer) { _writer = writer; }
        public override System.Text.Encoding Encoding => _writer.Encoding;
        public override void Write(string value) => _writer.Write(value);
        public override void Write(char value) => _writer.Write(value);
        public void RenderBeginTag(HtmlTextWriterTag tag) { }
        public void RenderBeginTag(string tag) { }
        public void RenderEndTag() { }
        public void AddAttribute(string name, string value) { }
        public void AddStyleAttribute(string name, string value) { }
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

        public void Add(string name, object value) => _store[name] = value;
        public void Remove(string name) => _store.Remove(name);
        public void Clear() => _store.Clear();
        public void Abandon() => _store.Clear();
    }

    public class HttpRequestBase
    {
        public virtual NameValueCollection QueryString { get; } = new NameValueCollection();
        public virtual string RawUrl { get; set; } = string.Empty;
        public virtual string UserHostAddress { get; set; } = string.Empty;

        public virtual string this[string key] => QueryString[key];

        public HttpCookieCollection Cookies { get; } = new HttpCookieCollection();
    }

    public class HttpCookieCollection
    {
        private readonly Dictionary<string, HttpCookie> _cookies = new Dictionary<string, HttpCookie>();
        public HttpCookie this[string name]
        {
            get => _cookies.TryGetValue(name, out var v) ? v : null;
            set => _cookies[name] = value;
        }
        public void Add(HttpCookie cookie) => _cookies[cookie.Name] = cookie;
    }

    public class HttpCookie
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public HttpCookie(string name) { Name = name; }
        public HttpCookie(string name, string value) { Name = name; Value = value; }
    }

    public class HttpResponseBase
    {
        public virtual void Redirect(string url) { }
        public virtual void Redirect(string url, bool endResponse) { }
        public virtual void Write(string s) { }
        public virtual void Clear() { }
        public virtual void ClearContent() { }
        public virtual void End() { }
        public virtual void Flush() { }
        public virtual void AddHeader(string name, string value) { }
        public string ContentType { get; set; }
        public string Charset { get; set; }
        public bool Buffer { get; set; }
        public TextWriter Output { get; set; } = TextWriter.Null;
        public Stream OutputStream { get; set; } = Stream.Null;
        public HttpCachePolicy Cache { get; } = new HttpCachePolicy();
    }

    public class HttpCachePolicy
    {
        public void SetExpires(DateTime date) { }
        public void SetCacheability(System.Web.HttpCacheability cacheability) { }
        public void SetNoStore() { }
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

// ---------------------------------------------------------------------------
//  System.Web.UI.HtmlControls
// ---------------------------------------------------------------------------
namespace System.Web.UI.HtmlControls
{
    public class HtmlControl : System.Web.UI.Control
    {
        public System.Web.UI.WebControls.AttributeCollection Attributes { get; } = new System.Web.UI.WebControls.AttributeCollection();
        public System.Web.UI.WebControls.CssStyleCollection Style { get; } = new System.Web.UI.WebControls.CssStyleCollection();
        public string TagName { get; set; }
    }

    public class HtmlContainerControl : HtmlControl
    {
        public string InnerHtml { get; set; }
        public string InnerText { get; set; }
    }

    public class HtmlForm : HtmlContainerControl
    {
    }

    public class HtmlHead : HtmlContainerControl
    {
        public string Title { get; set; }
    }

    public class HtmlGenericControl : HtmlContainerControl
    {
        public HtmlGenericControl() { }
        public HtmlGenericControl(string tag) { TagName = tag; }
    }

    public class HtmlAnchor : HtmlContainerControl
    {
        public string HRef { get; set; }
        public string Target { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
    }

    public class HtmlTable : HtmlContainerControl
    {
    }

    public class HtmlTableRow : HtmlContainerControl
    {
    }

    public class HtmlTableCell : HtmlContainerControl
    {
    }

    public class HtmlInputControl : HtmlControl
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
    }

    public class HtmlInputHidden : HtmlInputControl
    {
        public HtmlInputHidden() { Type = "hidden"; }
    }

    public class HtmlInputText : HtmlInputControl
    {
        public HtmlInputText() { Type = "text"; }
        public int MaxLength { get; set; }
        public int Size { get; set; }
    }

    public class HtmlInputButton : HtmlInputControl
    {
        public HtmlInputButton() { Type = "button"; }
        public HtmlInputButton(string type) { Type = type; }
        public event EventHandler ServerClick;
        public void OnServerClick(EventArgs e) => ServerClick?.Invoke(this, e);
    }
}

// ---------------------------------------------------------------------------
//  System.Web.UI.WebControls
// ---------------------------------------------------------------------------
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

    public enum DataControlRowState
    {
        Normal = 0,
        Alternate = 1,
        Selected = 2,
        Edit = 4,
        Insert = 8
    }

    public enum HorizontalAlign
    {
        NotSet = 0,
        Left = 1,
        Center = 2,
        Right = 3,
        Justify = 4
    }

    public enum TextBoxMode
    {
        SingleLine = 0,
        MultiLine = 1,
        Password = 2
    }

    public enum ValidatorDisplay
    {
        None = 0,
        Static = 1,
        Dynamic = 2
    }

    public enum ValidationDataType
    {
        String = 0,
        Integer = 1,
        Double = 2,
        Date = 3,
        Currency = 4
    }

    public enum ValidationCompareOperator
    {
        Equal = 0,
        NotEqual = 1,
        GreaterThan = 2,
        GreaterThanEqual = 3,
        LessThan = 4,
        LessThanEqual = 5,
        DataTypeCheck = 6
    }

    public class Unit
    {
        public double Value { get; }
        public Unit(double value) { Value = value; }
        public Unit(string value) { double.TryParse(value, out var v); Value = v; }
        public static implicit operator Unit(int value) => new Unit(value);
        public static implicit operator Unit(double value) => new Unit(value);
        public override string ToString() => Value.ToString();
    }

    public class CssStyleCollection
    {
        private readonly Dictionary<string, string> _styles = new Dictionary<string, string>();
        public void Add(string key, string value) => _styles[key] = value;
        public string this[string key]
        {
            get => _styles.TryGetValue(key, out var v) ? v : null;
            set => _styles[key] = value;
        }
        public void Remove(string key) => _styles.Remove(key);
    }

    public class Style
    {
        public HorizontalAlign HorizontalAlign { get; set; }
        public int Width { get; set; }
        public string CssClass { get; set; }
        public void Add(string key, string value) { }
    }

    public class PlaceHolder : System.Web.UI.Control
    {
    }

    public class WebControl : System.Web.UI.Control
    {
        public virtual string Text { get; set; }
        public bool Enabled { get; set; } = true;
        public string CssClass { get; set; }
        public Unit Width { get; set; }
        public Unit Height { get; set; }
        public string ToolTip { get; set; }
        public AttributeCollection Attributes { get; } = new AttributeCollection();
        public Style ControlStyle { get; } = new Style();
        public CssStyleCollection Style { get; } = new CssStyleCollection();
        public string AccessKey { get; set; }
        public short TabIndex { get; set; }

        public void Focus() { }
    }

    public class Label : WebControl
    {
        public string AssociatedControlID { get; set; }
    }

    public class Image : WebControl
    {
        public string ImageUrl { get; set; }
        public string AlternateText { get; set; }
    }

    public class Literal : WebControl
    {
    }

    public class Panel : WebControl
    {
        public string GroupingText { get; set; }
        public string DefaultButton { get; set; }
        public ScrollBars ScrollBars { get; set; }
    }

    public enum ScrollBars
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
        Both = 3,
        Auto = 4
    }

    public class UpdatePanel : WebControl
    {
        public string UpdateMode { get; set; }
        public ITemplate ContentTemplateContainer { get; set; }
    }

    // ListItem and ListItemCollection for DropDownList/ListBox
    public class ListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }

        public ListItem() { }
        public ListItem(string text) { Text = text; Value = text; }
        public ListItem(string text, string value) { Text = text; Value = value; }

        public override string ToString() => Text ?? string.Empty;
    }

    public class ListItemCollection : IEnumerable<ListItem>
    {
        private readonly List<ListItem> _items = new List<ListItem>();

        public int Count => _items.Count;
        public ListItem this[int index] => _items[index];

        public void Add(ListItem item) => _items.Add(item);
        public void Add(string item) => _items.Add(new ListItem(item));
        public void Insert(int index, ListItem item) => _items.Insert(index, item);
        public void Insert(int index, string item) => _items.Insert(index, new ListItem(item));
        public void Remove(ListItem item) => _items.Remove(item);
        public void RemoveAt(int index) => _items.RemoveAt(index);
        public void Clear() => _items.Clear();
        public bool Contains(ListItem item) => _items.Contains(item);
        public int IndexOf(ListItem item) => _items.IndexOf(item);

        public ListItem FindByText(string text) => _items.Find(i => i.Text == text);
        public ListItem FindByValue(string value) => _items.Find(i => i.Value == value);

        public IEnumerator<ListItem> GetEnumerator() => _items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
    }

    public class ListControl : WebControl
    {
        public ListItemCollection Items { get; } = new ListItemCollection();

        public virtual string SelectedValue
        {
            get
            {
                foreach (var item in Items)
                    if (item.Selected) return item.Value;
                return Items.Count > 0 ? Items[0].Value : string.Empty;
            }
            set
            {
                foreach (var item in Items) item.Selected = false;
                foreach (var item in Items)
                    if (item.Value == value) { item.Selected = true; break; }
            }
        }

        public virtual int SelectedIndex
        {
            get
            {
                for (int i = 0; i < Items.Count; i++)
                    if (Items[i].Selected) return i;
                return Items.Count > 0 ? 0 : -1;
            }
            set
            {
                for (int i = 0; i < Items.Count; i++) Items[i].Selected = false;
                if (value >= 0 && value < Items.Count) Items[value].Selected = true;
            }
        }

        public virtual ListItem SelectedItem
        {
            get
            {
                foreach (var item in Items)
                    if (item.Selected) return item;
                return Items.Count > 0 ? Items[0] : null;
            }
        }

        public object DataSource { get; set; }
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }
        public string DataMember { get; set; }

        public bool AutoPostBack { get; set; }
        public event EventHandler SelectedIndexChanged;
        public void OnSelectedIndexChanged(EventArgs e) => SelectedIndexChanged?.Invoke(this, e);

        public void DataBind() { }
    }

    public class DropDownList : ListControl
    {
    }

    public class ListBox : ListControl
    {
        public int Rows { get; set; }
        public ListSelectionMode SelectionMode { get; set; }
    }

    public enum ListSelectionMode
    {
        Single = 0,
        Multiple = 1
    }

    public class TextBox : WebControl
    {
        public TextBoxMode TextMode { get; set; }
        public int MaxLength { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public bool ReadOnly { get; set; }
        public bool AutoPostBack { get; set; }
        public string ValidationGroup { get; set; }
        public bool CausesValidation { get; set; }
        public event EventHandler TextChanged;
        public void OnTextChanged(EventArgs e) => TextChanged?.Invoke(this, e);
    }

    public class Button : WebControl
    {
        public string CommandName { get; set; }
        public string CommandArgument { get; set; }
        public string OnClientClick { get; set; }
        public bool CausesValidation { get; set; } = true;
        public string ValidationGroup { get; set; }
        public string PostBackUrl { get; set; }
        public bool UseSubmitBehavior { get; set; } = true;
        public event EventHandler Click;
        public event EventHandler Command;
        public void OnClick(EventArgs e) => Click?.Invoke(this, e);
        public void OnCommand(EventArgs e) => Command?.Invoke(this, e);
    }

    public class LinkButton : WebControl
    {
        public string CommandName { get; set; }
        public string CommandArgument { get; set; }
        public string OnClientClick { get; set; }
        public bool CausesValidation { get; set; } = true;
        public string ValidationGroup { get; set; }
        public string PostBackUrl { get; set; }
        public event EventHandler Click;
        public event EventHandler Command;
        public void OnClick(EventArgs e) => Click?.Invoke(this, e);
        public void OnCommand(EventArgs e) => Command?.Invoke(this, e);
    }

    public class ImageClickEventArgs : EventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ImageClickEventArgs(int x, int y) { X = x; Y = y; }
    }

    public class ImageButton : WebControl
    {
        public string ImageUrl { get; set; }
        public string AlternateText { get; set; }
        public string CommandName { get; set; }
        public string CommandArgument { get; set; }
        public string OnClientClick { get; set; }
        public bool CausesValidation { get; set; } = true;
        public string ValidationGroup { get; set; }
        public string PostBackUrl { get; set; }
        public event EventHandler<ImageClickEventArgs> Click;
        public event EventHandler Command;
        public void OnClick(ImageClickEventArgs e) => Click?.Invoke(this, e);
        public void OnCommand(EventArgs e) => Command?.Invoke(this, e);
    }

    public class HyperLink : WebControl
    {
        public string NavigateUrl { get; set; }
        public string Target { get; set; }
        public string ImageUrl { get; set; }
    }

    public class HiddenField : System.Web.UI.Control
    {
        public string Value { get; set; }
        public event EventHandler ValueChanged;
        public void OnValueChanged(EventArgs e) => ValueChanged?.Invoke(this, e);
    }

    public class CheckBox : WebControl
    {
        public bool Checked { get; set; }
        public bool AutoPostBack { get; set; }
        public string ValidationGroup { get; set; }
        public event EventHandler CheckedChanged;
        public void OnCheckedChanged(EventArgs e) => CheckedChanged?.Invoke(this, e);
    }

    public class RadioButton : CheckBox
    {
        public string GroupName { get; set; }
    }

    // Validators
    public class ServerValidateEventArgs : EventArgs
    {
        public string Value { get; set; }
        public bool IsValid { get; set; } = true;
        public ServerValidateEventArgs(string value, bool isValid) { Value = value; IsValid = isValid; }
    }

    public class BaseValidator : WebControl
    {
        public string ControlToValidate { get; set; }
        public string ErrorMessage { get; set; }
        public string ValidationGroup { get; set; }
        public ValidatorDisplay Display { get; set; }
        public bool IsValid { get; set; } = true;
        public bool SetFocusOnError { get; set; }
    }

    public class RequiredFieldValidator : BaseValidator
    {
        public string InitialValue { get; set; }
    }

    public class CompareValidator : BaseValidator
    {
        public string ControlToCompare { get; set; }
        public string ValueToCompare { get; set; }
        public ValidationCompareOperator Operator { get; set; }
        public ValidationDataType Type { get; set; }
    }

    public class RegularExpressionValidator : BaseValidator
    {
        public string ValidationExpression { get; set; }
    }

    public class CustomValidator : BaseValidator
    {
        public bool ValidateEmptyText { get; set; }
        public bool EnableClientScript { get; set; } = true;
        public event EventHandler<ServerValidateEventArgs> ServerValidate;
        public void OnServerValidate(ServerValidateEventArgs e) => ServerValidate?.Invoke(this, e);
    }

    public class ValidationSummary : WebControl
    {
        public string ValidationGroup { get; set; }
        public string HeaderText { get; set; }
        public bool ShowMessageBox { get; set; }
        public bool ShowSummary { get; set; } = true;
    }

    // DataGrid
    public class DataGrid : WebControl
    {
        public object DataSource { get; set; }
        public void DataBind() { }
        public void RenderControl(System.IO.TextWriter writer) { }
    }

    // DataKey
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

    // TableCell
    public class TableCellCollection : List<TableCell>
    {
    }

    public class TableCell : WebControl
    {
        public int ColumnSpan { get; set; }
        public int RowSpan { get; set; }
        public HorizontalAlign HorizontalAlign { get; set; }
    }

    public class TableHeaderCell : TableCell
    {
    }

    public class Table : WebControl
    {
        public TableRowCollection Rows { get; } = new TableRowCollection();
    }

    public class TableRow : WebControl
    {
        public TableCellCollection Cells { get; } = new TableCellCollection();
    }

    public class TableRowCollection : List<TableRow>
    {
    }

    // AttributeCollection
    public class AttributeCollection
    {
        private readonly Dictionary<string, string> _attrs = new Dictionary<string, string>();
        public void Add(string key, string value) => _attrs[key] = value;
        public void Remove(string key) => _attrs.Remove(key);
        public string this[string key]
        {
            get => _attrs.TryGetValue(key, out var v) ? v : null;
            set => _attrs[key] = value;
        }
        public int Count => _attrs.Count;
    }

    // GridView row and events
    public class GridViewRow : System.Web.UI.Control
    {
        public DataControlRowType RowType { get; set; }
        public DataControlRowState RowState { get; set; }
        public int RowIndex { get; set; }
        public int DataItemIndex { get; set; }
        public TableCellCollection Cells { get; } = new TableCellCollection();
        public AttributeCollection Attributes { get; } = new AttributeCollection();

        public GridViewRow() { }
        public GridViewRow(int rowIndex, int dataItemIndex, DataControlRowType rowType, DataControlRowState rowState)
        {
            RowIndex = rowIndex;
            DataItemIndex = dataItemIndex;
            RowType = rowType;
            RowState = rowState;
        }

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

    public class GridViewCommandEventArgs : EventArgs
    {
        public string CommandName { get; set; }
        public object CommandArgument { get; set; }
        public GridViewRow Row { get; set; }
        public GridViewCommandEventArgs(string commandName, object commandArgument)
        {
            CommandName = commandName;
            CommandArgument = commandArgument;
        }
    }

    public class GridViewDeleteEventArgs : System.ComponentModel.CancelEventArgs
    {
        public int RowIndex { get; set; }
        public GridViewDeleteEventArgs(int rowIndex) { RowIndex = rowIndex; }
    }

    public class GridViewPageEventArgs : EventArgs
    {
        public int NewPageIndex { get; set; }
        public GridViewPageEventArgs(int newPageIndex) { NewPageIndex = newPageIndex; }
    }

    public class GridViewSelectEventArgs : EventArgs
    {
        public int NewSelectedIndex { get; set; }
        public GridViewSelectEventArgs(int newSelectedIndex) { NewSelectedIndex = newSelectedIndex; }
    }

    public class DataControlFieldCollection : List<DataControlField>
    {
        public new void Insert(int index, DataControlField field)
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
        public string DataKeyNames { get; set; }
        public bool AllowPaging { get; set; }
        public bool AllowSorting { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageCount { get; set; }
        public GridViewRow HeaderRow { get; set; }
        public GridViewRow FooterRow { get; set; }
        public GridViewRow SelectedRow { get; set; }
        public int SelectedIndex { get; set; } = -1;
        public string EmptyDataText { get; set; }
        public string SortExpression { get; set; }
        public bool AutoGenerateColumns { get; set; } = true;
        public bool ShowHeader { get; set; } = true;
        public bool ShowFooter { get; set; }

        public event EventHandler<GridViewSortEventArgs> Sorting;
        public event EventHandler<GridViewRowEventArgs> RowCreated;
        public event EventHandler<GridViewRowEventArgs> RowDataBound;
        public event EventHandler<GridViewCommandEventArgs> RowCommand;
        public event EventHandler<GridViewDeleteEventArgs> RowDeleting;
        public event EventHandler<GridViewPageEventArgs> PageIndexChanging;
        public event EventHandler<GridViewSelectEventArgs> SelectedIndexChanging;
        public event EventHandler SelectedIndexChanged;

        public void DataBind() { }

        public void OnSorting(GridViewSortEventArgs e) => Sorting?.Invoke(this, e);
        public void OnRowCreated(GridViewRowEventArgs e) => RowCreated?.Invoke(this, e);
        public void OnRowDataBound(GridViewRowEventArgs e) => RowDataBound?.Invoke(this, e);
    }
}

// ---------------------------------------------------------------------------
//  System.Web.Services
// ---------------------------------------------------------------------------
namespace System.Web.Services
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class WebServiceAttribute : Attribute
    {
        public string Namespace { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class WebServiceBindingAttribute : Attribute
    {
        public WsiProfiles ConformsTo { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Location { get; set; }
        public WebServiceBindingAttribute() { }
        public WebServiceBindingAttribute(string name) { Name = name; }
    }

    public enum WsiProfiles
    {
        None = 0,
        BasicProfile1_1 = 1
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class WebMethodAttribute : Attribute
    {
        public string Description { get; set; }
        public bool EnableSession { get; set; }
        public string MessageName { get; set; }
        public int CacheDuration { get; set; }
        public bool BufferResponse { get; set; } = true;
    }

    public class WebService
    {
    }
}

// ---------------------------------------------------------------------------
//  ACL stubs (external dependency not available in .NET 8)
// ---------------------------------------------------------------------------
namespace ACL.Control
{
    public static class URL
    {
        public const string URLEMPLOYEEID = "eid";
        public const string URLCOMPANYID = "cid";
    }
}

namespace ACL.Security
{
    public static class Encryption
    {
        public static string Decrypt(string value) => value ?? string.Empty;
        public static string Encrypt(string value) => value ?? string.Empty;
    }
}

namespace ACL.MenuBar.Object
{
    public class LeftMenuItem
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public bool Expanded { get; set; }

        public LeftMenuItem() { }
        public LeftMenuItem(string id, string text, bool expanded)
        {
            ID = id;
            Text = text;
            Expanded = expanded;
        }
    }

    public class LeftMenuItemList
    {
        private readonly System.Collections.Generic.List<LeftMenuItem> _items = new System.Collections.Generic.List<LeftMenuItem>();

        public int Count => _items.Count;
        public LeftMenuItem this[int index] => _items[index];

        public void AddItem(LeftMenuItem item) => _items.Add(item);
        public void Clear() => _items.Clear();

        public System.Collections.Generic.IEnumerator<LeftMenuItem> GetEnumerator() => _items.GetEnumerator();
    }
}
