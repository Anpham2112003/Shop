using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ResponseModel
{
    public class UserResponseModel
    {
        public Guid Id { get; set; }
        public string? FistName { get; set; }
        public string? LastName {  get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        

    }
}
