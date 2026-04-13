using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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

        private static IConfiguration _appConfiguration;

        /// <summary>
        /// Set the ASP.NET Core IConfiguration so that connection strings
        /// defined in appsettings.json are available to the legacy DAL layer.
        /// Call this once at application startup.
        /// </summary>
        public static void SetConfiguration(IConfiguration configuration)
        {
            _appConfiguration = configuration;
        }

        private string _constr = string.Empty;
        public string ConnectionString
        {
            get { return _constr; }
            set { _constr = value; }
        }

        public Connection(string ConnectionStringName)
        {
            // Try ASP.NET Core configuration first, then fall back to ConfigurationManager
            string connStr = null;
            if (_appConfiguration != null)
            {
                connStr = _appConfiguration.GetConnectionString(ConnectionStringName);
            }
            if (string.IsNullOrEmpty(connStr))
            {
                var setting = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringName];
                connStr = setting?.ToString();
            }

            this.ConnectionString = connStr ?? string.Empty;

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
