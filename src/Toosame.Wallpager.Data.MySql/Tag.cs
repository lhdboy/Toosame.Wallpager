using System.Collections.Generic;
using Toosame.Wallpager.Data.Interfaces;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.MySql
{
    public class Tag : ITag
    {
        public IEnumerable<Model.Tag> GetTagByRandom(int count)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PictureSummary> GetTagPictureById(int id, int index, int size)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PictureSummary> GetTagPictureByName(string name, int index, int size)
        {
            throw new System.NotImplementedException();
        }
    }
}
