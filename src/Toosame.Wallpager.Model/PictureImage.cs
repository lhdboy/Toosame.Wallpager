using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Toosame.Wallpager.Model
{
    public class PictureImage
    {
        public PictureImage() { }

        public PictureImage(DataRow dataRow)
        {
            Preview = dataRow["picPreview"].ToString();
            Url = dataRow["picUrl"].ToString();
        }

        public string Preview { get; set; }

        public string Url { get; set; }
    }
}
