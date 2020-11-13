using System;
using System.Collections.Generic;
using Velvetech.Domain.Common.Validation;
using Velvetech.Domain.Common.Validation.Interfaces;

namespace Velvetech.Domain.Entities
{
    public class User// : ValidatableEntity<Guid>
    {
        public User()
        {
            UserRole = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
