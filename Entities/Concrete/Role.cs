using System;
using System.Collections.Generic;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Entities.Concrete
{
    public class Role : BaseEntity, IEntity
    {
        public string Subject { get; set; }
        public bool IsAdmin { get; set; }

        public List<UserRole> UserRoles { get; set; } 


    }
}

