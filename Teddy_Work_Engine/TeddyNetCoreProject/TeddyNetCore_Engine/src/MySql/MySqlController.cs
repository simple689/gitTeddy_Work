using System;
using MySql.Data.MySqlClient;
using System.Data;
using TeddyNetCore_EngineCore;

namespace TeddyNetCore_Engine {
    public class MySqlController {
        EngineBase _controller = null;

        public string _connectionStr;

        public void init(EngineBase controller) { // "keyName"=连接字符串名。
            _controller = controller;
        }

        public void setConnectionStr(string host, string port, string user, string password, string dataBase, string other) {
            string config = "Allow User Variables=True;";
            _connectionStr = string.Format("host={0};port={1};user id={2};password={3};database={4};{5};{6}", host, port, user, password, dataBase, config, other);
        }

        public int ExecuteNonQuery(string sqlStr) { // 执行SQL语句，返回影响的记录数。 "SQLString"=SQL语句。return=影响的记录数
            using (MySqlConnection connection = new MySqlConnection(_connectionStr)) {
                using (MySqlCommand cmd = new MySqlCommand(sqlStr, connection)) {
                    int rows = 0;
                    try {
                        connection.Open();
                        //rows = cmd.ExecuteNonQuery();
                        object obj = cmd.ExecuteScalar();
                        _controller.callBackLogPrint(obj.ToString());

                        cmd.Parameters.Clear();
                    } catch (MySqlException e) {
                        rows = -1;
                        throw e;
                    }
                    cmd.Dispose();
                    connection.Close();
                    return rows;
                }
            }
        }

        public int ExecuteNonQuery(string sqlStr, params MySqlParameter[] cmdParams) { // 执行SQL语句，返回影响的记录数。 "SQLString"=SQL语句。return=影响的记录数
            using (MySqlConnection connection = new MySqlConnection(_connectionStr)) {
                using (MySqlCommand cmd = new MySqlCommand(sqlStr, connection)) {
                    try {
                        PrepareCommand(cmd, connection, null, sqlStr, cmdParams);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    } catch (MySql.Data.MySqlClient.MySqlException e) {
                        throw e;
                    }
                }
            }
        }

        public object ExecuteScalar(string sql, params MySqlParameter[] parameters) {
            MySqlConnection con = new MySqlConnection(_connectionStr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            foreach (MySqlParameter parameter in parameters) {
                cmd.Parameters.Add(parameter);
            }
            object res = cmd.ExecuteScalar();
            cmd.Dispose();
            con.Close();
            return res;
        }

        //public DataTable ExecuteDataTable(string sql, params MySqlParameter[] parameters) {
        //    MySqlConnection con = new MySqlConnection(connectionString);
        //    con.Open();
        //    MySqlCommand cmd = new MySqlCommand(sql, con);
        //    foreach (MySqlParameter parameter in parameters) {
        //        cmd.Parameters.add(parameter);
        //    }
        //    DataSet dataset = new DataSet();//dataset放执行后的数据集合
        //    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
        //    adapter.Fill(dataset);
        //    cmd.Dispose();
        //    con.Close();
        //    return dataset.Tables[0];
        //}

        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, MySqlParameter[] cmdParams) { // 创建command 
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType; 
            if (cmdParams != null) {
                foreach (MySqlParameter parameter in cmdParams) {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                    (parameter.Value == null)) {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
    }
}
