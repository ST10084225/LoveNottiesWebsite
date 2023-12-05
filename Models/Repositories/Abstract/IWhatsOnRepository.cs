using System.Collections.Generic;

namespace NottiesRebuiltV3.Models.Repositories.Abstract
{
    public interface IWhatsOnRepository
    {
        //get all products currently on site
        IEnumerable<WhatsOnModel> GetAllBlogPosts();
    }
}
