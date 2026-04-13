using System;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class PRINT_ALIGN_INITModel : BasePageModel
    {
        private readonly IConfiguration _configuration;

        public PRINT_ALIGN_INITModel(IConfiguration configuration)
        {
            _configuration = configuration;
            SetupKey = "PRINT_ALIGN_INIT";
            DefaultSort = "ID_Print_Align_Init";
            SortDirection = "0";
            DeleteControl = true;
            PrintControl = false;
            ViewHistoryControl = true;
            RecordTypeColumn = 6;
        }

        public override void BindData()
        {
            string ulevel = SessionGet("ULEVEL") ?? "";
            string companyCode = SessionGet("COMPANYCODE") ?? "";
            DeleteControl = ulevel != "2";

            Library.Database.ListCollection list;

            if (ulevel == "3")
            {
                list = Library.Database.BLL.PrintAlignInit.List(
                    "Print_Align_Init_func('" + companyCode + "')",
                    "ID_Print_Align_Init",
                    SearchField, SearchValue, SortField,
                    int.Parse(SortDirection), PageNo,
                    ShowDeleted ? 1 : 0);
            }
            else
            {
                list = Library.Database.BLL.PC1.List(
                    "PV_PRINT_ALIGN_INIT",
                    "ID_Print_Align_Init",
                    SearchField, SearchValue, SortField,
                    int.Parse(SortDirection), PageNo,
                    ShowDeleted ? 1 : 0);
            }

            GridData = list.Data;
            TotalRecords = list.TotalRow;
        }

        public IActionResult OnGetDownloadExe()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "printlabelling.zip");
            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/zip", "printlabelling.zip");
            }
            return NotFound();
        }

        public IActionResult OnGetDownloadSettings()
        {
            string constr = _configuration.GetConnectionString("PFR_Label_DB");
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from PRINT_ALIGN_INIT Where Default_Printer = 1 And REC_TYPE != 5"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            StringBuilder csv = new StringBuilder();

                            foreach (DataColumn column in dt.Columns)
                            {
                                csv.Append(column.ColumnName + ",");
                            }
                            csv.AppendLine();

                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    csv.Append(row[column.ColumnName].ToString().Replace(",", ";") + ",");
                                }
                                csv.AppendLine();
                            }

                            byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                            return File(bytes, "application/text", "settings.csv");
                        }
                    }
                }
            }
        }
    }
}
