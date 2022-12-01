using Content.Core.Entities.Media;
using Content.EndPoint.Models.Media;
using Content.EndPoint.Models.MediaModels;
using Content.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Content.EndPoint.Controllers.Media
{
    [ApiController]
    public class MediaController : ApiController
    {
        private readonly Context _context;
        private readonly Ram.Ram _ram;

        public MediaController(Context context, Ram.Ram ram)
        {
            _context = context;
            _ram = ram;
        }

        [HttpPost("add-media")]
        public async Task<IActionResult> AddMedia(AddMediaModel req)
        {
            var presenter = _context.Set<Presenter>().SingleOrDefault(p => p.Id == req.PresenterId);
            if (presenter == null)
                return NotFound("ارائه دهنده مورد نظر ثبت نشده است!");

            string attrs = string.Empty;

            foreach (var item in req.Attributes.OrderBy(p => p.Key).ThenBy(p => p.Value))
            {
                attrs += (item.Key + ":" + item.Value + "-");
            }
            attrs = attrs.Substring(0, attrs.Length - 1);

            var media = new Core.Entities.Media.Media
            {
                Attributes = attrs,
                CreatedTime = DateTime.Now,
                Description = req.Description,
                IsActive = req.IsActive,
                Poster = req.Poster,
                Src = req.Src,
                Title = req.Title,
                PresenterId = req.PresenterId,
                Type = req.Type,
                ProductCode = req.ProductCode,
                IsFree = req.Price == 0
            };

            await _context.Set<Core.Entities.Media.Media>().AddAsync(media);
            
            await _context.SaveChangesAsync();

            foreach (var item in req.Attributes)
            {
                if(_ram.TagInRams.Any(p => p.TagId == item.Value && p.ExpirationDate > DateTime.Now))
                {
                    _ram.MediaInRams.Add(new Models.RamModels.MediaInRam
                    {
                        MediaId = media.Id,
                        Attributes = media.Attributes
                    });
                }
            }

            foreach (var item in _ram.TagInRams)
            {

            }

            return Ok(media.Id);
        }

        [HttpPost("update-media")]
        public async Task<IActionResult> UpdateMedia(UpdateMediaModel req)
        {
            var presenter = _context.Set<Presenter>().SingleOrDefault(p => p.Id == req.PresenterId);
            if (presenter == null)
                return NotFound("ارائه دهنده مورد نظر ثبت نشده است!");

            var media = _context.Set<Core.Entities.Media.Media>().SingleOrDefault(p => p.Id == req.MediaId);
            if (media == null)
                return NotFound("محتوایی جهت ویرایش یافت نشد");

            string attrs = string.Empty;

            foreach (var item in req.Attributes.OrderBy(p => p.Key).ThenBy(p => p.Value))
            {
                attrs += (item.Key + ":" + item.Value + "-");
            }
            attrs = attrs.Substring(0, attrs.Length - 1);

            media.Attributes = attrs;
            media.CreatedTime = DateTime.Now;
            media.Description = req.Description;
            media.Poster = req.Poster;
            media.Src = req.Src;
            media.Title = req.Title;
            media.PresenterId = req.PresenterId;
            media.Type = req.Type;
            media.IsFree = req.IsFree;

            _context.Set<Core.Entities.Media.Media>().Update(media);

            await _context.SaveChangesAsync();

            var mediaInRam = _ram.MediaInRams.SingleOrDefault(p => p.MediaId == req.MediaId);

            if(mediaInRam != null)
                mediaInRam.Attributes = media.Attributes;

            return Ok();
        }

        [HttpGet("toggle-activity/{mediaId}")]
        public async Task<IActionResult> ToggleMediaActivity(int mediaId)
        {
            var media = _context.Set<Core.Entities.Media.Media>().SingleOrDefault(p => p.Id == mediaId);
            if (media == null)
                return NotFound("محتوای مورد نظر یافت نشد");

            media.IsActive = !media.IsActive;

            _context.Update(media);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpGet("get/{mediaId}")]
        public IActionResult GetMediaById([FromHeader]int userId,int mediaId)
        {
            var media = _context.Set<Core.Entities.Media.Media>().SingleOrDefault(p => p.Id == mediaId);
            if (media == null)
                return NotFound("فایلی جهت نمایش یافت نشد!");

            if (!_context.Set<Core.Entities.Access.MediaAccessInfo>().Any(p => p.MediaId == media.Id && p.UserId == userId) && !media.IsFree)
                return Unauthorized();

            return Ok(media);
        }

        [HttpGet("get-by-filter")]
        public async Task<IActionResult> GetMediaByFilter(List<GetByFilterRequest> filters)
        {
            foreach (var item in filters)
            {
                foreach (var tag in item.TagIds)
                {

                }
            }

            return Ok();
        }

        
    }
}
