﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.MultyTenants
{
    public interface IHaveTenant
    {
        public int? TenantId { get; set; }
    }
}
