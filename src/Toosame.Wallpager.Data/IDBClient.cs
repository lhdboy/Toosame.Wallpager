using System;
using System.Data;

namespace Toosame.Wallpager.Data
{
    public interface IDBClient
    {
        int ExecuteNonQuery(string sql, params object[] parameters);

        object ExecuteScalar(string sql, params object[] parameters);

        DataTable ExecuteDataTable(string sql, params object[] parameters);

        DataSet ExecuteDataSet(string sql, params object[] parameters);
    }
}
