using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Toosame.Wallpager.Data.Interfaces;
using Toosame.Wallpager.Data.SqlServer.Service;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.SqlServer
{
    public class Channel : IChannel
    {
        public IEnumerable<Model.Channel> GetAllChannel()
        {
            DataTable dataTable = DB.Client.ExecuteDataTable("SELECT channelId,channelName FROM PictureChannel");

            List<Model.Channel> channels = new List<Model.Channel>();

            foreach (DataRow item in dataTable.Rows)
                channels.Add(new Model.Channel(item));

            return channels;
        }

        public IEnumerable<PictureSummary> GetChannelPicture(int id, int index, int size)
        {
            DataTable dataTable = DB.Client.ExecuteDataTable(SqlServerClient.PagingBuild(@"
SELECT 
    pic.picId,
    pic.picSize,
    pic.picNum,
    pic.picName,
    (SELECT TOP 1 picSrc.picPreview FROM PictureSource picSrc WHERE picSrc.picId = pic.picId) AS picPreview,
    Row_Number() OVER(ORDER BY pic.picId) RowNumber
FROM Picture pic WHERE pic.picChannel = @channelId", index, size),
new SqlParameter("@channelId", id),
new SqlParameter("@pageSize", size),
new SqlParameter("@pageIndex", index));

            List<PictureSummary> channels = new List<PictureSummary>();

            foreach (DataRow item in dataTable.Rows)
                channels.Add(new PictureSummary(item));

            return channels;
        }
    }
}
