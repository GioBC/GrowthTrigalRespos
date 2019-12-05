using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrowthTrigal.Common.Models
{
    public  class UpRequest
    {
        public int Id { get; set; }

        [Required]
        public string AliasFarm { get; set; }
    }
}
