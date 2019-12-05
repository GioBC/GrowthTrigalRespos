using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrowthTrigal.Web.Data.Entities
{
    public class UP
    {
        public int Id { get; set; }

        [MaxLength(25, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Farm Name")]
        public string FarmName { get; set; }

        [MaxLength(4, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Alias Farm")]
        public string AliasFarm { get; set; }

        public string FullFarm => $"{FarmName} - {AliasFarm}";

        public ICollection<Home> Homes { get; set; }


        

    }
}
