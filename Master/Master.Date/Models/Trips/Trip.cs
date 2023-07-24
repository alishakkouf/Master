using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Domain.MultyTenants;

namespace Master.Data.Models.Trips
{
    internal class Trip : IAuditedEntity
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string From { get; set; }

        [StringLength(100)]
        public string To { get; set; }

        public DateTime Date { get; set; }

        public int NumOfSeats { get; set; }

        public int AvailableNumOfSeats { get; set; }

        public long? CreatorUserId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public long? ModifierUserId { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool? IsDeleted { get; set; } = false;
    }
}
