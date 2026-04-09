using System;
using System.Data;

public partial class Transactions_Housekeeping : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        rfddlDataRet.ErrorMessage = string.Format(Resources.Message.FieldEmpty, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ddlDataRet.SelectedValue = "";
        }
    }

    protected void ddlDataRet_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime currDate = DateTime.Today;
        if (ddlDataRet.SelectedValue == "")
        {
            lbCurrDate.Text = "";
        }
        else
        {
            DateTime reqDate = currDate.AddDays(-Convert.ToDouble(ddlDataRet.SelectedValue));
            lbCurrDate.Text = reqDate.ToString("dd/MM/yyyy");
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string childSucc = "Y";
        string motSucc = "Y";
        string lotSucc = "Y";
        string subslitDone = "N";
        string lotNoDone = "N";

        if (ddlDataRet.SelectedValue == "" || lbCurrDate.Text == "")
        {
            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Please select Data Retention");
        }
        else
        {
            string pDatePurge;
            string[] preDate = lbCurrDate.Text.Split('/');
            pDatePurge = preDate[2] + "-" + preDate[1] + "-" + preDate[0];

            string pCompany;
            if (Session["ULEVEL"].ToString() == "3")
            {
                return;
            }
            else
            {
                pCompany = " ";
            }

            //---------- SUBSLIT PURGE ----------------
            //**** CHILD ****
            DataTable dtSubSlitChild = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "SSCHILD");
            for (int i = 0; i < dtSubSlitChild.Rows.Count; i++)
            {
                string temp = Library.Database.BLL.HouseKeep.DelSubSlitChild(dtSubSlitChild.Rows[i]["ID_SUBSLIT_REQUEST_CHILD"].ToString(), "SSCHILD");

                if (temp == "1")
                {
                    // Success
                }
                else
                {
                    childSucc = "N";
                    if (temp == "0")
                    {
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "There is an error on purging SUBSLIT CHILD!");
                    }
                    else
                    {
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, temp);
                    }
                }
            }

            //**** MOTHER ****
            if (childSucc == "Y")
            {
                DataTable dtSubSlitMother = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "SSMOTHER");
                for (int j = 0; j < dtSubSlitMother.Rows.Count; j++)
                {
                    string temp2 = Library.Database.BLL.HouseKeep.DelSubSlitChild(dtSubSlitMother.Rows[j]["SUBSLIT_REQ_MOTHER_SEQNO"].ToString(), "SSMOTHER");

                    if (temp2 == "1")
                    {
                        // Success
                    }
                    else
                    {
                        motSucc = "N";
                        if (temp2 == "0")
                        {
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "There is an error on purging SUBSLIT MOTHER!");
                        }
                        else
                        {
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, temp2);
                        }
                    }
                }
            }

            //**** MAIN ****
            if (motSucc == "Y")
            {
                DataTable dtSubSlitMain = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "SSMAIN");
                for (int k = 0; k < dtSubSlitMain.Rows.Count; k++)
                {
                    string temp3 = Library.Database.BLL.HouseKeep.DelSubSlitChild(dtSubSlitMain.Rows[k]["ID_SUBSLIT_REQUEST"].ToString(), "SSMAIN");

                    if (temp3 == "1")
                    {
                        subslitDone = "Y";
                    }
                    else
                    {
                        if (temp3 == "0")
                        {
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "There is an error on purging SUBSLIT MOTHER!");
                        }
                        else
                        {
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, temp3);
                        }
                    }
                }
            }

            //------------------ LOTNO PURGE ---------------------
            DataTable dtLotSlit = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "LOTSLIT");
            for (int m = 0; m < dtLotSlit.Rows.Count; m++)
            {
                string temp4 = Library.Database.BLL.HouseKeep.DelSubSlitChild(dtLotSlit.Rows[m]["ID_LOT_SLITTING"].ToString(), "LOTSLIT");

                if (temp4 == "1")
                {
                    // Success
                }
                else
                {
                    lotSucc = "N";
                    if (temp4 == "0")
                    {
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "There is an error on purging LOT SLITTING!");
                    }
                    else
                    {
                        Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, temp4);
                    }
                }
            }

            //**** pc2_lotno ****
            if (lotSucc == "Y")
            {
                DataTable dtPC2Lot = Library.Database.BLL.HouseKeep.GetSubSlitChild(pCompany, pDatePurge, "PC2LOT");
                for (int n = 0; n < dtPC2Lot.Rows.Count; n++)
                {
                    string temp5 = Library.Database.BLL.HouseKeep.DelSubSlitChild(dtPC2Lot.Rows[n]["ID_PC2_LOTNO"].ToString(), "PC2LOT");

                    if (temp5 == "1")
                    {
                        lotNoDone = "Y";
                    }
                    else
                    {
                        if (temp5 == "0")
                        {
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "There is an error on purging PC2 LOTNO!");
                        }
                        else
                        {
                            Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, temp5);
                        }
                    }
                }
            }

            if (lotNoDone == "Y" && subslitDone == "Y")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Successfully Purge Data");
            }
            else if (lotNoDone == "Y" && subslitDone == "N")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Successfully Purge Lot No data");
            }
            else if (lotNoDone == "N" && subslitDone == "Y")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "Successfully Purge Subslit data");
            }
            else if (lotNoDone == "N" && subslitDone == "N")
            {
                Library.Root.Control.MessageCenter.ShowAJAXMessageBox(this.Page, "No data was purge.");
            }
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.End();
    }
}