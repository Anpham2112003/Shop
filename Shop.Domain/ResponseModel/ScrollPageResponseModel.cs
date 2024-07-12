using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ResponseModel
{
    public class ScrollPageResponseModel<T> 
    {
        public int skip {  get; set; }
        public int take {  get; set; }
        public IEnumerable<T>? Data { get; set; }
    }
}
