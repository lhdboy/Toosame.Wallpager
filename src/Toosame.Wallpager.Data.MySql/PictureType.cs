using System;
using System.Collections.Generic;
using System.Text;
using Toosame.Wallpager.Data.Interfaces;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.MySql
{
    public class PictureType : IPictureType
    {
        public IEnumerable<PictureSummary> GetPictureByType(int typeId, int index, int size)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Model.PictureType> IPictureType.GetAllPictureType()
        {
            throw new NotImplementedException();
        }
    }
}
