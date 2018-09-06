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
        /// ��ȡ���ձ�ֽ
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
        /// ��ȡ����ͼƬ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Picture Detail(int id)
            => _dataSourceService.Picture.GetPictureDetail(id);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<PictureSummary> Search(string keyword, int index = 1, int size = 20)
        {
            if (index < 1) index = 1;
            if (size < 5 || size > 100) size = 20;

            //��ȷ���û�����Ĺؼ����ǲ���һ����˼�ܹ�Ĵ�����磺���ֻ���ֽ���������Ա�ֽ��
            int findType = FindCommon(keyword);
            if (findType > 0)
                return _dataSourceService.PictureType.GetPictureByType(findType, index, size);

            //�������һ����Χ������������ǩ
            var tagResult = _dataSourceService.Tag.GetTagPictureByName(keyword, index, size);
            if (tagResult != null && tagResult is List<PictureSummary> tagSum && tagSum.Count > 0)
                return tagResult;

            //�����ǩ�Ҳ�����������ͼƬ����ȫ��
            return _dataSourceService.Picture.GetPictureByIntro(keyword, index, size);
        }

        /// <summary>
        /// ����Ƽ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<PictureSummary> Recommend(int count = 25)
            => _dataSourceService.Picture.GetPictureByRandom(count);

        /// <summary>
        /// ��������
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

            string[] similarKeyword = new string[] { "��ֽ", "ͼƬ", "����", "����", "ǽֽ" };

            //���û������������Ƿ���ڹ���ؼ���
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
