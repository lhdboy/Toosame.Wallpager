using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Toosame.Wallpager.Model
{
    public class Tag
    {
        public Tag() { }

        public Tag(DataRow dataRow)
        {
            TagId = Convert.ToInt32(dataRow[nameof(TagId)]);
            TagName = dataRow[nameof(TagName)].ToString();
        }

        public int TagId { get; set; }

        public string TagName { get; set; }
    }
}
