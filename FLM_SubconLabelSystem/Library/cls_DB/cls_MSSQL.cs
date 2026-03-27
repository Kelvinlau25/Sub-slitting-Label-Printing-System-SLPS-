using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace cls_DB
{
    public class cls_MSSQL
    {
        private SqlConnection dbConn;

        private SqlCommand dbCmd;

        private SqlParameter dbParam;

        private string str_connstr;

        private SqlDataReader dbDr;

        private string str_LastErroMsg;

        private string str_DB_Tag;

        public cls_MSSQL(string pStr_DB_Tag_or_ConnStr) : base()
        {
            this.dbConn = null;
            this.dbCmd = null;
            this.dbParam = null;
            this.str_connstr = "";
            this.dbDr = null;
            this.str_LastErroMsg = "";
            this.str_DB_Tag = "";
            this.Locate_ConnStr(pStr_DB_Tag_or_ConnStr);
        }

        public bool active_connection()
        {
            return this.n_active_connection();
        }

        public bool active_connection(string pStr_DB_Tag_or_ConnStr)
        {
            this.Locate_ConnStr(pStr_DB_Tag_or_ConnStr);
            return this.n_active_connection();
        }

        public void Begin_Trans()
        {
            if (!this.n_check_conn_status())
            {
                try
                {
                    this.dbConn.Dispose();
                }
                catch (System.Exception)
                {
                }
                this.dbConn = null;
                this.active_connection();
            }
            this.dbConn.BeginTransaction();
        }

        public bool check_conn_status()
        {
            return this.n_check_conn_status();
        }

        private string chk_config_file_auto(string pStr_DB_Tag)
        {
            string connectionString;
            this.str_LastErroMsg = "";
            try
            {
                string lower = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.ToLower();
                if (lower.IndexOf(string.Concat(AppDomain.CurrentDomain.FriendlyName.ToLower(), ".config")) > 0)
                {
                    connectionString = ConfigurationManager.ConnectionStrings[pStr_DB_Tag].ConnectionString;
                    return connectionString;
                }
                else if (lower.IndexOf("web.config") > 0)
                {
                    connectionString = WebConfigurationManager.ConnectionStrings[pStr_DB_Tag].ConnectionString;
                    return connectionString;
                }
                else if (lower.IndexOf("app.config") > 0)
                {
                    connectionString = ConfigurationManager.ConnectionStrings[pStr_DB_Tag].ConnectionString;
                    return connectionString;
                }
            }
            catch (System.Exception exception)
            {
                this.str_LastErroMsg = exception.ToString();
            }
            connectionString = "";
            return connectionString;
        }

        public void Commit_Trans()
        {
            this.dbCmd.Transaction.Commit();
        }

        public bool crete_command()
        {
            return this.n_crete_command();
        }

        public void dispose_command()
        {
            this.n_dispose_command();
        }

        public void dispose_dbConn()
        {
            this.n_dispose_dbConn();
        }

        public bool executeActionQuery(string pstrQuery, string[,] parr_str_param, bool pbol_StoreProc)
        {
            return this.n_executeActionQuery(pstrQuery, parr_str_param, pbol_StoreProc);
        }

        public SqlDataReader executeQuery(string pstrQuery, string[,] parr_str_param, bool pbol_StoreProc)
        {
            return this.n_executeQuery(pstrQuery, parr_str_param, pbol_StoreProc);
        }

        public string get_LastError()
        {
            return this.str_LastErroMsg;
        }

        public SqlConnection getDBConn()
        {
            return this.dbConn;
        }

        private void Locate_ConnStr(string pStr_DB_Tag_or_ConnStr)
        {
            string str = this.chk_config_file_auto(pStr_DB_Tag_or_ConnStr);
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "", false) == 0)
            {
                this.str_connstr = pStr_DB_Tag_or_ConnStr;
                this.str_DB_Tag = "";
            }
            else
            {
                this.str_connstr = str;
                this.str_DB_Tag = pStr_DB_Tag_or_ConnStr;
            }
        }

        private bool locate_parameter(string[,] parr_str_param)
        {
            bool flag;
            int i = 0;
            bool flag1 = false;
            this.dbCmd.Parameters.Clear();
            int length = parr_str_param.GetLength(0) - 1;
            i = 0;
            while (true)
            {
                if (i > length)
                {
                    if (!flag1)
                    {
                        int num = parr_str_param.GetLength(0) - 1;
                        for (i = 0; i <= num; i++)
                        {
                            System.Data.SqlClient.SqlParameter sqlParameter = new System.Data.SqlClient.SqlParameter()
                            {
                                ParameterName = parr_str_param[i, 0],
                                Value = parr_str_param[i, 1]
                            };
                            this.dbCmd.Parameters.Add(sqlParameter);
                        }
                    }
                    flag = false;
                    break;
                }
                else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(parr_str_param[i, 0], "", false) != 0)
                {
                    flag1 = false;
                    i = i + 1;
                }
                else
                {
                    flag1 = true;
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private bool n_active_connection()
        {
            bool flag;
            this.str_LastErroMsg = "";
            try
            {
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.str_DB_Tag, "", false) != 0)
                {
                    this.str_connstr = this.chk_config_file_auto(this.str_DB_Tag);
                }
                this.dbConn = new SqlConnection(this.str_connstr);
                this.dbConn.Open();
                flag = true;
            }
            catch (System.Exception exception)
            {
                this.str_LastErroMsg = exception.ToString();
                flag = false;
            }
            return flag;
        }

        private bool n_check_conn_status()
        {
            bool flag;
            if (this.dbConn != null)
            {
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.str_DB_Tag, "", false) != 0)
                {
                    this.str_connstr = this.chk_config_file_auto(this.str_DB_Tag);
                }
                flag = this.active_connection();
            }
            else if (this.dbConn != null)
            {
                flag = this.dbConn.State != ConnectionState.Open ? false : true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private bool n_crete_command()
        {
            bool flag;
            this.str_LastErroMsg = "";
            try
            {
                this.dbCmd = new SqlCommand()
                {
                    Connection = this.dbConn
                };
                flag = false;
            }
            catch (System.Exception exception)
            {
                this.str_LastErroMsg = exception.ToString();
                flag = true;
            }
            return flag;
        }

        private void n_dispose_command()
        {
            try
            {
                this.dbDr.Close();
                this.dbDr.Dispose();
                this.dbDr = null;
            }
            catch (System.Exception)
            {
            }
            try
            {
                this.dbCmd.Parameters.Clear();
                this.dbCmd.Dispose();
                this.dbCmd = null;
            }
            catch (System.Exception)
            {
            }
        }

        private void n_dispose_dbConn()
        {
            this.dbConn.Close();
            this.dbConn.Dispose();
        }

        private bool n_executeActionQuery(string pstrQuery, string[,] parr_str_param, bool pbol_StoreProc)
        {
            bool flag;
            this.str_LastErroMsg = "";
            try
            {
                if (!this.n_check_conn_status())
                {
                    try
                    {
                        this.dbConn.Dispose();
                    }
                    catch (System.Exception)
                    {
                    }
                    this.dbConn = null;
                    this.active_connection();
                }
                if (this.dbCmd == null)
                {
                    if (this.n_crete_command())
                    {
                        flag = true;
                        return flag;
                    }
                }
                if (!pbol_StoreProc)
                {
                    this.dbCmd.CommandType = CommandType.Text;
                }
                else
                {
                    this.dbCmd.CommandType = CommandType.StoredProcedure;
                }
                if (parr_str_param != null)
                {
                    if (this.locate_parameter(parr_str_param))
                    {
                        flag = true;
                        return flag;
                    }
                }
                this.dbCmd.CommandText = pstrQuery;
                this.dbCmd.ExecuteNonQuery();
                flag = false;
            }
            catch (System.Exception exception1)
            {
                this.str_LastErroMsg = exception1.ToString();
                flag = true;
            }
            return flag;
        }

        private System.Data.SqlClient.SqlDataReader n_executeQuery(string pstrQuery, string[,] parr_str_param, bool pbol_StoreProc)
        {
            System.Data.SqlClient.SqlDataReader sqlDataReader;
            this.str_LastErroMsg = "";
            try
            {
                if (!this.n_check_conn_status())
                {
                    try
                    {
                        this.dbConn.Dispose();
                    }
                    catch (System.Exception)
                    {
                    }
                    this.dbConn = null;
                    this.active_connection();
                }
                if (this.dbCmd == null)
                {
                    if (this.n_crete_command())
                    {
                        sqlDataReader = null;
                        return sqlDataReader;
                    }
                }
                if (!pbol_StoreProc)
                {
                    this.dbCmd.CommandType = CommandType.Text;
                }
                else
                {
                    this.dbCmd.CommandType = CommandType.StoredProcedure;
                }
                if (parr_str_param == null)
                {
                    this.dbCmd.Parameters.Clear();
                }
                else if (this.locate_parameter(parr_str_param))
                {
                    sqlDataReader = null;
                    return sqlDataReader;
                }
                this.dbCmd.CommandText = pstrQuery;
                this.dbDr = this.dbCmd.ExecuteReader();
                sqlDataReader = this.dbDr;
            }
            catch (System.Exception exception1)
            {
                this.str_LastErroMsg = exception1.ToString();
                sqlDataReader = null;
            }
            return sqlDataReader;
        }

        public void Rollback_Trans()
        {
            this.dbCmd.Transaction.Rollback();
        }
    }
}
