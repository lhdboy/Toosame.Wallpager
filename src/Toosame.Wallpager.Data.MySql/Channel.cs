using System.Collections.Generic;
using Toosame.Wallpager.Data.Interfaces;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.MySql
{
    public class Channel : IChannel
    {
        public IEnumerable<Model.Channel> GetAllChannel()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PictureSummary> GetChannelPicture(int id, int index, int size)
        {
            throw new System.NotImplementedException();
        }
    }
}
