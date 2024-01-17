using Shop.Aplication.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Command
{
    public  class CreateUserCommand : ICommand
    {
        
        public Guid Id => Guid.NewGuid();
        public string? FistName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Refreshtoken => null;
        public DateTime CreatedAt => DateTime.UtcNow;
        public DateTime? UpdatedAt => null;
        public bool IsDeleted => false;
        public DateTime? DeletedAt => null;


    }
}
