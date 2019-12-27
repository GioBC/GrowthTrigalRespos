using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowthTrigal.Common.Models
{
    public class TokenResponse
    {
        [PrimaryKey, AutoIncrement]
        public int TokenId { get; set; }
        public string Token { get; set;}

        public DateTime Expiration { get; set; }

        public DateTime ExpirationLocal => ExpirationLocal.ToLocalTime();

        public bool IsRemembered { get; set; }

        public override int GetHashCode()
        {
            return TokenId;
        }
    }
}
