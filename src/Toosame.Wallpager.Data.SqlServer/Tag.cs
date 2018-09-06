using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Toosame.Wallpager.Data.Interfaces;
using Toosame.Wallpager.Data.SqlServer.Service;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.SqlServer
{
    public class Tag : ITag
    {
        public IEnumerable<Model.Tag> GetTagByRandom(int count)
        {
            DataTable dataTable = DB.Client.ExecuteDataTable("SELECT TOP(@count) tag.tagId,tag.tagName FROM PictureTag tag ORDER BY NEWID()",
                new SqlParameter("@count", count));

            List<Model.Tag> tags = new List<Model.Tag>();

            foreach (DataRow item in dataTable.Rows)
                tags.Add(new Model.Tag(item));

            return tags;
        }

        public IEnumerable<PictureSummary> GetTagPictureById(int id, int index, int size)
        {
            DataTable dataTable = DB.Client.ExecuteDataTable(SqlServerClient.PagingBuild(@"
SELECT
		pic.picId,
		pic.picSize,
		pic.picNum,
		pic.picName,
		(SELECT TOP 1 picSrc.picPreview FROM PictureSource picSrc WHERE picSrc.picId = pic.picId) AS picPreview,
		Row_Number() OVER(ORDER BY pic.picId) RowNumber
FROM PictureTagMap map INNER JOIN Picture pic ON pic.picId = map.picId
WHERE map.tagId = @tag", index, size),
                new SqlParameter("@tag", id),
                new SqlParameter("@pageSize", size),
                new SqlParameter("@pageIndex", index));

            List<PictureSummary> pictures = new List<PictureSummary>();

            foreach (DataRow item in dataTable.Rows)
                pictures.Add(new PictureSummary(item));

            return pictures;
        }

        public IEnumerable<PictureSummary> GetTagPictureByName(string name, int index, int size)
        {
            DataTable dataTable = DB.Client.ExecuteDataTable(SqlServerClient.PagingBuild(@"
SELECT
		pic.picId,
		pic.picSize,
		pic.picNum,
		pic.picName,
		(SELECT TOP 1 picSrc.picPreview FROM PictureSource picSrc WHERE picSrc.picId = pic.picId) AS picPreview,
		Row_Number() OVER(ORDER BY pic.picId) RowNumber
FROM PictureTag tag
	INNER JOIN PictureTagMap map ON map.tagId = tag.tagId
	LEFT JOIN Picture pic ON pic.picId = map.picId
WHERE tag.tagName LIKE @tag", index, size),
                new SqlParameter("@tag", $"%{name}%"),
                new SqlParameter("@pageSize", size),
                new SqlParameter("@pageIndex", index));

            List<PictureSummary> pictures = new List<PictureSummary>();

            foreach (DataRow item in dataTable.Rows)
                pictures.Add(new PictureSummary(item));

            return pictures;
        }
    }
}
