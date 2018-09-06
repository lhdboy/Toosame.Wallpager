using System;
using System.Data;

namespace Toosame.Wallpager.Model
{
    public class Channel
    {
        public Channel() { }

        public Channel(DataRow dataRow)
        {
            Id = Convert.ToInt32(dataRow["channelId"]);
            Name = dataRow["channelName"].ToString();
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
