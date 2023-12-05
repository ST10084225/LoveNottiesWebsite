using NottiesRebuiltV3.Data;
using NottiesRebuiltV3.Models.Repositories.Abstract;
using System.Collections.Generic;

namespace NottiesRebuiltV3.Models.Repositories.Implemented
{
    public class OurPeopleRepository : IOurPeopleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlobService _IblobService;

        public OurPeopleRepository(ApplicationDbContext context, IBlobService iblobService)
        {
            this._context = context;
            _IblobService = iblobService;
        }

        public IEnumerable<OurPeople> GetAllOurPeople()
        {
            var posts = _context.OurPeoples;
            IEnumerable<OurPeople> list = _context.OurPeoples;
            foreach (var item in posts)
            {
                return list;
            }
            return list;
        }
    }
}
