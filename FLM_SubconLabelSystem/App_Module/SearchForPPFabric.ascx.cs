using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

public partial class App_Module_SearchForPPFabric : System.Web.UI.UserControl
{
    private List<string> _search;
    private List<string> _query;

    public bool Deleted
    {
        get { return chkDeleted.Checked; }
    }

    public string SearchType
    {
        get { return Session["Type"] != null ? Session["Type"].ToString() : null; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindDDL();

        Control.Base setting = (Control.Base)this.Page;

        if (Session["key"] == null)
        {
            Session["key"] = setting.SetupKey;
        }
        else
        {
            if (Session["key"].ToString() != setting.SetupKey)
            {
                Session["key"] = setting.SetupKey;
                Session["Type"] = null;
                flush();
            }
        }

        if (Session["Type"] == null)
        {
            ChangeType("B");
        }
        else
        {
            ChangeType(Session["Type"].ToString());
            _search = Session["Search"] as List<string>;
            _query = Session["Query"] as List<string>;
        }

        if (!IsPostBack)
        {
            if (Session["Type"] != null && Session["Type"].ToString() == "B")
            {
                if (setting.SearchField.Trim() != string.Empty)
                {
                    txtSearch.Text = setting.SearchValue;
                    ddlSearch.SelectedValue = setting.SearchField;
                }
            }
            else
            {
                BindCriteria(Session["Search"] as List<string>);
            }
        }

        btnReset.Enabled = searchCriteria.Items.Count > 0;
        btnMinus.Enabled = searchCriteria.Items.Count > 0;
    }

    protected void BindDDL()
    {
        Control.Base setting = (Control.Base)this.Page;

        this.chkDeleted.Visible = setting.DeleteControl;

        if (setting.DeleteControl)
        {
            this.chkDeleted.Visible = setting.ShowDeletedControl;
            this.chkDeleted.Checked = setting.ShowDeleted;
        }

        if (setting.SetupKey == string.Empty)
        {
            this.Visible = false;
        }
        else
        {
            Control.Binding.BindDropDownListResource(this.ddlSearch, setting.SetupKey, "-", "-");
            Control.Binding.BindDropDownListResource(this.ddlSearchUsing, setting.SetupKey, "-", "-");
        }
    }

    private void ChangeType(string type)
    {
        Session["Type"] = type;

        if (type == "A")
        {
            lblSearch.Text = "Advance Search";
            pnlBasic.Visible = false;
            pnlAdvance.Visible = true;
        }
        else if (type == "B")
        {
            lblSearch.Text = "Basic Search";
            pnlBasic.Visible = true;
            pnlAdvance.Visible = false;
        }
    }

    private void detector()
    {
        ddlSearch2.Enabled = _search.Count > 0;
    }

    private void resetController()
    {
        ddlSearch.SelectedIndex = 0;
        ddlSearchUsing.SelectedIndex = 0;
        ddlOperator1.SelectedIndex = 0;
        ddlSearch2.SelectedIndex = 0;
        txtSearch.Text = string.Empty;
        txtSearchUsing.Text = string.Empty;
    }

    private void flush()
    {
        _search = new List<string>();
        _query = new List<string>();
        resetController();
        searchCriteria.Items.Clear();
        Session["Search"] = _search;
        Session["Query"] = _query;
    }

    protected void lbAdvSearch_Click(object sender, EventArgs e)
    {
        flush();
        ChangeType("A");
    }

    protected void lbBasicSearch_Click(object sender, EventArgs e)
    {
        flush();
        ChangeType("B");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Control.Base setting = (Control.Base)this.Page;

        if (ddlSearch.SelectedIndex > 0)
        {
            setting.SearchField = ddlSearch.SelectedValue;
            setting.SearchValue = txtSearch.Text;
        }
        else
        {
            setting.SearchField = string.Empty;
            setting.SearchValue = string.Empty;
        }

        setting.Item8 = "PostBack";
        setting.PageNo = 1;
        Response.Redirect(setting.GenerateList);
    }

    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        Control.Base setting = (Control.Base)this.Page;
        setting.SearchField = string.Empty;
        setting.SearchValue = ConversionString(_query);
        setting.Item8 = "PostBack";
        setting.PageNo = 1;
        Response.Redirect(setting.GenerateList);
    }

