using System;
using System.Collections;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PFRLabelIssuing.Pages.MasterMaint
{
    public class ExportModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ExportModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            ArrayList slitSlotNoArray = null;
            if (HttpContext.Session.GetString("SlitSlotNoArray_json") != null)
            {
                slitSlotNoArray = System.Text.Json.JsonSerializer.Deserialize<ArrayList>(
                    HttpContext.Session.GetString("SlitSlotNoArray_json"));
            }

            // CheckBoxUpdate
            if (slitSlotNoArray != null)
            {
                for (int j = 0; j <= slitSlotNoArray.Count - 1; j++)
                {
                    string upd_stat = Library.Database.BLL.LotSlitting.UpdPrintSel(
                        slitSlotNoArray[j].ToString(), true, "Update");
                }
            }

            // CsvDownload
            string constr = _configuration.GetConnectionString("PFR_Label_DB");

            using (SqlConnection con = new SqlConnection(constr))
            using (SqlCommand cmd = new SqlCommand(
                "select a.* from VIEW_LOT_SLITTING_SERIES a Inner join LOT_SLITTING b on a.SLIT_LOT_NO = b.SLIT_LOT_NO AND a.ID_LOT_SLITTING = b.ID_LOT_SLITTING where (b.PRINT_SEL = 1) And (a.REC_TYPE = 1 OR a.REC_TYPE = 3)"))
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;

                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        var sb = new StringBuilder();

                        foreach (DataColumn column in dt.Columns)
                        {
                            sb.Append(column.ColumnName + ",");
                        }
                        sb.Append("\r\n");

                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (DataColumn column in dt.Columns)
                            {
                                if (column.ColumnName == "LOTNO" || column.ColumnName == "SLIT_LOT_NO")
                                {
                                    sb.Append(row[column.ColumnName].ToString().Replace(",", ";") + "A" + ",");
                                }
                                else
                                {
                                    sb.Append(row[column.ColumnName].ToString().Replace(",", ";") + ",");
                                }
                            }
                            sb.Append("\r\n");
                        }

                        // CheckBoxInit
                        if (slitSlotNoArray != null)
                        {
                            for (int j = 0; j <= slitSlotNoArray.Count - 1; j++)
                            {
                                Library.Database.BLL.LotSlitting.UpdPrintSel(
                                    slitSlotNoArray[j].ToString(), true, "Init");
                            }
                        }

                        byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
                        return File(bytes, "application/text", "printlabel.csv");
                    }
                }
            }

            return Page();
        }
    }
}
