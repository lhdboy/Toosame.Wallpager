using System.Data;
using System.Collections.Generic;
using System;

namespace Toosame.Wallpager.Model
{
    public class Picture : PictureSummary
    {
        public Picture() { }

        public Picture(DataRow dataRow) : base(dataRow)
        {
            Name = dataRow["picName"].ToString();
            Intro = dataRow["picIntro"].ToString();

            ChannelId = Convert.ToInt32(dataRow["picChannel"]);
            TypeId = Convert.ToInt32(dataRow["picType"]);

            ChannelName = dataRow[nameof(ChannelName)].ToString();
            TypeName = dataRow[nameof(TypeName)].ToString();

            Images = new List<PictureImage>();

            Tags = new List<Tag>();
        }

        public string Name { get; set; } 

        public string Intro { get; set; }

        public int ChannelId { get; set; }

        public string ChannelName { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public List<PictureImage> Images { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
