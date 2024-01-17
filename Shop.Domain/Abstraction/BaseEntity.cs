﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Abstraction
{
    public interface BaseEntity
    {
        public  Guid Id { get; set; }
        public  DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public    bool IsDeleted { get; set; }
        public  DateTime? DeletedAt { get; set; }

    }
}

