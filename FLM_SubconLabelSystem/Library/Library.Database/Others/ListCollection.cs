using System.Data;

namespace Library.Database
{
    public class ListCollection
    {
        private int _TotalRow = 0;
        public int TotalRow
        {
            get { return _TotalRow; }
            set { _TotalRow = value; }
        }

        private DataTable _Data = new DataTable();
        public DataTable Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
    }
}
