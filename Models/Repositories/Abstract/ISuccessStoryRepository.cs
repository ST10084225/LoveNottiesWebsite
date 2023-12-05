using System.Collections.Generic;

namespace NottiesRebuiltV3.Models.Repositories.Abstract
{
    public interface ISuccessStoryRepository
    {
        //get all of the success stories
        IEnumerable<SuccessStory> GetAllSuccessStories();
    }
}
