using System.Collections.Generic;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Data.Interfaces
{
    public interface IChannel
    {
        IEnumerable<Channel> GetAllChannel();

        IEnumerable<PictureSummary> GetChannelPicture(int id, int index, int size);
    }
}
