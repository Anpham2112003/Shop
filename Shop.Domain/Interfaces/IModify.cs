using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IModify
    {
        public DateTime UpdatedAt { get; set; }
    }
}
