using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Toosame.Wallpager.Data;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Controllers
{
    [Route("api/[controller]")]
    public class ChannelController : Controller
    {
        private readonly IDataSourceService _dataSourceService;

        public ChannelController(IDataSourceService dataSourceService)
        {
            _dataSourceService = dataSourceService;
        }

        /// <summary>
        /// 获取所有频道
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<Channel> Get()
            => _dataSourceService.Channel.GetAllChannel();

        [HttpGet("{id}")]
        public IEnumerable<PictureSummary> Get(int id, int index = 1, int size = 20)
        {
            if (index < 1) index = 1;
            if (size < 5 || size > 100) size = 20;

            return _dataSourceService.Channel.GetChannelPicture(id, index, size);
        }
    }
}
