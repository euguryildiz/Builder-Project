using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace Entities.Concrete
{
    public class Group : BaseEntity, IEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [ForeignKey("ParentId")]
        public int? ParentId { get; set; }

        public int? DisplayOrder { get; set; }

        public int? V_ID { get; set; }

        public Group Parent { get; set; }
    }
}

