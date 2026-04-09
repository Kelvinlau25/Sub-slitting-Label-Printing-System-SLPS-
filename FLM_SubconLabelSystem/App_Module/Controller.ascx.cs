using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Module_Controller : System.Web.UI.UserControl
{
    public enum DisplayType
    {
        Full = 3,
        Half = 2,
        Name = 1,
        ID = 0
    }

    #region Audit

    private string _createdCompanyCode = string.Empty;
    public string CreatedCompanyCode { get { return _createdCompanyCode; } set { _createdCompanyCode = value; } }

    private string _createdBy = string.Empty;
    public string CreatedBy { get { return _createdBy; } set { _createdBy = value; } }

    private DateTime _createdDate = DateTime.Now;
    public DateTime CreatedDate { get { return _createdDate; } set { _createdDate = value; } }

    private string _createdLoc = string.Empty;
    public string CreatedLoc { get { return _createdLoc; } set { _createdLoc = value; } }

    private string _updatedCompanyCode = string.Empty;
    public string UpdatedCompanyCode { get { return _updatedCompanyCode; } set { _updatedCompanyCode = value; } }

    private string _updatedBy = string.Empty;
    public string UpdatedBy { get { return _updatedBy; } set { _updatedBy = value; } }

    private DateTime _updatedDate = DateTime.Now;
    public DateTime UpdatedDate { get { return _updatedDate; } set { _updatedDate = value; } }

    private string _updatedLoc = string.Empty;
    public string UpdatedLoc { get { return _updatedLoc; } set { _updatedLoc = value; } }

    #endregion

    private string _connectionstring = "PFR_Label_DB";
    public string connectionstring { get { return _connectionstring; } set { _connectionstring = value; } }

    private string _dateTimeFormat = "dd / MMM / yyyy hh:mm:ss";
    public string DateTimeFormat { get { return _dateTimeFormat; } set { _dateTimeFormat = value; } }

    /// <summary>
    /// Full - Display Company Code + Employee Name + ID
    /// Half - Display Employee Name + ID
    /// Name - Display Employee Name
    /// ID - Display ID
    /// </summary>
    private DisplayType _auditTrailDisplayType = DisplayType.ID;
    public DisplayType AuditTrailDisplayType { get { return _auditTrailDisplayType; } set { _auditTrailDisplayType = value; } }

    /// <summary>
    /// Control the ability of the button
    /// </summary>
    private bool _editMode = true;
    public bool EditMode { get { return _editMode; } set { _editMode = value; } }

    public string ValidationGroup { get; set; }

    private bool _add = true;
    public bool Add { get { return _add; } set { _add = value; } }

    private bool _edit = true;
    public bool Edit { get { return _edit; } set { _edit = value; } }

    private bool _delete = true;
    public bool Delete { get { return _delete; } set { _delete = value; } }

    private bool _history = true;
    public bool History { get { return _history; } set { _history = value; } }

    private string _listKey = string.Empty;
    public string ListKey { get { return _listKey; } set { _listKey = value; } }

    public event Action AddAction;
    public event Action EditAction;
    public event Action DeleteAction;
    public event Action AddResetAction;
    public event Action EditResetAction;
    public event Action ViewEditAction;
    public event Action ModifyMode;
    public event Action DisplayMode;

    protected void Page_Load(object sender, EventArgs e)
    {
        Control.Base setting = (Control.Base)this.Page;

        hpLink.NavigateUrl = setting.GetUrl(Control.Base.EnumAction.History, this.ListKey);

        switch (setting.Action)
        {
            case Control.Base.EnumAction.Add:
                btnDelete.Visible = false;
                hpLink.Visible = false;
                pnconfirmation.Visible = false;
                pninfo.Visible = false;

                if (ModifyMode != null) ModifyMode();

                if (Add == false)
                    Response.Redirect(setting.GetUrl(Control.Base.EnumAction.None));
                break;

            case Control.Base.EnumAction.Delete:
                hpLink.Visible = false;
                btnReset.Visible = false;
                btnDelete.Visible = false;

                if (DisplayMode != null) DisplayMode();

                if (Delete == false)
                    Response.Redirect(setting.GetUrl(Control.Base.EnumAction.None));
                break;

            case Control.Base.EnumAction.Edit:
                hpLink.Visible = false;
                btnDelete.Visible = false;
                pnconfirmation.Visible = false;

                if (ModifyMode != null) ModifyMode();

                if (Edit == false)
                    Response.Redirect(setting.GetUrl(Control.Base.EnumAction.None));
                break;

            case Control.Base.EnumAction.View:
                btnSubmit.Text = "Edit";
                btnReset.Visible = false;
                btnSubmit.CausesValidation = false;
                btnDelete.Visible = false;
                pnconfirmation.Visible = false;

                string setKey = Session["Setkey"] != null ? Session["Setkey"].ToString() : null;
                int uLevel = Convert.ToInt32(Session["ULEVEL"]);

                if (setKey == "PC2_LOTNO" || setKey == "VIEW_LOT_SLITTING_SERIES" || setKey == "PRINT_ALIGN_INIT")
                {
                    if (uLevel == 3 || uLevel == 1)
                    {
                        btnDelete.Visible = true;
                        btnSubmit.Visible = true;
                    }
                    else
                    {
                        btnDelete.Visible = false;
                        btnSubmit.Visible = false;
                    }
                }
                else
                {
                    if (uLevel == 3 || uLevel == 2)
                    {
                        if (uLevel == 2 && setKey == "MM_PC2")
                        {
                            btnDelete.Visible = true;
                            btnSubmit.Visible = true;
                        }
                        else
                        {
                            btnDelete.Visible = false;
                            btnSubmit.Visible = false;
                        }
                    }
                    else
                    {
                        btnDelete.Visible = true;
                        btnSubmit.Visible = true;
                    }
                }

                if (DisplayMode != null) DisplayMode();
                break;
        }

        if (!string.IsNullOrEmpty(this.ValidationGroup))
        {
            btnSubmit.ValidationGroup = ValidationGroup;
            cvdeleteyes.ValidationGroup = ValidationGroup;
            cvdeleteyes.ErrorMessage = Resources.Message.Deletemessage;
        }

        if (pninfo.Visible)
        {
            if (!IsPostBack)
            {
                if (AuditTrailDisplayType == DisplayType.Full ||
                    AuditTrailDisplayType == DisplayType.Half ||
                    AuditTrailDisplayType == DisplayType.Name)
                {
                    if (string.IsNullOrEmpty(CreatedBy))
                        CreatedBy = "N/A";

                    if (string.IsNullOrEmpty(UpdatedBy))
                        UpdatedBy = "N/A";
                }

                string createdText = string.Empty;
                string updatedText = string.Empty;

                if (AuditTrailDisplayType != DisplayType.Name)
                {
                    createdText = GenerateText(createdText, " ID : " + this.CreatedBy);
                    updatedText = GenerateText(updatedText, " ID : " + this.UpdatedBy);
                }

                lblcreatedby.Text = createdText;
                lblcreateddate.Text = this.CreatedDate.ToString(this.DateTimeFormat);
                lblcreatedloc.Text = this.CreatedLoc;

                lblupdatedby.Text = updatedText;
                lblupdateddate.Text = this.UpdatedDate.ToString(this.DateTimeFormat);
                lblUpdatedloc.Text = this.UpdatedLoc;
            }
        }

        if (this.EditMode == false)
        {
            btnSubmit.Visible = false;
            btnDelete.Visible = false;
        }
    }

    private string GenerateText(string value, string addText)
    {
        if (value.Length > 0)
            return value + " - " + addText;

        return addText;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Control.Base setting = (Control.Base)this.Page;
        Response.Redirect(setting.GetUrl(Control.Base.EnumAction.None));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (DeleteAction != null) DeleteAction();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Control.Base setting = (Control.Base)this.Page;
        switch (setting.Action)
        {
            case Control.Base.EnumAction.Delete:
                if (DeleteAction != null) DeleteAction();
                break;
            case Control.Base.EnumAction.Add:
                if (AddAction != null) AddAction();
                break;
            case Control.Base.EnumAction.Edit:
                if (EditAction != null) EditAction();
                break;
            case Control.Base.EnumAction.View:
                if (ViewEditAction != null) ViewEditAction();
                Response.Redirect(setting.GetUrl(Control.Base.EnumAction.Edit));
                break;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Control.Base setting = (Control.Base)this.Page;
        switch (setting.Action)
        {
            case Control.Base.EnumAction.Add:
                if (AddResetAction != null) AddResetAction();
                break;
            case Control.Base.EnumAction.Edit:
                if (EditResetAction != null) EditResetAction();
                break;
        }
    }

    protected void cvdeleteyes_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = rbyes.Checked;
    }
}