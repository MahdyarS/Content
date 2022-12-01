using Content.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Content.EndPoint.Controllers.Access
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly Context _context;

        public AccessController(Context context)
        {
            _context = context;
        }

        public IActionResult AddAccess([FromHeader]int userId,int mediaId,string phoneNumber,string name)
        {
            var media = _context.Set<Core.Entities.Media.Media>().SingleOrDefault(p => p.Id == mediaId);
            if (media == null)
                return NotFound("فایلی جهت دسترسی دادن یافت نشد!");

            _context.Set<Core.Entities.Access.MediaAccessInfo>().Add(new Core.Entities.Access.MediaAccessInfo
            {
                UserId = userId,
                MediaId = mediaId,
                PhoneNumber = phoneNumber,
                AddedDateTime = DateTime.Now,
                FullName = name,
            });

            _context.SaveChanges();

            return Ok();
        }
    }
}
