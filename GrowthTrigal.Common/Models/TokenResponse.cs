using System;
using System.Collections.Generic;
using System.Text;

namespace GrowthTrigal.Common.Models
{
    public class TokenResponse
    {
        //[JsonProperty("token")]
        public string Token { get; set;}

        public DateTime Expiration { get; set; }

        public DateTime ExpirationLocal => ExpirationLocal.ToLocalTime();
    }
}
