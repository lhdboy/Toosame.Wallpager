using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Toosame.Wallpager.Model
{
    public class PictureType
    {
        public PictureType() { }

        public PictureType(DataRow dataRow)
        {
            TypeId = Convert.ToInt32(dataRow[nameof(TypeId)]);
            TypeName = dataRow[nameof(TypeName)].ToString();
        }

        public int TypeId { get; set; }

        public string TypeName { get; set; }
    }
}
