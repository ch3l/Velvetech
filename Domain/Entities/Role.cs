﻿using System;
using System.Collections.Generic;

namespace Velvetech.Domain.Entities
{
    public class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
