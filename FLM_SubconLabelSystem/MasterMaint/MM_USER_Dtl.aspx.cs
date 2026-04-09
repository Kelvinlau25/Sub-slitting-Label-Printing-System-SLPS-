using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library.Root.Control;

public partial class MasterMaint_MM_USER_Dtl : Control.Base
{
    public MasterMaint_MM_USER_Dtl()
    {
        SetupKey = "MM_USER";
    }

    public override void BindData()
    {
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        rfName.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Name");
        rfUserID.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "UserID");
        rfDepartment.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Department");
        rfEmail.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Email Address");
        rfPassword.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Password");
        rfCompName.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "Company Name");
        rePassword.ValidationExpression = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d!$%@#£€*?&]{9,15}$";
        rePassword.ErrorMessage = "Invalid Format. New Password Must Contain at least 1 Alphabet and 1 Number with a Minimum 9 Characters.";
        rfEmail2.ErrorMessage = "Email Address format:   eg.you@(domain.com)";

        DataTable _dtCompName = Library.Database.BLL.user.GetDLLData("CompanyName", "");

        if (_dtCompName.Rows.Count > 0)
        {
            ddlCompName.Items.Insert(0, new ListItem(" - SELECT - ", ""));
            ddlCompName.DataSource = _dtCompName;
            ddlCompName.DataTextField = "CompanyName";
            ddlCompName.DataValueField = "ID_MM_COMPANY";
            ddlCompName.DataBind();
        }
        else if (_dtCompName.Rows.Count == 0)
        {
            ddlCompName.Items.Add(new ListItem(" - SELECT - ", ""));
        }

        int uLevel = Convert.ToInt32(Session["ULEVEL"]);
        resetlnk.Visible = !(uLevel == 3 || uLevel == 2);
    }

    protected void UCAction_DisplayMode()
    {
        Cdisplay.Visible = true;
        Cmodify.Visible = false;

        DataTable _datatable = Library.Database.BLL.user.GetData(Key);

        lblCompName.Text = _datatable.Rows[0]["COMPANYNAME"].ToString();
        lblName.Text = _datatable.Rows[0]["NAME"].ToString();
        lblUserID.Text = _datatable.Rows[0]["USERID"].ToString();
        lblDepartment.Text = _datatable.Rows[0]["DEPARTMENT"].ToString();
        lblEmail.Text = _datatable.Rows[0]["Email"].ToString();

        int status = Convert.ToInt32(_datatable.Rows[0]["STATUS"]);
        if (status == 0)
            lblaccstats.Text = "Normal";
        else if (status == 1)
            lblaccstats.Text = "Locked";

        int uLevel = Convert.ToInt32(_datatable.Rows[0]["ULEVEL"]);
        if (uLevel == 1)
            lblLevel.Text = "System Administrator";
        else if (uLevel == 2)
            lblLevel.Text = "User";
        else
            lblLevel.Text = "Vendor";

        UCAction.CreatedBy = "";
        UCAction.CreatedDate = new DateTime();
        UCAction.CreatedLoc = "";

        DateTime tmpCreateDate;
        if (DateTime.TryParse(_datatable.Rows[0]["CREATED_DATE"].ToString(), out tmpCreateDate))
        {
            UCAction.CreatedBy = _datatable.Rows[0]["CREATED_BY"].ToString();
            UCAction.CreatedDate = tmpCreateDate;
            UCAction.CreatedLoc = _datatable.Rows[0]["CREATED_LOC"].ToString();
        }

        UCAction.UpdatedBy = "";
        UCAction.UpdatedDate = new DateTime();
        UCAction.UpdatedLoc = "";

        DateTime tmpUpdateDate;
        if (DateTime.TryParse(_datatable.Rows[0]["UPDATED_DATE"].ToString(), out tmpUpdateDate))
        {
            UCAction.UpdatedBy = _datatable.Rows[0]["UPDATED_BY"].ToString();
            UCAction.UpdatedDate = tmpUpdateDate;
            UCAction.UpdatedLoc = _datatable.Rows[0]["UPDATED_LOC"].ToString();
        }

        UCAction.EditMode = _datatable.Rows[0]["REC_TYPE"].ToString() != "5";
    }

    public void CheckRadioButton()
    {
        if (!RBLevel1.Checked && !RBLevel2.Checked && !RBLevel3.Checked)
        {
            rfRB.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "User Level");
        }
    }

    protected void UCAction_ModifyMode()
    {
        if (Action == EnumAction.Add || Action == EnumAction.Edit)
        {
            CheckRadioButton();
        }

        Cdisplay.Visible = false;
        Cmodify.Visible = true;

        if (!IsPostBack)
        {
            if (Action == EnumAction.Edit)
            {
                tblpassword.Visible = false;
                txtUserID.Visible = false;
                lbUserID.Visible = true;

                int uLevel = Convert.ToInt32(Session["ULEVEL"]);

                if (uLevel == 2)
                {
                    lbCompName.Visible = true;
                    ddlCompName.Visible = false;
                }
                else
                {
                    lbCompName.Visible = false;
                    ddlCompName.Visible = true;
                }

                if (uLevel == 1)
                {
                    lblAccStats2.Visible = false;
                    ddlAccStats.Visible = true;
                }
                else
                {
                    lblAccStats2.Visible = true;
                    ddlAccStats.Visible = false;
                }

                DataTable _datatable = Library.Database.BLL.user.GetData(Key);

                ddlCompName.SelectedValue = _datatable.Rows[0]["ID_MM_COMPANY"].ToString();
                lbCompName.Text = _datatable.Rows[0]["COMPANYNAME"].ToString();
                txtName.Text = _datatable.Rows[0]["NAME"].ToString();
                txtUserID.Text = _datatable.Rows[0]["USERID"].ToString();
                lbUserID.Text = _datatable.Rows[0]["USERID"].ToString();
                txtDepartment.Text = _datatable.Rows[0]["DEPARTMENT"].ToString();
                txtEmail.Text = _datatable.Rows[0]["EMAIL"].ToString();

                int status = Convert.ToInt32(_datatable.Rows[0]["STATUS"]);
                if (status == 0)
                {
                    lblAccStats2.Text = "Normal";
                    ddlAccStats.SelectedValue = _datatable.Rows[0]["STATUS"].ToString();
                }
                else if (status == 1)
                {
                    lblAccStats2.Text = "Locked";
                    ddlAccStats.SelectedValue = _datatable.Rows[0]["STATUS"].ToString();
                }

                int dbULevel = Convert.ToInt32(_datatable.Rows[0]["ULEVEL"]);
                if (dbULevel == 1) { RBLevel1.Checked = true; TextBox1.Text = "1"; }
                if (dbULevel == 2) { RBLevel2.Checked = true; TextBox1.Text = "2"; }
                if (dbULevel == 3) { RBLevel3.Checked = true; TextBox1.Text = "3"; }

                txtPassword.Text = _datatable.Rows[0]["PASSWORD"].ToString();

                UCAction.CreatedBy = "";
                UCAction.CreatedDate = new DateTime();
                UCAction.CreatedLoc = "";

                DateTime tmpCreateDate;
                if (DateTime.TryParse(_datatable.Rows[0]["CREATED_DATE"].ToString(), out tmpCreateDate))
                {
                    UCAction.CreatedBy = _datatable.Rows[0]["CREATED_BY"].ToString();
                    UCAction.CreatedDate = tmpCreateDate;
                    UCAction.CreatedLoc = _datatable.Rows[0]["CREATED_LOC"].ToString();
                }

                UCAction.UpdatedBy = "";
                UCAction.UpdatedDate = new DateTime();
                UCAction.UpdatedLoc = "";

                DateTime tmpUpdateDate;
                if (DateTime.TryParse(_datatable.Rows[0]["UPDATED_DATE"].ToString(), out tmpUpdateDate))
                {
                    UCAction.UpdatedBy = _datatable.Rows[0]["UPDATED_BY"].ToString();
                    UCAction.UpdatedDate = tmpUpdateDate;
                    UCAction.UpdatedLoc = _datatable.Rows[0]["UPDATED_LOC"].ToString();
                }
            }
            else if (Action == EnumAction.Add)
            {
                tblpassword.Visible = true;
                txtUserID.Visible = true;
                lbUserID.Visible = false;
                resetlnk.Visible = false;
                lblAccStats2.Visible = true;
                ddlAccStats.Visible = false;
                lblAccStats2.Text = "Normal";
                ddlAccStats.SelectedValue = "0";

                if (Session["ULEVEL"] != null && Session["ULEVEL"].ToString() == "2")
                {
                    lbCompName.Visible = true;
                    lbCompName.Text = Session["COMPANYCODE"] != null ? Session["COMPANYCODE"].ToString() : string.Empty;
                    ddlCompName.Visible = false;
                    RBLevel1.Enabled = false;
                }
                else
                {
                    lbCompName.Visible = false;
                    ddlCompName.Visible = true;
                }
            }
        }
    }

    /// <summary>
    /// Handler for the Add/Edit Submit Function
    /// </summary>
    protected void UCAction_AddAction()
    {
        if (!RBLevel1.Checked && !RBLevel2.Checked && !RBLevel3.Checked)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msgbox", "alert('User Level Must be checked');", true);
        }
        else
        {
            string _temp = Library.Database.BLL.user.Maint(
                Key, ddlCompName.SelectedValue, txtName.Text, txtUserID.Text,
                txtDepartment.Text, txtEmail.Text,
                RBLevel1.Checked, RBLevel2.Checked, RBLevel3.Checked,
                GlobalFunctions.Encrypt(txtPassword.Text),
                ddlAccStats.SelectedValue, Convert.ToInt32(Action).ToString());

            if (_temp == "1")
            {
                Response.Redirect(GetUrl(EnumAction.None), false);
            }
            else if (_temp == "0")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, string.Format(Resources.Message.Failed, Action.ToString()));
            }
            else
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, _temp);
            }
        }
    }

    protected void UCAction_AddResetAction()
    {
        Response.Redirect(Request.RawUrl);
    }

    /// <summary>
    /// Delete Action
    /// </summary>
    protected void UCAction_DeleteAction()
    {
        string _temp = Library.Database.BLL.user.Maint(
            Key, "0", lblName.Text, lblUserID.Text, lblDepartment.Text, lblEmail.Text,
            RBLevel1.Checked, RBLevel2.Checked, RBLevel3.Checked,
            txtPassword.Text, ddlAccStats.SelectedValue,
            Convert.ToInt32(Library.Root.Control.Base.EnumAction.Delete).ToString());

        if (_temp == "1")
        {
            Response.Redirect(GetUrl(EnumAction.None), false);
        }
        else if (_temp == "0")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, string.Format(Resources.Message.Failed, Action.ToString()));
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, _temp);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void RBLevel1_CheckedChanged(object sender, EventArgs e)
    {
        RBLevel2.Checked = false;
        RBLevel3.Checked = false;
        TextBox1.Text = "1";
    }

    protected void RBLevel2_CheckedChanged(object sender, EventArgs e)
    {
        RBLevel1.Checked = false;
        RBLevel3.Checked = false;
        TextBox1.Text = "2";
    }

    protected void RBLevel3_CheckedChanged(object sender, EventArgs e)
    {
        RBLevel1.Checked = false;
        RBLevel2.Checked = false;
        TextBox1.Text = "3";
    }

    protected void ddlCompName_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void resetlnk_Click(object sender, EventArgs e)
    {
        string _temp = Library.Database.BLL.user.ResetPass(Key, "00YToB6QsF8IHDg0ts+HSw==XXrRKhkbJuN4oft7xknZmg==", "3");

        if (_temp == "1")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert",
                "alert('Your password has been reset successfully to abcd12345. Please proceed to change your password immediately.');window.location ='MM_USER.aspx';", true);
        }
        else if (_temp == "0")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, string.Format(Resources.Message.Failed, Action.ToString()));
        }
        else
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(Page, _temp);
        }
    }
}