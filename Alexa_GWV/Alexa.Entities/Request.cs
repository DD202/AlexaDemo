using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexa.Entities
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        [MaxLength(500)]
        public string SessionId { get; set; }

        [MaxLength(500)]
        public string AppId { get; set; }

        [MaxLength(500)]
        public string RequestId { get; set; }

        [MaxLength(500)]
        public string UserId { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(500)]
        public string Intent { get; set; }

        [MaxLength(50)]
        public string Slots { get; set; }

        public bool IsNew { get; set; }

        [MaxLength(5)]
        public string Version { get; set; }

        [MaxLength(50)]
        public string Type { get; set; }

        [MaxLength(50)]
        public string Reason { get; set; }

        public DateTime DateCreated { get; set; }

        private List<KeyValuePair<string, string>> slotsList = new List<KeyValuePair<string, string>>();

        [NotMapped]
        public string AccessToken { get; set; }

        [NotMapped]
        public List<KeyValuePair<string, string>> SlotsList
        {
            get
            {
                return slotsList;
            }
            set
            {
                slotsList = value;
                var slots = new StringBuilder();
                SlotsList.ForEach(s => slots.AppendFormat("{0}|{1},", s.Key, s.Value));
                Slots = slots.ToString().TrimEnd(',');
            }
        }
    }
}
