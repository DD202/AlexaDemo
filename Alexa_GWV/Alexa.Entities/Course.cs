using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexa.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Author { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public int Votes { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
