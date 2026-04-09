using System.Collections.Generic;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    public class ColorDetail
    {
        public string ID;
        public string SerialNo;
        public string ColorSeq;
        public string ColorWay;
        public int ContractQty;
    }

    [WebMethod]
    public List<ColorDetail> GetColorDetailList()
    {
        var list = new List<ColorDetail>();

        for (int i = 0; i <= 100; i++)
        {
            var temp = new ColorDetail
            {
                ID = "" + i,
                SerialNo = "SerialNo" + i,
                ColorSeq = "ColorSeq" + i,
                ColorWay = "ColorWay" + i,
                ContractQty = i
            };
            list.Add(temp);
        }

        return list;
    }
}