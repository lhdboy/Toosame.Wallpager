using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Toosame.Wallpager.Data;
using Toosame.Wallpager.Model;

namespace Toosame.Wallpager.Controllers
{
    [Route("api/[controller]")]
    public class PictureController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly IDataSourceService _dataSourceService;

        public PictureController(IMemoryCache cache,
            IDataSourceService dataSourceService)
        {
            _cache = cache;
            _dataSourceService = dataSourceService;
        }

        /// <summary>
        /// 获取今日壁纸
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Picture Get()
        {
            if (!_cache.TryGetValue("today_time", out DateTime dateTime))
            {
                var todayPic = _dataSourceService.Picture.GetPictureDetailByRandom();
                _cache.Set("today_pic", todayPic);
                _cache.Set("today_time", DateTime.UtcNow);

                return todayPic;
            }

            TimeSpan timeSpan = DateTime.UtcNow - dateTime;

            if (timeSpan.TotalDays > 1 || !_cache.TryGetValue("today_pic", out Picture todayPicture))
            {
                var todayPic = _dataSourceService.Picture.GetPictureDetailByRandom();
                _cache.Set("today_pic", todayPic);
                _cache.Set("today_time", DateTime.UtcNow);

                return todayPic;
            }

            return todayPicture;
        }

        /// <summary>
        /// 获取单张图片详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Picture Detail(int id)
            => _dataSourceService.Picture.GetPictureDetail(id);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<PictureSummary> Search(string keyword, int index = 1, int size = 20)
        {
            if (index < 1) index = 1;
            if (size < 5 || size > 100) size = 20;

            //先确定用户输入的关键词是不是一个意思很广的词语，比如：“手机壁纸”，”电脑壁纸“
            int findType = FindCommon(keyword);
            if (findType > 0)
                return _dataSourceService.PictureType.GetPictureByType(findType, index, size);

            //如果不是一个大范围，则先搜索标签
            var tagResult = _dataSourceService.Tag.GetTagPictureByName(keyword, index, size);
            if (tagResult != null && tagResult is List<PictureSummary> tagSum && tagSum.Count > 0)
                return tagResult;

            //如果标签找不到，则搜索图片介绍全文
            return _dataSourceService.Picture.GetPictureByIntro(keyword, index, size);
        }

        /// <summary>
        /// 随机推荐
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<PictureSummary> Recommend(int count = 25)
            => _dataSourceService.Picture.GetPictureByRandom(count);

        /// <summary>
        /// 查找类似
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private int FindCommon(string keyword)
        {
            if (!_cache.TryGetValue("pic_type", out List<PictureType> picTypes))
            {
                picTypes = (List<PictureType>)_dataSourceService.PictureType.GetAllPictureType();
                _cache.Set("pic_type", picTypes);
            }

            string[] similarKeyword = new string[] { "壁纸", "图片", "桌面", "背景", "墙纸" };

            //从用户条件中搜索是否存在广义关键词
            foreach (string item in similarKeyword)
            {
                if (keyword.Contains(item))
                {
                    string typeKeyword = keyword.Replace(item, "");
                    foreach (string rightStr in similarKeyword)
                    {
                        var findPicType = picTypes.Find(f => f.TypeName == typeKeyword + rightStr);
                        if (findPicType != null)
                        {
                            return findPicType.TypeId;
                        }
                    }

                    return 0;
                }
            }

            return 0;
        }
    }
}
