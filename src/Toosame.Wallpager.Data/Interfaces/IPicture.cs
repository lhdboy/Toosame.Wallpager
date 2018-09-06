using System.Collections.Generic;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.Interfaces
{
    public interface IPicture
    {
        Picture GetPictureDetail(int id);

        IEnumerable<PictureSummary> GetPictureByIntro(string keyword, int index, int size);

        IEnumerable<PictureSummary> GetPictureByRandom(int count);

        Picture GetPictureDetailByRandom();
    }
}
