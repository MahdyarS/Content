using Content.Core.Entities.Media;
using Content.EndPoint.Models.Media;
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
            };

            await _context.Set<Core.Entities.Media.Media>().AddAsync(media);

            _ram.MediaAbstracts.Add(new Models.RamModels.MediaAbstract
            {
                MediaId = media.Id,
                Attributes = media.Attributes,
                Title = media.Title,
                Poster = media.Poster,
                IsActive = media.IsActive,
                Presenter = media.Presenter
            });

            await _context.SaveChangesAsync();

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

            _context.Set<Core.Entities.Media.Media>().Update(media);

            var mediaInRam = _ram.MediaAbstracts.SingleOrDefault(p => p.MediaId == media.Id);
            if (mediaInRam == null)
                _ram.MediaAbstracts.Add(new Models.RamModels.MediaAbstract
                {
                    MediaId = media.Id,
                    Attributes = media.Attributes,
                    Presenter = media.Presenter,
                    IsActive = media.IsActive,
                    Poster = media.Poster,
                    Title = media.Title
                });
            else
            {
                mediaInRam.Attributes = media.Attributes;
                mediaInRam.Presenter = media.Presenter;
                mediaInRam.IsActive = media.IsActive;
                mediaInRam.Poster = media.Poster;
                mediaInRam.Title = media.Title;
            }
                

            await _context.SaveChangesAsync();

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

            var mediaInRam = _ram.MediaAbstracts.SingleOrDefault(p => p.MediaId == media.Id);
            if (mediaInRam == null)
                _ram.MediaAbstracts.Add(new Models.RamModels.MediaAbstract
                {
                    MediaId = media.Id,
                    Attributes = media.Attributes,
                    Presenter = media.Presenter,
                    IsActive = media.IsActive,
                    Poster = media.Poster,
                    Title = media.Title
                });
            else
                mediaInRam.IsActive = media.IsActive;

            return Ok();
        }


        [HttpGet("get/{mediaId}")]
        public IActionResult GetMediaById([FromHeader]int userId,int mediaId)
        {
            var media = _context.Set<Core.Entities.Media.Media>().SingleOrDefault(p => p.Id == mediaId);
            if (media == null)
                return NotFound("فایلی جهت نمایش یافت نشد!");

            if (!_context.Set<Core.Entities.Access.MediaAccessInfo>().Any(p => p.MediaId == media.Id && p.UserId == userId))
                return Unauthorized();

            return Ok(media);
        }

        

        
    }
}
