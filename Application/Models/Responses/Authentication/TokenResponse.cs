using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses.Authentication
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }

        public TokenResponse(string token, DateTime? expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
}
