using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ResponseModel
{
    public class CommentResponseModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? Content {  get; set; }
        public int Rate {  get; set; }
        public string? UserName {  get; set; }
        public string? Avatar { get; set; }
    }
}
