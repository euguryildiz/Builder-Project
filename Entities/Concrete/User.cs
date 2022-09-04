using System;
using System.Collections.Generic;
using System.Security.Claims;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Entities.Concrete
{
    public class User : BaseEntity, IEntity
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SpecialCode1 { get; set; }
        public string SpecialCode2 { get; set; }
        public string SpecialCode3 { get; set; }
        public DateTime Birthday { get; set; }
        public int EducationId { get; set; }
        public int TitleId { get; set; }
        public int DepartmentId { get; set; }
        public string MobilePhone { get; set; }
        public string Phone { get; set; }
        public string PhoneInternal { get; set; }
        public string Company { get; set; }
        public string CompanyCode { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }

        public List<UserRole> UserRoles { get; set; } 
        public List<UserLog> UserLogs { get; set; }

       
    }
}

