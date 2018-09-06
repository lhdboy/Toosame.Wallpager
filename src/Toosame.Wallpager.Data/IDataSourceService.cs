using Toosame.Wallpager.Data.Interfaces;

namespace Toosame.Wallpager.Data
{
    public interface IDataSourceService
    {
        IChannel Channel { get; set; }

        IPicture Picture { get; set; }

        ITag Tag { get; set; }

        IPictureType PictureType { get; set; }
    }
}
