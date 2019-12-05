using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrowthTrigal.Common.Models
{
   public  class HomeRequest
    {

        public int Id { get; set; }

        [Required]
        public string BlockNumber { get; set; }

      

    }
}

