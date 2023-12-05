using System.Collections.Generic;

namespace NottiesRebuiltV3.Models.Repositories.Abstract
{
    public interface IOurPeopleRepository
    {
        //get all of our people
        IEnumerable<OurPeople> GetAllOurPeople();
    }
}
