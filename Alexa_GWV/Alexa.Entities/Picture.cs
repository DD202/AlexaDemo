using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Alexa.Entities
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(500)]
        public string SmallImageUrl { get; set; }

        [MaxLength(500)]
        public string LargeImageUrl { get; set; }

    }
}
