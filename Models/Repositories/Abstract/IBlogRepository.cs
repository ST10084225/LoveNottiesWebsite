using System.Collections.Generic;

namespace NottiesRebuiltV3.Models.Repositories.Abstract
{
    public interface IBlogRepository
    {
        //get all products currently on site
        IEnumerable<BlogItem> GetAllBlogPosts();
    }
}
