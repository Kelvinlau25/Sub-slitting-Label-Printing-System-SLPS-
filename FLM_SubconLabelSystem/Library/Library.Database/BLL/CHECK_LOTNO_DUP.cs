namespace Library.Database.BLL
{
    public class CHECK_LOTNO_DUP
    {
        public static string check_lotno_dup(string pstr_CompanyCode, string pstr_LotNo)
        {
            using (var _dal = new DAL.CHECK_LOTNO_DUP())
            {
                return _dal.check_lotno_dup(pstr_CompanyCode, pstr_LotNo);
            }
        }
    }
}
