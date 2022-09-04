using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class UserLog: BaseEntity,  IEntity
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }


        virtual public User User { get; set; }
    }
}

