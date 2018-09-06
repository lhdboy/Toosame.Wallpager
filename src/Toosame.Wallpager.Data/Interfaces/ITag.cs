using System.Collections.Generic;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.Interfaces
{
    public interface ITag
    {
        IEnumerable<PictureSummary> GetTagPictureByName(string name, int index, int size);

        IEnumerable<PictureSummary> GetTagPictureById(int id, int index, int size);

        IEnumerable<Tag> GetTagByRandom(int count);
    }
}
