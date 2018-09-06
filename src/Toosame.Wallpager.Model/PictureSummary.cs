using System;
using System.Data;

namespace Toosame.Wallpager.Model
{
    public class PictureSummary
    {
        public PictureSummary() { }

        public PictureSummary(DataRow dataRow)
        {
            PicId = Convert.ToInt32(dataRow[nameof(PicId)]);
            PicName = dataRow[nameof(PicName)].ToString();
            PicNum = dataRow[nameof(PicNum)].ToString();
            PicSize = dataRow[nameof(PicSize)].ToString();
            PicPreview = dataRow[nameof(PicPreview)].ToString();
        }

        public int PicId { get; set; }

        public string PicName { get; set; }

        public string PicNum { get; set; }

        public string PicSize { get; set; }

        public string PicPreview { get; set; }
    }
}
