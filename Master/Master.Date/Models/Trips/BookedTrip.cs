using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Data.Models.Account;
using Master.Domain.MultyTenants;

namespace Master.Data.Models.Trips
{
    internal class BookedTrip : IAuditedEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TripId { get; set; }

        [ForeignKey(nameof(TripId))]
        public virtual Trip Trip { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserAccount User { get; set; }

        public long? CreatorUserId { get; set; } 

        public DateTime? CreatedAt { get; set; }

        public long? ModifierUserId { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
