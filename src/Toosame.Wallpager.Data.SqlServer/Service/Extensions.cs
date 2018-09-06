using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Toosame.Wallpager.Data.SqlServer.Service
{
    public static class Extensions
    {
        #region 创建表SQL
        private const string PictureCreateSql = @"
CREATE TABLE [dbo].[Picture](
	[picId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[picName] [nvarchar](128) NOT NULL,
	[picIntro] [nvarchar](max) NOT NULL,
	[picSize] [varchar](12) NOT NULL,
	[picChannel] [int] NOT NULL,
	[picType] [int] NOT NULL,
	[picNum] [int] NOT NULL)";

        private const string PictureChannelCreateSql = @"
CREATE TABLE [dbo].[PictureChannel](
	[channelId] [int] IDENTITY(1000,1) NOT NULL PRIMARY KEY,
	[channelName] [nvarchar](32) NOT NULL)";

        private const string PictureSourceCreateSql = @"
CREATE TABLE [dbo].[PictureSource](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[picId] [int] NOT NULL,
	[picUrl] [varchar](max) NOT NULL,
	[picPreview] [varchar](max) NOT NULL)";

        private const string PictureTagCreateSql = @"
CREATE TABLE [dbo].[PictureTag](
	[tagId] [int] IDENTITY(3000000,1) NOT NULL PRIMARY KEY,
	[tagName] [nvarchar](64) NOT NULL)";

        private const string PictureTagMapCreateSql = @"
CREATE TABLE [dbo].[PictureTagMap](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[picId] [int] NOT NULL,
	[tagId] [int] NOT NULL)";

        private const string PictureTypeCreateSql = @"
CREATE TABLE [dbo].[PictureType](
	[typeId] [int] IDENTITY(200000,1) NOT NULL PRIMARY KEY,
	[typeName] [nvarchar](18) NOT NULL)";
        #endregion

        public static IWebHostBuilder UseSqlServer(this IWebHostBuilder webHost, string connString)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    //创建Picture表
                    if (CreateTable(conn, tran, "Picture", PictureCreateSql))
                    {
                        tran.Rollback();
                        throw new Exception("表Picture创建失败");
                    }

                    //创建PictureChannel表
                    if (CreateTable(conn, tran, "PictureChannel", PictureChannelCreateSql))
                    {
                        tran.Rollback();
                        throw new Exception("表PictureChannel创建失败");
                    }

                    //创建PictureSource表
                    if (CreateTable(conn, tran, "PictureSource", PictureSourceCreateSql))
                    {
                        tran.Rollback();
                        throw new Exception("表PictureSource创建失败");
                    }

                    //创建PictureTag表
                    if (CreateTable(conn, tran, "PictureTag", PictureTagCreateSql))
                    {
                        tran.Rollback();
                        throw new Exception("表PictureTag创建失败");
                    }

                    //创建PictureTagMap表
                    if (CreateTable(conn, tran, "PictureTagMap", PictureTagMapCreateSql))
                    {
                        tran.Rollback();
                        throw new Exception("表PictureTagMap创建失败");
                    }

                    //创建PictureType表
                    if (CreateTable(conn, tran, "PictureType", PictureTypeCreateSql))
                    {
                        tran.Rollback();
                        throw new Exception("表PictureType创建失败");
                    }

                    tran.Commit();
                }
            }

            DB.Client = new SqlServerClient(connString);

            webHost.ConfigureServices(service =>
            {
                service.AddTransient<IDataSourceService, SqlServerDataSourceService>();
            });

            return webHost;
        }

        private static bool CreateTable(SqlConnection conn, SqlTransaction sqlTransaction, string tableName, string sql)
        {
            int tableColCount = 0;
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"SELECT COUNT(1) FROM SYSCOLUMNS WHERE ID=OBJECT_ID('{tableName}')";
                cmd.Transaction = sqlTransaction;
                tableColCount = (int)cmd.ExecuteScalar();
            }

            if (tableColCount <= 0)
            {
                try
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Transaction = sqlTransaction;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
