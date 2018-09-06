using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Toosame.Wallpager.Data.Interfaces;
using Toosame.Wallpager.Data.SqlServer.Service;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.SqlServer
{
    public class PictureType : IPictureType
    {
        public IEnumerable<PictureSummary> GetPictureByType(int typeId, int index, int size)
        {
            DataTable dataTable = DB.Client.ExecuteDataTable(SqlServerClient.PagingBuild(@"
SELECT 
    pic.picId,
    pic.picSize,
    pic.picNum,
    pic.picName,
    (SELECT TOP 1 picSrc.picPreview FROM PictureSource picSrc WHERE picSrc.picId = pic.picId) AS picPreview,
	Row_Number() OVER(ORDER BY pic.picId) RowNumber
FROM Picture pic WHERE pic.picType = @typeId", index, size),
                new SqlParameter("@typeId", typeId),
                new SqlParameter("@pageSize", size),
                new SqlParameter("@pageIndex", index));

            List<PictureSummary> pictures = new List<PictureSummary>();

            foreach (DataRow item in dataTable.Rows)
                pictures.Add(new PictureSummary(item));

            return pictures;
        }

        public IEnumerable<Model.PictureType> GetAllPictureType()
        {
            DataTable dataTable = DB.Client.ExecuteDataTable("SELECT typ.typeId,typ.typeName FROM PictureType typ");

            List<Model.PictureType> pictureTypes = new List<Model.PictureType>();

            foreach (DataRow item in dataTable.Rows)
                pictureTypes.Add(new Model.PictureType(item));

            return pictureTypes;
        }
    }
}
