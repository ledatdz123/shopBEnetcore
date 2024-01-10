using Dapper;
using Npgsql;
using System.Data;
using System.Runtime.CompilerServices;

namespace shopEcomerceExBE.Common.BaseService
{
    public class InitNpgConnection
    {
        private string connectionString;

        private NpgsqlConnection idbConn = null;

        private NpgsqlCommand cmdIDbCommand;

        private NpgsqlTransaction tsnTransaction = null;

        private string strTableName;

        private static string strPassword3 = "123456";
        private int ConnectionTimeout { get; set; }

        public void Connect()
        {
            Disconnect();
            idbConn = new NpgsqlConnection(connectionString);
            idbConn.Open();
        }

        public void Disconnect()
        {
            try
            {
                if (cmdIDbCommand != null)
                {
                    cmdIDbCommand.Dispose();
                }

                if (idbConn != null)
                {
                    idbConn.Close();
                    NpgsqlConnection.ClearPool(idbConn);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int GetTimeout(int? commandTimeout = null)
        {
            if (commandTimeout.HasValue)
            {
                return commandTimeout.Value;
            }

            return ConnectionTimeout;
        }

        private InitNpgConnection(string strConnect)
        {
            connectionString = strConnect;
        }
        public void CreateNewStoredProcedure(string strStoreName)
        {
            strTableName = strStoreName;
            cmdIDbCommand = SetCommand(strStoreName);
            cmdIDbCommand.CommandType = CommandType.StoredProcedure;
            cmdIDbCommand.CommandTimeout = 10;
        }
        public void AddParameter(string strParameterName, object objValue)
        {
            if (cmdIDbCommand != null)
            {
                cmdIDbCommand.Parameters.AddWithValue(FormatParameter(strParameterName), objValue ?? DBNull.Value);
            }
        }
        public static InitNpgConnection CreateData(string strConnect)
        {
            return new InitNpgConnection(strConnect);
        }
        private NpgsqlCommand SetCommand(string strSQL)
        {
            cmdIDbCommand = new NpgsqlCommand(strSQL, idbConn);
            if (tsnTransaction != null)
            {
                cmdIDbCommand.Transaction = tsnTransaction;
            }

            return cmdIDbCommand;
        }
        private string FormatParameter(string name)
        {
            if (name.IndexOf("@").Equals(0))
            {
                return name.Replace("@", "p_");
            }

            if (name.IndexOf("p_").Equals(0) || name.IndexOf("p_").Equals(0))
            {
                return name;
            }

            return "p_" + name;
        }
        public List<T> ExecStoreProcedureToList<T>() where T : class
        {
            using NpgsqlTransaction npgsqlTransaction = idbConn.BeginTransaction();
            try
            {
                cmdIDbCommand.Transaction = npgsqlTransaction;
                string text = string.Empty;
                using (NpgsqlDataReader npgsqlDataReader = cmdIDbCommand.ExecuteReader())
                {
                    if (npgsqlDataReader.Read())
                    {
                        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
                        defaultInterpolatedStringHandler.AppendLiteral("FETCH ALL IN \"");
                        defaultInterpolatedStringHandler.AppendFormatted<object>(npgsqlDataReader[0]);
                        defaultInterpolatedStringHandler.AppendLiteral("\";");
                        text = defaultInterpolatedStringHandler.ToStringAndClear();
                    }
                }

                NpgsqlConnection cnn = idbConn;
                string sql = text;
                CommandType? commandType = CommandType.Text;
                return cnn.Query<T>(sql, null, npgsqlTransaction, buffered: true, null, commandType).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                npgsqlTransaction?.Commit();
            }
        }
        public string ExecStoreToString()
        {
            bool flag = false;
            if (tsnTransaction == null || tsnTransaction.Connection == null)
            {
                flag = true;
                tsnTransaction = idbConn.BeginTransaction();
            }

            try
            {
                cmdIDbCommand.Transaction = tsnTransaction;
                string text = string.Empty;
                using (NpgsqlDataReader npgsqlDataReader = cmdIDbCommand.ExecuteReader())
                {
                    if (npgsqlDataReader.Read())
                    {
                        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
                        defaultInterpolatedStringHandler.AppendLiteral("FETCH ALL IN \"");
                        defaultInterpolatedStringHandler.AppendFormatted<object>(npgsqlDataReader[0]);
                        defaultInterpolatedStringHandler.AppendLiteral("\";");
                        text = defaultInterpolatedStringHandler.ToStringAndClear();
                    }
                }

                return idbConn.QueryFirstOrDefault<string>(text, null, commandType: CommandType.Text, transaction: tsnTransaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (flag && tsnTransaction.Connection != null)
                {
                    tsnTransaction.Commit();
                    tsnTransaction = null;
                }
            }
        }
        public T ExecStoreRefToObject<T>() where T : class
        {
            using NpgsqlTransaction npgsqlTransaction = idbConn.BeginTransaction();
            try
            {
                cmdIDbCommand.Transaction = npgsqlTransaction;
                string text = string.Empty;
                using (NpgsqlDataReader npgsqlDataReader = cmdIDbCommand.ExecuteReader())
                {
                    if (npgsqlDataReader.Read())
                    {
                        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
                        defaultInterpolatedStringHandler.AppendLiteral("FETCH ALL IN \"");
                        defaultInterpolatedStringHandler.AppendFormatted<object>(npgsqlDataReader[0]);
                        defaultInterpolatedStringHandler.AppendLiteral("\";");
                        text = defaultInterpolatedStringHandler.ToStringAndClear();
                    }
                }

                NpgsqlConnection cnn = idbConn;
                string sql = text;
                CommandType? commandType = CommandType.Text;
                return cnn.QueryFirstOrDefault<T>(sql, null, npgsqlTransaction, null, commandType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                npgsqlTransaction?.Commit();
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
