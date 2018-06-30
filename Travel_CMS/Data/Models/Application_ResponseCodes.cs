using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpower.Data.Models
{
    public class Application_ResponseCodes
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 id { get; set; }
        
        [RequiredAttribute]
        public System.Int64 applicationID { get; set; }

        [StringLength(10)]
        [RequiredAttribute]
        public string responseCode { get; set; }

        [StringLength(450)]
        public string responseMessage { get; set; }

    }
}