using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexa.Entities
{
    public class UserPreference
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(128)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Preferred Name")]
        [MaxLength(200)]
        public string PreferredName { get; set; }

        [Display(Name ="Home City")]
        [MaxLength(50)]
        public string City { get; set; }

    }
}
