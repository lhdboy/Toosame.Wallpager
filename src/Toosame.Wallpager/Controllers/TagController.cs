using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Toosame.Wallpager.Data;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Controllers
{
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly IDataSourceService _dataSourceService;

        public TagController(IDataSourceService dataSourceService)
        {
            _dataSourceService = dataSourceService;
        }

        /// <summary>
        /// 根据标签获取图片列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{tag}")]
        public IEnumerable<PictureSummary> Get(string tag, int index = 1, int size = 20)
        {
            if (index < 1) index = 1;
            if (size < 5 || size > 100) size = 20;

            if (int.TryParse(tag, out int tagId))
                return _dataSourceService.Tag.GetTagPictureById(tagId, index, size);

            return _dataSourceService.Tag.GetTagPictureByName(tag.Trim(), index, size);
        }

        /// <summary>
        /// 随机返回标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<Tag> Get(int count)
        {
            if (count < 1 || count > 50) count = 8;

            return _dataSourceService.Tag.GetTagByRandom(count);
        }
    }
}
