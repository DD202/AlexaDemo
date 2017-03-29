using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexa.Entities
{
    public class InformationalFact
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Fact { get; set; }

        public int Votes { get; set; }
    }
}
