using System;
using System.Data;
using System.Data.SqlClient;

namespace Library.SQLServer
{
    /// <summary>
    /// Handler the SQL Server Connection Class
    /// -------------------------------------------
    /// C.C.Yeon    25 April 2011   Initial Version
    /// </summary>
    public abstract class Connection : IDisposable
    {
        protected SqlConnection _con;
        protected SqlCommand _cmd;
        protected SqlDataReader _rdr;
        protected SqlTransaction _tran;
        protected SqlDataAdapter _sqladp;

        private string _constr = string.Empty;
        public string ConnectionString
        {
            get { return _constr; }
            set { _constr = value; }
        }

        public Connection(string ConnectionStringName)
        {
            this.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName].ToString();

            if (ConnectionString == string.Empty)
            {
                throw new Exception("Invalid Connection String Name That Set At Web Config");
            }

            this._con = new SqlConnection(this.ConnectionString);
            this._con.Open();
            this._cmd = _con.CreateCommand();
            this._tran = this._con.BeginTransaction();
            this._cmd.Transaction = this._tran;
        }

        public string Status
        {
            get
            {
                if (_con != null)
                {
                    return _con.State.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Commit all the transaction
        /// </summary>
        public void Commit()
        {
            this._tran.Commit();
        }

        /// <summary>
        /// Rollback all the transaction
        /// </summary>
        public void Rollback()
        {
            this._tran.Rollback();
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: free other state (managed objects).
                }

                if (_rdr != null)
                {
                    _rdr.Dispose();
                }

                if (_cmd != null)
                {
                    _cmd.Dispose();
                }

                if (_con != null)
                {
                    if (_con.State == ConnectionState.Open)
                    {
                        _con.Close();
                    }

                    _con.Dispose();
                }
            }
            this.disposedValue = true;
        }

        #region IDisposable Support
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
