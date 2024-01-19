using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid Id { get; set; }
        public string? UserName {  get; set; }
        public string? Address { get; set; }
        public string? Phonenumber { get; set; }
        public string? Email {  get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set;}
        public bool IsPayment {  get; set; }
        public string? PaymentMethod {  get; set; }
        public bool OrderState {  get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }


    }
}
