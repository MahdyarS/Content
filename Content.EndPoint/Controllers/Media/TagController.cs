using Content.Core.Entities.Media;
using Content.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Content.EndPoint.Controllers.Media
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly Context _context;
        private readonly Ram.Ram _ram;

        public TagController(Context context, Ram.Ram ram)
        {
            _context = context;
            _ram = ram;
        }

        [HttpGet("create-tag")]
        public async Task<IActionResult> CreateTag(int propertyId, string value)
        {
            var property = _context.Set<Property>().SingleOrDefault(p => p.Id == propertyId);
            if (property == null)
                return NotFound("ویژگی مورد نظر برای ایجاد تگ یافت نشد");

            var tag = new Tag(property, value);

            await _context.Set<Tag>().AddAsync(tag);
            await _context.SaveChangesAsync();

            return Ok(tag.Id);
        }

        [HttpGet("get-all-properties")]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _context.Set<Property>().ToListAsync();
            return Ok(properties);
        }

        [HttpGet("get-property-tags")]
        public async Task<IActionResult> GetPropertyTags()
        {
            var res = await _context.Set<Tag>().GroupBy(p => p.Property).ToListAsync();
            return Ok(res);
        }

        [HttpDelete("delete-tag/{tagId}")]
        public async Task<IActionResult> DeleteTag(int tagId,bool sure)
        {
            var tag = _context.Set<Tag>().SingleOrDefault(p => p.Id == tagId);
            if (tag == null)
                return NotFound("تگ مورد نظر یافت نشد!");

            var mediaTags = _context.Set<MediaTag>().Where(p => p.TagId == tagId).ToList();

            if (mediaTags.Any() && !sure)
                return BadRequest("برخی کالا ها تگ مورد نظر را دارند. آیا از حذف این تگ مطمئنید؟");

            _context.Set<Tag>().Remove(tag);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
