using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Data.Models
{
    internal class Setting
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public long? UserId { get; set; }
    }
}
