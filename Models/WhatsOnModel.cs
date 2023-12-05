using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NottiesRebuiltV3.Models
{
    public class WhatsOnModel
    {
        [Key]
        [DataType(DataType.Text)]
        public string PostID { get; set; }

        [DataType(DataType.Text)]
        public string PostTitle { get; set; }

        [DataType(DataType.Text)]
        public string PostImageID { get; set; }

        [DataType(DataType.Text)]
        public string PostDescription { get; set; }

        [DataType(DataType.Date)]
        public string PostDate { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile PostImageFile { get; set; }

        [NotMapped]
        public byte[] PostImageBytes { get; set; }
    }
}
