using System;
using System.Collections.Generic;
using System.Text;
using Toosame.Wallpager.Data.Interfaces;

namespace Toosame.Wallpager.Data.SqlServer.Service
{
    public class SqlServerDataSourceService : IDataSourceService
    {
        public IChannel Channel { get; set; }
        public IPicture Picture { get; set; }
        public ITag Tag { get; set; }
        public IPictureType PictureType { get; set; }

        public SqlServerDataSourceService()
        {
            Channel = new Channel();
            Picture = new Picture();
            Tag = new Tag();
            PictureType = new PictureType();
        }
    }
}
