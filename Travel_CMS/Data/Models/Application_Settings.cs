using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpower.Data.Models
{
    public class Application_Settings
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 id { get; set; }

        [StringLength(25)]
        [RequiredAttribute]
        public string key { get; set; }

        [StringLength(150)]
        [RequiredAttribute]
        public string value { get; set; }
        
        [RequiredAttribute]
        public System.Int64 applicationID { get; set; }

    }
}