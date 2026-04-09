using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Library.Root.Control
{
    /// <summary>
    /// Handler All Page Common Function
    /// 1 ) Retrieve and Determine Key
    /// 2 ) Retrieve and Determine Action(Insert / Update / Delete)
    /// 3 ) Generate (Insert / Edit / Delete / List / View ) URL Based on the Setup Key
    /// 4 ) Generete Title Based on the Setup Key
    /// 5 ) Generate Action Desc
    /// 6 ) Retrieve and Determine Sort Field , Sort Direction
    /// 7 ) Generate the List View URL (Include Sort Field , Sort Value and Page No )
    /// 8 ) Function Control property , Default = true , if false all the generate list will not auto generate, this is to igone the error wish the page doest not have setting in the resource page
    /// 9 ) Delete Control property , Default = true, if false then the show deleted Check box will be disappear.
    /// 10) List Page Path (Read Only)
    /// 11) Detail Page Path (Read Only)
    /// 12) Log Page Path (Read Only)
    /// 13) Add Show Delete Property
    /// 14) Print Page Parameter Generator
    /// 15) MustOverride Databind Function
    /// 16) PrintPage Property (Read Only)
    /// 17) Add item into item1 ID (URL Parameter)
    /// 18) Remove item From item1 (URL Parameter)
    /// 19) Check the Id was In the list
    /// 20) Add Control Property
    /// 21) Advance Control Property
    /// 22) Delete Image Path Property
    /// 23) Grid View Property
    /// 24) Grid View First Column Checkbox Property
    /// 25) Grid View Mouse Out script
    /// 26) Grid View Mouse Over Script
    /// 27) Print Control Property
    /// 28) View History Control Property
    /// 29) Show Delete Control Property , Default = true, if false then the show deleted Check box will be disappear.(note if delete control = false, this will be automatically false)
    /// 30) Delete Redirect the same page Control : Default : False
    /// 31) URL Additional Add ReturnURL parameter control. if true then the url at the back will attach the current url.
    ///     note : this is use to back to the previous page.
    /// 32) Record Type Column Property - This will handler the list for the delete record type
    /// 33) Deleted Text for Hidden the Delete Control
    /// 34) Deleted Column Compare, if match the action will be follow the properties setting.
    /// 35) List for Detail Page(Detail Page was contain List Page), if true the parameter will carry to the detail page and combine with the listing page
    /// 36) Delete confirmation box in the listing page
    /// 37) Delete class name
    ///
    /// Remark : The default sort property will determine whether the url is check or not
    /// check ( if the url failed retrieve the sort field then will generate and redirect its
    /// -------------------------------------------------------------------------------
    /// C.C.Yeon    25 April 2011   initial Version
    /// C.C.Yeon    12 May   2011   Add FunctionControl Property
    /// C.C.Yeon    08 July  2011   Add Print Page Parameter Generator Function
    /// C.C.Yeon    29 April 2012   Add DetailListingFunction was handler the detail was contains listing function
    /// C.C.Yeon    07 May   2012   Add Control for the Delete confirmation
    /// C.C.Yeon    24 June  2012   Add Delete Class Name
    /// Elven Lee   05 June  2013   Property [Record Type Column], 32, handle additional control field added in GridViewRow to ease developer.
    /// </summary>
    public abstract class Base_backup : Page
    {
        public enum EnumAction
        {
            None = 0,
            Add = 1,
            Edit = 3,
            Delete = 5,
            View = 7,
            History = 9
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.FunctionControl)
            {
                this.BindAction();
            }

            this.BindSort();
            base.OnInit(e);

            if (this.FunctionControl)
            {
                this.CheckURL();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.FunctionControl)
            {
                this.BindKey();
            }

            if (this.CustomTitle == false)
            {
                this.Title = this.DisplayTitle;
            }

            base.OnLoad(e);

            if (this.GridView != null)
            {
                if (this.DeleteControl)
                {
                    TemplateField _field = new TemplateField();
                    _field.ItemTemplate = new deletefield(ListItemType.Item);
                    _field.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    _field.ControlStyle.Width = 30;
                    _field.ItemStyle.Width = 30;
                    _field.HeaderStyle.Width = 30;
                    _field.ItemStyle.CssClass = "Delete";
                    this.GridView.Columns.Insert(0, _field);
                }

                if (this.GridViewCheckColumn)
                {
                    TemplateField _field = new TemplateField();
                    _field.ItemTemplate = new checkboxfield(ListItemType.Item);
                    _field.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    _field.HeaderTemplate = new checkboxfield(ListItemType.Header);
                    _field.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    _field.ControlStyle.Width = 30;
                    _field.ItemStyle.Width = 30;
                    _field.HeaderStyle.Width = 30;
                    this.GridView.Columns.Insert(0, _field);
                }

                if (this.GridViewRadioColumn)
                {
                    TemplateField _field = new TemplateField();
                    _field.ItemTemplate = new radiobuttonfield(ListItemType.Item);
                    _field.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    //_field.HeaderTemplate = new radiobuttonfield(ListItemType.Header);
                    //_field.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    _field.ControlStyle.Width = 30;
                    _field.ItemStyle.Width = 30;
                    _field.HeaderStyle.Width = 30;
                    this.GridView.Columns.Insert(0, _field);
                }

                if (this.ViewHistoryControl)
                {
                    TemplateField _field = new TemplateField();
                    _field.ItemTemplate = new historyfield(ListItemType.Item);
                    _field.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    _field.HeaderTemplate = new historyfield(ListItemType.Header);
                    _field.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    _field.ControlStyle.Width = 80;
                    _field.ItemStyle.Width = 80;
                    _field.HeaderStyle.Width = 80;
                    this.GridView.Columns.Add(_field);
                }

                this.GridView.Sorting += this.Sorting;
                this.GridView.RowCreated += this.gridview_rowcreated;
                this.GridView.RowDataBound += this.gridview_RowDataBound;
            }
        }

        private void Sorting(object sender, GridViewSortEventArgs e)
        {
            this.SortField = e.SortExpression.ToString();
            this.SortDirection = ((Convert.ToInt32(this.SortDirection) + 1) % 2).ToString();
            Response.Redirect(this.GenerateList);
        }

        protected void CheckURL()
        {
            if (!IsPostBack)
            {
                if (this.DefaultSort != string.Empty)
                {
                    if (SortField == string.Empty)
                    {
                        Response.Redirect(GenerateList);
                    }
                }
                else
                {
                    if (this._Action == EnumAction.None)
                    {
                        Response.Redirect(GetUrl(EnumAction.None));
                    }
                }
            }
        }

        /// <summary>
        /// Retrieve the Key from View State or URL
        /// </summary>
        private void BindKey()
        {
            if (ViewState["Key"] == null)
            {
                this._Key = Request.QueryString["id"];
                ViewState["Key"] = this._Key;
            }
            else
            {
                this._Key = Convert.ToString(ViewState["Key"]);
            }
        }

        /// <summary>
        /// Retrieve the Sort Field and Sort Direction
        /// </summary>
        private void BindSort()
        {
            foreach (string _query in Request.QueryString)
            {
                if (!string.IsNullOrEmpty(_query))
                {
                    switch (_query)
                    {
                        case "sort":
                            this._SortField = Request.QueryString["sort"];
                            break;
                        case "dic":
                            this._sortDirection = Request.QueryString["dic"];
                            break;
                        case "page":
                            if (int.TryParse(Request.QueryString["page"], out int parsedPage))
                            {
                                this._Pageno = parsedPage;
                            }
                            else
                            {
                                this._Pageno = 1;
                            }
                            break;
                        case "fld":
                            this._SearchField = Server.UrlDecode(Request.QueryString["fld"]);
                            break;
                        case "vl":
                            this._SearchValue = Server.UrlDecode(Request.QueryString["vl"]);
                            break;
                        case "type":
                            this._type = Request.QueryString["type"];
                            break;
                        case "itm1":
                            this._item1 = Request.QueryString["itm1"];
                            break;
                        case "itm2":
                            this._item2 = Request.QueryString["itm2"];
                            break;
                        case "itm3":
                            this._item3 = Request.QueryString["itm3"];
                            break;
                        case "itm4":
                            this._item4 = Request.QueryString["itm4"];
                            break;
                        case "itm5":
                            this._item5 = Request.QueryString["itm5"];
                            break;
                        case "itm6":
                            this._item6 = Request.QueryString["itm6"];
                            break;
                        case "itm7":
                            this._item7 = Request.QueryString["itm7"];
                            break;
                        case "itm8":
                            this._item8 = Request.QueryString["itm8"];
                            break;
                        case "itm9":
                            this._item9 = Request.QueryString["itm9"];
                            break;
                        case "itm10":
                            this._item10 = Request.QueryString["itm10"];
                            break;
                        case "itm11":
                            this._item11 = Request.QueryString["itm11"];
                            break;
                        case "itm12":
                            this._item12 = Request.QueryString["itm12"];
                            break;
                        case "dlt":
                            this._ShowDeleted = Convert.ToBoolean(Request.QueryString["dlt"]);
                            break;
                        case "id":
                            if (this.DetailListingFunction)
                            {
                                this._Key = Request.QueryString["id"];
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Retrieve the Action From URL
        /// </summary>
        private void BindAction()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["action"]))
            {
                switch (Convert.ToInt32(Request.QueryString["action"]))
                {
                    case 1:
                        this._Action = EnumAction.Add;
                        break;
                    case 3:
                        this._Action = EnumAction.Edit;
                        break;
                    case 5:
                        this._Action = EnumAction.Delete;
                        break;
                    case 7:
                        this._Action = EnumAction.View;
                        break;
                    default:
                        this._Action = EnumAction.None;
                        break;
                }
            }
        }

        // 37
        private string _DeleteClassName = string.Empty;
        public string DeleteClassName
        {
            get { return _DeleteClassName; }
            set { _DeleteClassName = value; }
        }

        // 36
        private bool _DeleteConfirmationBox = false;
        public bool DeleteConfirmationBox
        {
            get { return _DeleteConfirmationBox; }
            set { _DeleteConfirmationBox = value; }
        }

        // 35
        private bool _DetailListingFunction = false;
        public bool DetailListingFunction
        {
            get { return _DetailListingFunction; }
            set { _DetailListingFunction = value; }
        }

        // 34
        private bool _DeletedVisibleControl = false;
        public bool DeletedVisibleControl
        {
            get { return _DeletedVisibleControl; }
            set { _DeletedVisibleControl = value; }
        }

        // 33
        private string _DetetedText = "Deleted";
        public string DeletedText
        {
            get { return _DetetedText; }
            set { _DetetedText = value; }
        }

        // 32
        private int _recordTypeColumn = -1;
        public int RecordTypeColumn
        {
            get
            {
                return (_recordTypeColumn > -1)
                    ? _recordTypeColumn + (this.GridViewCheckColumn ? 1 : 0) + (this.GridViewRadioColumn ? 1 : 0) + (this.DeleteControl ? 1 : 0)
                    : -1;
            }
            set { _recordTypeColumn = value; }
        }

        // 31
        private bool _ReturnURLControl = false;
        public bool ReturnURLControl
        {
            get { return _ReturnURLControl; }
            set { _ReturnURLControl = value; }
        }

        // 30
        private bool _DeleteRedirectList = false;
        public bool DeleteRedirectList
        {
            get { return _DeleteRedirectList; }
            set { _DeleteRedirectList = value; }
        }

        // 29
        private bool _ShowDeletedControl = true;
        public bool ShowDeletedControl
        {
            get { return this._ShowDeletedControl; }
            set { this._ShowDeletedControl = value; }
        }

        // 28
        private bool _ViewHistoryControl = true;
        public bool ViewHistoryControl
        {
            get { return this._ViewHistoryControl; }
            set { this._ViewHistoryControl = value; }
        }

        // 27
        private bool _PrintControl = true;
        public bool PrintControl
        {
            get { return this._PrintControl; }
            set { this._PrintControl = value; }
        }

        // 26
        private string _gvMouseOver = "Highlight(this)";
        public string GridViewRowMouseOver
        {
            get { return this._gvMouseOver; }
            set { this._gvMouseOver = value; }
        }

        // 25
        private string _gvMouseOUT = "UnHighlight(this)";
        public string GridViewRowMouseOut
        {
            get { return this._gvMouseOUT; }
            set { this._gvMouseOUT = value; }
        }

        // 24
        private bool _checkcolumn = true;
        public bool GridViewCheckColumn
        {
            get { return this._checkcolumn; }
            set { this._checkcolumn = value; }
        }

        // 24
        private bool _CheckBoxPostBack = true;
        public bool GridViewCheckBoxPostBack
        {
            get { return this._CheckBoxPostBack; }
            set { this._CheckBoxPostBack = value; }
        }

        private bool _radiocolumn = false;
        public bool GridViewRadioColumn
        {
            get { return this._radiocolumn; }
            set { this._radiocolumn = value; }
        }

        // 23
        private System.Web.UI.WebControls.GridView _gridview = null;
        public System.Web.UI.WebControls.GridView GridView
        {
            get { return this._gridview; }
            set { this._gridview = value; }
        }

        // 22
        private string _deleteImagePath = "~/Image/delete1.gif";
        public string DeleteImagePath
        {
            get { return this._deleteImagePath; }
            set { this._deleteImagePath = value; }
        }

        // 21
        private bool _AdvanceControl = true;
        public bool AdvancedControl
        {
            get { return this._AdvanceControl; }
            set { this._AdvanceControl = value; }
        }

        // 20
        private bool _AddControl = true;
        public bool AddControl
        {
            get { return this._AddControl; }
            set { this._AddControl = value; }
        }

        private string _type = string.Empty;
        public string Type
        {
            get { return _type; }
        }

        // Page Key
        private string _Key = string.Empty;
        public string Key
        {
            get { return _Key; }
        }

        // Action Such as Insert, Edit or Delete
        private EnumAction _Action = EnumAction.None;
        public EnumAction Action
        {
            get { return _Action; }
        }

        private string _DefaultSort = string.Empty;
        public string DefaultSort
        {
            get { return _DefaultSort; }
            set { _DefaultSort = value; }
        }

        private string _sortDirection = "1";
        public string SortDirection
        {
            get { return _sortDirection; }
            set { _sortDirection = value; }
        }

        private string _SortField;
        public string SortField
        {
            get { return _SortField; }
            set { _SortField = value; }
        }

        public int _Pageno = 1;
        public int PageNo
        {
            get { return _Pageno; }
            set { _Pageno = value; }
        }

        private string _SearchField = string.Empty;
        public string SearchField
        {
            get { return Server.UrlDecode(_SearchField); }
            set { _SearchField = Server.UrlEncode(value); }
        }

        private string _SearchValue = string.Empty;
        public string SearchValue
        {
            get { return Server.UrlDecode(_SearchValue); }
            set
            {
                if (value.IndexOf("AND ") != -1 || value.IndexOf("OR ") != -1)
                {
                    // Eliminate the AND or OR during the first words
                    if (value.Substring(0, 4).IndexOf("AND ") != -1)
                    {
                        value = value.Substring(3, value.Length - 3);
                    }
                    else if (value.Substring(0, 3).IndexOf("OR ") != -1)
                    {
                        value = value.Substring(2, value.Length - 2);
                    }
                }
                _SearchValue = Server.UrlEncode(value);
            }
        }

        private bool _ShowDeleted = false;
        public bool ShowDeleted
        {
            get { return _ShowDeleted; }
            set { _ShowDeleted = value; }
        }

        private string _item1 = string.Empty;
        public string Item1
        {
            get { return Server.UrlDecode(_item1); }
            set { _item1 = value; }
        }

        private string _item2 = string.Empty;
        public string Item2
        {
            get { return _item2; }
            set { _item2 = value; }
        }

        private string _item3 = string.Empty;
        public string Item3
        {
            get { return _item3; }
            set { _item3 = value; }
        }

        private string _item4 = string.Empty;
        public string Item4
        {
            get { return _item4; }
            set { _item4 = value; }
        }

        private string _item5 = string.Empty;
        public string Item5
        {
            get { return _item5; }
            set { _item5 = value; }
        }

        private string _item6 = string.Empty;
        public string Item6
        {
            get { return _item6; }
            set { _item6 = value; }
        }

        private string _item7 = string.Empty;
        public string Item7
        {
            get { return _item7; }
            set { _item7 = value; }
        }

        private string _item8 = string.Empty;
        public string Item8
        {
            get { return _item8; }
            set { _item8 = value; }
        }

        private string _item9 = string.Empty;
        public string Item9
        {
            get { return _item9; }
            set { _item9 = value; }
        }

        private string _item10 = string.Empty;
        public string Item10
        {
            get { return _item10; }
            set { _item10 = value; }
        }

        private string _item11 = string.Empty;
        public string Item11
        {
            get { return _item11; }
            set { _item11 = value; }
        }

        private string _item12 = string.Empty;
        public string Item12
        {
            get { return _item12; }
            set { _item12 = value; }
        }

        // 5)
        public string ActionDesc
        {
            get
            {
                switch (_Action)
                {
                    case EnumAction.Add:
                        return "Add";
                    case EnumAction.Delete:
                        return "Delete";
                    case EnumAction.Edit:
                        return "Edit";
                    case EnumAction.View:
                        return "View";
                    case EnumAction.None:
                        return string.Empty;
                    default:
                        return string.Empty;
                }
            }
        }

        // 7)
        public string GenerateList
        {
            get
            {
                return ResolveUrl(this.ListPage) + "?sort=" + (this.SortField == string.Empty ? DefaultSort : this.SortField) +
                    "&dic=" + (this.SortDirection == string.Empty ? "0" : this.SortDirection) + "&page=" + this._Pageno + "&fld=" + Server.UrlEncode(this._SearchField) + "&vl=" +
                    Server.UrlEncode(this._SearchValue) + "&type=" + this._type + "&itm1=" + Server.UrlEncode(this._item1) + "&itm2=" + this._item2 + "&itm3=" + this._item3 + "&itm4=" +
                    this._item4 + "&itm5=" + this._item5 + "&itm6=" + this._item6 + "&itm7=" + this._item7 + "&itm8=" + this._item8 + "&itm9=" + this._item9 +
                    "&itm10=" + this._item10 + "&itm11=" + this._item11 + "&itm12=" + this._item12 + "&dlt=" + this._ShowDeleted + (this._DetailListingFunction ? "&" + DetailListingAdditionalFunction() : string.Empty);
            }
        }

        private string DetailListingAdditionalFunction()
        {
            string _temp = string.Empty;
            switch (Action)
            {
                case EnumAction.Add:
                    _temp = "action=" + (int)EnumAction.Add;
                    break;
                case EnumAction.Delete:
                    _temp = "?action=" + (int)EnumAction.Delete + "&id=" + (Key == "" ? this.Key : Key);
                    break;
                case EnumAction.Edit:
                    _temp = "action=" + (int)EnumAction.Edit + "&id=" + (Key == "" ? this.Key : Key);
                    break;
                case EnumAction.View:
                    _temp = "action=" + (int)EnumAction.View + "&id=" + (Key == "" ? this.Key : Key);
                    break;
                default:
                    _temp = string.Empty;
                    break;
            }

            return _temp;
        }

        private string AddReturnURLControl(string URL)
        {
            if (URL != string.Empty)
            {
                if (ReturnURLControl)
                {
                    return URL + (URL.IndexOf("?") != -1 ? "&" : "?") + "ReturnURL=" + Server.UrlEncode(Request.RawUrl);
                }
                else
                {
                    return URL;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        // 3)
        public string GetUrl(EnumAction action, string key = "")
        {
            if (this.SetupKey == string.Empty)
            {
                return string.Empty;
            }

            string tempurl = string.Empty;

            switch (action)
            {
                case EnumAction.Add:
                    tempurl = ResolveUrl(this.DetailPage + "?action=" + (int)EnumAction.Add);
                    break;
                case EnumAction.Delete:
                    if (this.DeleteRedirectList)
                    {
                        tempurl = this.GenerateList + "&action=" + (int)EnumAction.Delete + "&id=" + (key == "" ? this.Key : key);
                    }
                    else
                    {
                        tempurl = ResolveUrl(this.DetailPage + "?action=" + (int)EnumAction.Delete + "&id=" + (key == "" ? this.Key : key));
                    }
                    break;
                case EnumAction.Edit:
                    tempurl = ResolveUrl(this.DetailPage + "?action=" + (int)EnumAction.Edit + "&id=" + (key == "" ? this.Key : key));
                    break;
                case EnumAction.View:
                    tempurl = ResolveUrl(this.DetailPage + "?action=" + (int)EnumAction.View + "&id=" + (key == "" ? this.Key : key));
                    break;
                case EnumAction.History:
                    tempurl = ResolveUrl(this.LogPage + "?id=" + (key == "" ? this.Key : key) + "&key=" + this.SetupKey + "&act=" + this.Action + "&page=1");
                    break;
                default:
                    tempurl = ResolveUrl(this.ListPage);
                    break;
            }

            return AddReturnURLControl(tempurl);
        }

        // This Key must Same With The Resource Page
        // Based On this key, the value will retrieved
        private string _SetupKey = string.Empty;
        public string SetupKey
        {
            get { return _SetupKey; }
            set { _SetupKey = value; }
        }

        // 4)
        public abstract string DisplayTitle { get; }

        // 8)
        private bool _FunctionControl = true;
        public bool FunctionControl
        {
            get { return _FunctionControl; }
            set { _FunctionControl = value; }
        }

        // 9)
        private bool _DeleteControl = true;
        public bool DeleteControl
        {
            get { return _DeleteControl; }
            set { _DeleteControl = value; }
        }

        private bool _customTitle = false;
        public bool CustomTitle
        {
            get { return _customTitle; }
            set { _customTitle = value; }
        }

        // 10)
        public abstract string ListPage { get; }

        // 11)
        public abstract string DetailPage { get; }

        // 12)
        public abstract string LogPage { get; }

        // 16)
        public abstract string PrintPage { get; }

        // 14)
        public string GeneratePrintPage()
        {
            return ResolveUrl(PrintPage + "?type=" + this.SetupKey + "&itm1=" + this.Item1);
        }

        // 17)
        public void AddItem(string id)
        {
            string[] idlist = this.Item1.Split(',');

            if (idlist.Contains(id))
            {
                return;
            }

            if (this._item1 == string.Empty)
            {
                this.Item1 = id;
            }
            else
            {
                this.Item1 += "," + id;
            }
        }

        // 18
        public void RemoveItem(string id)
        {
            string[] idlist = this.Item1.Split(',');

            if (idlist.Contains(id) == false)
            {
                return;
            }

            this.Item1 = string.Empty;

            foreach (string Str in idlist)
            {
                if (Str != id)
                {
                    if (this.Item1 == string.Empty)
                    {
                        this.Item1 = Str;
                    }
                    else
                    {
                        this.Item1 += "," + Str;
                    }
                }
            }
        }

        // 19
        public string MatchID(string GridViewID)
        {
            string result = Convert.ToString(false);

            string[] str = this.Item1.Split(',');
            if (str.Length > 0)
            {
                result = Convert.ToString(str.Contains(GridViewID));
            }
            return result;
        }

        protected void grdResult_Sorting(object sender, GridViewSortEventArgs e)
        {
            this.SortField = e.SortExpression.ToString();
            this.SortDirection = ((Convert.ToInt32(this.SortDirection) + 1) % 2).ToString();
            base.Response.Redirect(this.GenerateList);
        }

        protected void gridview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (this.DeleteControl)
                {
                    if (this.RecordTypeColumn > -1)
                    {
                        if (e.Row.Cells[this.RecordTypeColumn].Text.ToUpper() == this.DeletedText.ToUpper())
                        {
                            ((Literal)e.Row.FindControl("ltritem")).Visible = this._DeletedVisibleControl;
                        }
                        else
                        {
                            ((Literal)e.Row.FindControl("ltritem")).Visible = !this._DeletedVisibleControl;
                        }
                    }
                }
            }
        }

        private string addClassName()
        {
            if (this.DeleteClassName != string.Empty)
            {
                return "class='" + this.DeleteClassName + "'";
            }
            else
            {
                return string.Empty;
            }
        }

        private void gridview_rowcreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && this.GridView != null)
            {
                if (this.GridViewRowMouseOver != string.Empty)
                {
                    e.Row.Attributes.Add("onMouseOver", this.GridViewRowMouseOver);
                }

                if (this.GridViewRowMouseOut != string.Empty)
                {
                    e.Row.Attributes.Add("onMouseOut", this.GridViewRowMouseOut);
                }

                if (this.GridViewCheckColumn)
                {
                    CheckBox _item = (CheckBox)e.Row.FindControl("ckitem");
                    _item.Checked = Convert.ToBoolean(this.MatchID(Convert.ToString(this.GridView.DataKeys[e.Row.RowIndex][0])));

                    _item.CheckedChanged += OnCheckChanged;
                }

                if (this.GridViewRadioColumn)
                {
                    RadioButton _item = (RadioButton)e.Row.FindControl("rbitem");
                    _item.Checked = Convert.ToBoolean(this.MatchID(Convert.ToString(this.GridView.DataKeys[e.Row.RowIndex][0])));
                    _item.CheckedChanged += OnCheckChanged2;
                }

                if (this.DeleteControl)
                {
                    string _url = string.Empty;
                    ((Literal)e.Row.FindControl("ltritem")).Text =
                        string.Format("<a target='_self' {3} href='{0}' {2}><img src='{1}' alt='Delete' /></a>",
                            this.ResolveUrl(this.GetUrl(EnumAction.Delete, Convert.ToString(this.GridView.DataKeys[e.Row.RowIndex][0]))),
                            this.ResolveUrl(this.DeleteImagePath),
                            this.DeleteConfirmationBox ? "onclick=\"return confirm('Are you sure you want to delete?')\"" : "",
                            addClassName());
                }

                if (this.ViewHistoryControl)
                {
                    ((Literal)e.Row.FindControl("ltrhisitem")).Text = string.Format("<a target='_blank' href='{0}'>View</a>", this.GetUrl(EnumAction.History, Convert.ToString(this.GridView.DataKeys[e.Row.RowIndex][0])));
                }
            }

            if (e.Row.RowType == DataControlRowType.Header && this.GridView != null && this.GridViewCheckColumn)
            {
                if (this.GridViewCheckColumn)
                {
                    CheckBox _item = (CheckBox)e.Row.FindControl("chkall");

                    if (this.Item2 != string.Empty)
                    {
                        _item.Checked = Convert.ToBoolean(this.Item2);
                    }

                    _item.CheckedChanged += OnCheckAll;
                }
            }
        }

        public void OnCheckAll(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;

            this.Item2 = Convert.ToString(checkbox.Checked);

            foreach (GridViewRow gvr in this.GridView.Rows)
            {
                if (checkbox.Checked)
                {
                    this.AddItem(Convert.ToString(this.GridView.DataKeys[gvr.RowIndex][0]));
                }
                else
                {
                    this.RemoveItem(Convert.ToString(this.GridView.DataKeys[gvr.RowIndex][0]));
                }
            }

            base.Response.Redirect(this.GenerateList);
        }

        public void OnCheckChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            GridViewRow contain = (GridViewRow)checkbox.NamingContainer;

            this.Item2 = Convert.ToString(false);
            if (checkbox.Checked)
            {
                this.AddItem(Convert.ToString(this.GridView.DataKeys[contain.RowIndex][0]));
            }
            else
            {
                this.RemoveItem(Convert.ToString(this.GridView.DataKeys[contain.RowIndex][0]));
            }

            Response.Redirect(this.GenerateList);
        }

        public void OnCheckChanged2(object sender, EventArgs e)
        {
            RadioButton radiobutton = (RadioButton)sender;
            GridViewRow contain = (GridViewRow)radiobutton.NamingContainer;

            this.Item1 = "";
            this.Item2 = Convert.ToString(false);
            if (radiobutton.Checked)
            {
                this.AddItem(Convert.ToString(this.GridView.DataKeys[contain.RowIndex][0]));
            }

            Response.Redirect(this.GenerateList);
        }
    }
}
