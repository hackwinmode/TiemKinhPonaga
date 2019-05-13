using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace TiemKinhPonaga
{
    class XulyDuLieu
    {
        static SQLiteConnection _con = new SQLiteConnection();

        public static void createConnection()
        {
            string _strConnect = "Datasource=TiemkinhPonaga.sqlite;Version=3";
            _con.ConnectionString = _strConnect;
            _con.Open();
        }

        public static void closeConnection()
        {
            _con.Close();
        }

        public static void createTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS tbl_khachhang ([id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, hovaten nvarchar(100), sodienthoai varchar(15), " +
                "SmatTrai varchar(10), AmatTrai varchar(10), CmatTrai varchar(10)," +
                "SmatPhai varchar(10), AmatPhai varchar(10), CmatPhai varchar(10), addMat varchar(10))";
            if (!File.Exists("TiemkinhPonaga.sqlite"))
            SQLiteConnection.CreateFile("TiemkinhPonaga.sqlite");
            createConnection();
            SQLiteCommand command = new SQLiteCommand(sql, _con);
            command.ExecuteNonQuery();
            closeConnection();
        }

        public static void insertTable(string Ten, string SDT, string SmatTrai, string AmatTrai, string CmatTrai, string SmatPhai, string AmatPhai, string CmatPhai, string Add)
        {
            string sql_Insert = string.Format("INSERT INTO tbl_khachhang(hovaten, sodienthoai, " +
                "SmatTrai, AmatTrai, CmatTrai," +
                "SmatPhai, AmatPhai, CmatPhai, addMat) VALUES " +
                "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",Ten,SDT, SmatTrai, AmatTrai, CmatTrai, SmatPhai, AmatPhai, CmatPhai, Add);
            createConnection();
            SQLiteCommand command = new SQLiteCommand(sql_Insert, _con);
            command.ExecuteNonQuery();
            closeConnection();           
        }

        public static void updateTable(string Ten, string SDT, string SmatTrai, string AmatTrai, string CmatTrai, string SmatPhai, string AmatPhai, string CmatPhai, string Add, int Id)
        {
               string sql_Insert = string.Format("UPDATE tbl_khachhang set " +
                   "hovaten = '{0}', " +
                   "sodienthoai = '{1}', " +
                    "SmatTrai = '{2}', " +
                    "AmatTrai = '{3}', " +
                    "CmatTrai = '{4}', " +
                    "SmatPhai = '{5}'," +
                    "AmatPhai = '{6}', " +
                    "CmatPhai = '{7}', " +
                    "addMat = '{8}' WHERE id =' {9}'" , Ten, SDT, SmatTrai, AmatTrai, CmatTrai,  SmatPhai, AmatPhai, CmatPhai, Add ,Id);
                createConnection();
                SQLiteCommand command = new SQLiteCommand(sql_Insert, _con);
                command.ExecuteNonQuery();
                closeConnection();
       
        }

        public static void deleteRecord(int Id)
        {
            string SQL = string.Format("DELETE FROM tbl_khachhang WHERE Id = '{0}'", Id);
            createConnection();
            SQLiteCommand command = new SQLiteCommand(SQL, _con);
            command.ExecuteNonQuery();
            closeConnection();
        }

        public static DataSet getData(string SQL)
        {
            DataSet ds = new DataSet();
            createConnection();
            SQLiteDataAdapter da = new SQLiteDataAdapter(SQL, _con);

            da.Fill(ds);
            closeConnection();
            return ds;
        }

    }
}
