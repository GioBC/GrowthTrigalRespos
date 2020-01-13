using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrowthTrigal.Common.Models
{
    public class TokenRequest
    {
        [PrimaryKey]
        public string Username { get; set; }

        public string Password { get; set; }

        public string BlockNumber { get; set; }

        public string AliasFarm { get; set; }

        public DateTime fecha { get; set; }

    }
}
