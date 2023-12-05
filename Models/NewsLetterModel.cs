using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NottiesRebuiltV3.Models
{
    public class NewsLetterModel
    {
        [Key]
        [DataType(DataType.Text)]
        public string EmailID { get; set; }

        [DataType(DataType.Text)]
        public string Email { get; set; }
    }
}
