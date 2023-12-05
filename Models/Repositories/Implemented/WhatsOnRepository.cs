using NottiesRebuiltV3.Data;
using NottiesRebuiltV3.Models.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace NottiesRebuiltV3.Models.Repositories.Implemented
{
    public class WhatsOnRepository : IWhatsOnRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly IBlobService _IblobService;

        public WhatsOnRepository(ApplicationDbContext context, IBlobService iblobService)
        {
            this._context = context;
            _IblobService = iblobService;
        }

        public IEnumerable<WhatsOnModel> GetAllBlogPosts()
        {
            var posts = _context.WhatsOn;
            IEnumerable<WhatsOnModel> list = _context.WhatsOn;
            foreach (var item in posts)
            {

                /*posts. = _IblobService.GetFileBlobAsync(item.BlogImageID, BlobContainer.blogimages).Result;*/
                return list;
            }
            return list;
        }

    }
}
