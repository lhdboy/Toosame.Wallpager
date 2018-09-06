using System.Collections.Generic;
using Toosame.Wallpager.Data.Interfaces;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.MySql
{
    public class Picture : IPicture
    {
        public IEnumerable<PictureSummary> GetPictureByIntro(string keyword, int index, int size)
        {
            throw new System.NotImplementedException();
        }

        public Model.Picture GetPictureByType(int index, int size, params string[] keyword)
        {
            throw new System.NotImplementedException();
        }

        public Model.Picture GetPictureDetail(int id)
        {
            throw new System.NotImplementedException();
        }

        public Model.Picture GetPictureDetailByRandom(int count)
        {
            throw new System.NotImplementedException();
        }
    }
}
