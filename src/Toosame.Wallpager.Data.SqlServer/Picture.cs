using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Toosame.Wallpager.Data.Interfaces;
using Toosame.Wallpager.Data.SqlServer.Service;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.SqlServer
{
    public class Picture : IPicture
    {
        public IEnumerable<PictureSummary> GetPictureByIntro(string keyword, int index, int size)
        {
            DataTable dataTable = DB.Client.ExecuteDataTable(SqlServerClient.PagingBuild(@"
SELECT 
    pic.picId,
    pic.picSize,
    pic.picNum,
    pic.picName,
    (SELECT TOP 1 picSrc.picPreview FROM PictureSource picSrc WHERE picSrc.picId = pic.picId) AS picPreview,
	Row_Number() OVER(ORDER BY pic.picId) RowNumber
FROM Picture pic WHERE pic.picIntro LIKE @keyword", index, size),
                new SqlParameter("@keyword", $"%{keyword}%"),
                new SqlParameter("@pageSize", size),
                new SqlParameter("@pageIndex", index));

            List<PictureSummary> pictures = new List<PictureSummary>();

            foreach (DataRow item in dataTable.Rows)
                pictures.Add(new PictureSummary(item));

            return pictures;
        }

        public IEnumerable<PictureSummary> GetPictureByRandom(int count)
        {
            DataTable dataTable = DB.Client.ExecuteDataTable(@"
SELECT TOP(@count)
    pic.picId,
    pic.picSize,
    pic.picNum,
    pic.picName,
    (SELECT TOP 1 picSrc.picPreview FROM PictureSource picSrc WHERE picSrc.picId = pic.picId) AS picPreview,
	Row_Number() OVER(ORDER BY pic.picId) RowNumber
FROM Picture pic ORDER BY NEWID()", new SqlParameter("@count", count));

            List<PictureSummary> pictures = new List<PictureSummary>();

            foreach (DataRow item in dataTable.Rows)
                pictures.Add(new PictureSummary(item));

            return pictures;
        }

        public Model.Picture GetPictureDetail(int id)
        {
            //查出图片详情
            DataTable pictureDataTable = DB.Client.ExecuteDataTable(@"
SELECT TOP 1
    pic.picId,
    pic.picSize,
    pic.picNum,
    pic.picName,
    pic.picChannel,
    pic.picType,
    pic.picIntro,
    (SELECT TOP 1 chan.channelName FROM PictureChannel chan WHERE chan.channelId = pic.picChannel) AS ChannelName,
    (SELECT TOP 1 typ.typeName FROM PictureType typ WHERE typ.typeId = pic.picType) AS TypeName,
    (SELECT TOP 1 picSrc.picPreview FROM PictureSource picSrc WHERE picSrc.picId = pic.picId) AS picPreview
FROM Picture pic WHERE pic.picId = @id"
            , new SqlParameter("@id", id));

            if (pictureDataTable.Rows.Count <= 0)
                return null;

            return FillPictureInfo(new Model.Picture(pictureDataTable.Rows[0])); ;
        }

        public Model.Picture GetPictureDetailByRandom()
        {
            //查出图片详情
            DataTable pictureDataTable = DB.Client.ExecuteDataTable(@"
SELECT TOP 1
    pic.picId,
    pic.picSize,
    pic.picNum,
    pic.picName,
    pic.picChannel,
    pic.picType,
    pic.picIntro,
    (SELECT TOP 1 chan.channelName FROM PictureChannel chan WHERE chan.channelId = pic.picChannel) AS ChannelName,
    (SELECT TOP 1 typ.typeName FROM PictureType typ WHERE typ.typeId = pic.picType) AS TypeName,
    (SELECT TOP 1 picSrc.picPreview FROM PictureSource picSrc WHERE picSrc.picId = pic.picId) AS picPreview
FROM Picture pic WHERE pic.picType = 200000 AND pic.picSize = '1920x1080' AND pic.picNum > 1 ORDER BY NEWID()");

            if (pictureDataTable.Rows.Count <= 0)
                return null;

            return FillPictureInfo(new Model.Picture(pictureDataTable.Rows[0]));
        }

        private Model.Picture FillPictureInfo(Model.Picture picture)
        {
            //查询图片的所有原始图和预览图
            DataTable images = DB.Client.ExecuteDataTable("SELECT src.picPreview,src.picUrl FROM PictureSource src WHERE src.picId = @pid"
                , new SqlParameter("@pid", picture.PicId));

            foreach (DataRow item in images.Rows)
                picture.Images.Add(new PictureImage(item));

            //查询图片的标签
            DataTable tags = DB.Client.ExecuteDataTable("SELECT tag.tagId,tag.tagName FROM PictureTag tag WHERE tag.tagId IN(SELECT map.tagId FROM PictureTagMap map WHERE map.picId = @pid)"
                , new SqlParameter("@pid", picture.PicId));

            foreach (DataRow item in tags.Rows)
                picture.Tags.Add(new Model.Tag(item));

            return picture;
        }
    }
}
