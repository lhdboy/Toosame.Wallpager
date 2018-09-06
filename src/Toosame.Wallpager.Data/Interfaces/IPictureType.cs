using System;
using System.Collections.Generic;
using System.Text;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.Interfaces
{
    public interface IPictureType
    {
        IEnumerable<PictureSummary> GetPictureByType(int typeId, int index, int size);

        IEnumerable<PictureType> GetAllPictureType();
    }
}