    protected void btnPlus_Click(object sender, EventArgs e)
    {
        if (!CheckSelection(ddlSearchUsing.SelectedValue, "Search Field"))
            return;

        if (!CheckSelection(ddlOperator1.SelectedValue, "Operator"))
            return;

        if (_search.Count > 0)
        {
            if (!CheckSelection(ddlSearch2.SelectedValue, "Operator (And/Or)"))
                return;
        }

        if (!EmptyCheck(txtSearchUsing.Text, "Search Value"))
            return;

        _search.Add((_search.Count > 0 ? ddlSearch2.SelectedItem.Text.Replace("-", "") + " " : string.Empty)
            + ddlSearchUsing.SelectedItem.Text + " "
            + ddlOperator1.SelectedItem.Text + " "
            + txtSearchUsing.Text);

        AddQuery(
            _search.Count > 0 ? ddlSearch2.SelectedItem.Text.Replace("-", "") + " " : "Add ",
            ddlSearchUsing.SelectedValue,
            ddlOperator1.SelectedValue,
            txtSearchUsing.Text);

        Session["Search"] = _search;
        Session["Query"] = _query;

        BindCriteria(_search);
        detector();
        resetController();
        ddlSearchUsing.Focus();
    }

    private void AddQuery(string additional, string searchField, string @operator, string searchValue)
    {
        if (@operator.Trim().ToUpper() == "LIKE")
            searchValue = "'%" + searchValue + "%'";
        else
            searchValue = "'" + searchValue + "'";

        _query.Add(additional + " UPPER(" + searchField + ") " + @operator + " UPPER(" + searchValue + ")");
    }

    private bool CheckSelection(string value, string field)
    {
        if (value == "-")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, field + " was " + Resources.Message.InvalidSelect);
            return false;
        }

        return true;
    }

    private bool EmptyCheck(string value, string field)
    {
        if (value.Trim() == string.Empty)
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, string.Format(Resources.Message.FieldEmpty, field));
            return false;
        }

        return true;
    }

    private string ConversionString(List<string> list)
    {
        StringBuilder temp = new StringBuilder();
        foreach (string str in list)
            temp.AppendLine(str);

        return temp.ToString();
    }

    private void BindCriteria(List<string> list)
    {
        btnReset.Enabled = list.Count > 0;
        btnMinus.Enabled = list.Count > 0;

        searchCriteria.DataSource = list;
        searchCriteria.DataBind();
    }

    protected void btnMinus_Click(object sender, EventArgs e)
    {
        if (_search.Count == 0)
            return;

        if (searchCriteria.SelectedIndex == -1)
        {
            if (_search.Count == 1)
            {
                _search = new List<string>();
                _query = new List<string>();
            }
            else
            {
                _search.RemoveAt(_search.Count - 1);
                _query.RemoveAt(_search.Count - 1);
            }
        }
        else
        {
            _search.RemoveAt(searchCriteria.SelectedIndex);
            _query.RemoveAt(searchCriteria.SelectedIndex);
        }

        if (_search.Count > 0)
        {
            if (_search[0].StartsWith("AND"))
                _search[0] = _search[0].Substring(3);
            else if (_search[0].StartsWith("OR"))
                _search[0] = _search[0].Substring(2);
        }

        if (_query.Count > 0)
        {
            if (_query[0].StartsWith("AND"))
                _query[0] = _query[0].Substring(3);
            else if (_query[0].StartsWith("OR"))
                _query[0] = _query[0].Substring(2);

            _query[0] = "AND" + _query[0];
        }

        Session["Search"] = _search;
        Session["Query"] = _query;

        BindCriteria(_search);
        detector();
        resetController();
        ddlSearchUsing.Focus();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        _search = new List<string>();
        _query = new List<string>();
        Session["Search"] = _search;
        Session["Query"] = _query;

        BindCriteria(_search);
        detector();
        resetController();
    }

    protected void chkDeleted_CheckedChanged(object sender, EventArgs e)
    {
        Control.Base setting = (Control.Base)this.Page;
        setting.ShowDeleted = chkDeleted.Checked;
        Response.Redirect(setting.GenerateList);
    }
}