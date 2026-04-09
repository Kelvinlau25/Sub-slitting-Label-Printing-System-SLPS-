namespace Library.Database.BLL
{
    public class LogService : Library.Root.Other.BusinessLogicBase
    {
        public static ListCollection GetLogList(string Table, string Key, int Page, string Sort = "")
        {
            ListCollection result = new ListCollection();

            using (var _dal = new DAL.Log())
            {
                result = _dal.getLogList(Table, Key, FromRowNo(Page), ToRowNo(Page), Sort);
            }
            return result;
        }
    }
}
