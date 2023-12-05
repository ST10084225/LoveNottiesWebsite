using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace NottiesRebuiltV3.Models
{
    public class AdminModel
    {
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile BlogImageFile { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile OPImageFile { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile SSImageFile { get; set; }
    }
}
