using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Domain.MultyTenants;

namespace Master.Data.Models
{
    internal class Tenant : IAuditedEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string DomainName { get; set; }

        [StringLength(500)]
        public string AdminEmail { get; set; }

        public bool IsActive { get; set; } = true;

        public long? CreatorUserId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public long? ModifierUserId { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool? IsDeleted { get; set; } = false;
    }
}

