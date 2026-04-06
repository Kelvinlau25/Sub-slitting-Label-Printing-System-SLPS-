using Oracle.DataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Web.Configuration;

namespace cls_DB
{
    public class cls_Oracle
    {
        private OracleConnection dbOraConn;

        private OracleCommand dbOraCmd;

        private OracleParameter dbOraParam;

        private string str_connstr;

        private OracleDataReader dbOraDr;

        private string str_LastErroMsg;

        private string str_DB_Tag;

        public cls_Oracle(string pStr_DB_Tag_or_ConnStr) : base()
        {
            this.dbOraConn = null;
            this.dbOraCmd = null;
            this.dbOraParam = null;
            this.str_connstr = "";
            this.dbOraDr = null;
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

        public string executeActionQuery(string pstrQuery, string[,] parr_str_param, bool pbol_StoreProc)
        {
            return this.n_executeActionQuery(pstrQuery, parr_str_param, pbol_StoreProc);
        }

        public OracleDataReader executeQuery(string pstrQuery, string[,] parr_str_param, bool pbol_StoreProc)
        {
            return this.n_executeQuery(pstrQuery, parr_str_param, pbol_StoreProc);
        }

        public string get_LastError()
        {
            return this.str_LastErroMsg;
        }

        public OracleConnection getDBConn()
        {
            return this.dbOraConn;
        }

        private void Locate_ConnStr(string pStr_DB_Tag_or_ConnStr)
        {
            string str = this.chk_config_file_auto(pStr_DB_Tag_or_ConnStr);
            if (string.IsNullOrEmpty(str))
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
            this.dbOraCmd.Parameters.Clear();
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
                            Oracle.DataAccess.Client.OracleParameter oracleParameter = new Oracle.DataAccess.Client.OracleParameter()
                            {
                                ParameterName = parr_str_param[i, 0],
                                Value = parr_str_param[i, 1]
                            };
                            this.dbOraCmd.Parameters.Add(oracleParameter);
                        }
                    }
                    flag = false;
                    break;
                }
                else if (!string.IsNullOrEmpty(parr_str_param[i, 0]))
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
                if (!string.IsNullOrEmpty(this.str_DB_Tag))
                {
                    this.str_connstr = this.chk_config_file_auto(this.str_DB_Tag);
                }
                this.dbOraConn = new OracleConnection(this.str_connstr);
                this.dbOraConn.Open();
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
            if (this.dbOraConn != null)
            {
                if (!string.IsNullOrEmpty(this.str_DB_Tag))
                {
                    this.str_connstr = this.chk_config_file_auto(this.str_DB_Tag);
                }
                flag = this.active_connection();
            }
            else if (this.dbOraConn != null)
            {
                flag = (int)this.dbOraConn.State != 1 ? false : true;
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
                this.dbOraCmd = new OracleCommand()
                {
                    Connection = this.dbOraConn
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
                this.dbOraDr.Close();
                this.dbOraDr.Dispose();
                this.dbOraDr = null;
            }
            catch (System.Exception)
            {
            }
            try
            {
                this.dbOraCmd.Parameters.Clear();
                this.dbOraCmd.Dispose();
                this.dbOraCmd = null;
            }
            catch (System.Exception)
            {
            }
        }

        private void n_dispose_dbConn()
        {
            this.dbOraConn.Close();
            this.dbOraConn.Dispose();
        }

        private string n_executeActionQuery(string pstrQuery, string[,] parr_str_param, bool pbol_StoreProc)
        {
            string str;
            this.str_LastErroMsg = "";
            try
            {
                if (!this.n_check_conn_status())
                {
                    try
                    {
                        this.dbOraConn.Dispose();
                    }
                    catch (System.Exception)
                    {
                    }
                    this.dbOraConn = null;
                    this.active_connection();
                }
                if (this.dbOraCmd == null)
                {
                    if (this.n_crete_command())
                    {
                        str = true.ToString();
                        return str;
                    }
                }
                if (!pbol_StoreProc)
                {
                    this.dbOraCmd.CommandType = (CommandType)1;
                }
                else
                {
                    this.dbOraCmd.CommandType = (CommandType)4;
                }
                if (parr_str_param == null)
                {
                    this.dbOraCmd.Parameters.Clear();
                    this.dbOraCmd.Parameters.Add(new OracleParameter("RETURN_VALUE", OracleDbType.Int64, 20)).Direction = (ParameterDirection)2;
                    dbOraCmd.Parameters["RETURN_VALUE"].DbType = DbType.Int64;
                }
                else if (!this.locate_parameter(parr_str_param))
                {
                    this.dbOraCmd.Parameters.Add(new OracleParameter("RETURN_VALUE", OracleDbType.Int64, 20)).Direction = (ParameterDirection)2;
                    dbOraCmd.Parameters["RETURN_VALUE"].DbType = DbType.Int64;
                }
                else
                {
                    str = true.ToString();
                    return str;
                }
                this.dbOraCmd.CommandText = pstrQuery;
                this.dbOraCmd.ExecuteNonQuery();
                string str1 = this.dbOraCmd.Parameters["RETURN_VALUE"].Value.ToString();
                str = str1;
            }
            catch (System.Exception exception1)
            {
                this.str_LastErroMsg = exception1.ToString();
                str = true.ToString();
            }
            return str;
        }

        private OracleDataReader n_executeQuery(string pstrQuery, string[,] parr_str_param, bool pbol_StoreProc)
        {
            OracleDataReader oracleDataReaders;
            this.str_LastErroMsg = "";
            try
            {
                if (!this.n_check_conn_status())
                {
                    try
                    {
                        this.dbOraConn.Dispose();
                    }
                    catch (System.Exception)
                    {
                    }
                    this.dbOraConn = null;
                    this.active_connection();
                }
                if (this.dbOraCmd == null)
                {
                    if (this.n_crete_command())
                    {
                        oracleDataReaders = null;
                        return oracleDataReaders;
                    }
                }
                if (!pbol_StoreProc)
                {
                    this.dbOraCmd.CommandType = (CommandType)1;
                }
                else
                {
                    this.dbOraCmd.CommandType = (CommandType)4;
                }
                if (parr_str_param == null)
                {
                    this.dbOraCmd.Parameters.Clear();
                    this.dbOraCmd.Parameters.Add(new OracleParameter("sref", OracleDbType.RefCursor)).Direction = (ParameterDirection)2;
                }
                else if (!this.locate_parameter(parr_str_param))
                {
                    this.dbOraCmd.Parameters.Add(new OracleParameter("sref", OracleDbType.RefCursor)).Direction = (ParameterDirection)2;
                }
                else
                {
                    oracleDataReaders = null;
                    return oracleDataReaders;
                }
                this.dbOraCmd.CommandText = pstrQuery;
                this.dbOraDr = this.dbOraCmd.ExecuteReader();
                oracleDataReaders = this.dbOraDr;
            }
            catch (System.Exception exception1)
            {
                this.str_LastErroMsg = exception1.ToString();
                oracleDataReaders = null;
            }
            return oracleDataReaders;
        }
    }
}
