using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ResponseModel
{
    public class LoginResponseModel
    {
        public string? accesstoken {  get; set; }
        public string? refeshtoken { get; set; }

        public LoginResponseModel(string? accesstoken, string? refeshtoken)
        {
            this.accesstoken = accesstoken;
            this.refeshtoken = refeshtoken;
        }
    }
}
