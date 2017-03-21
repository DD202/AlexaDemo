using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexa.Entities
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string AlexaUserId { get; set; }

        public int RequestCount { get; set; }

        public DateTime LastRequestDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
