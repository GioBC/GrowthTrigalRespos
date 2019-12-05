using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrowthTrigal.Web.Data.Entities
{
    public class Home
    {

        public int Id { get; set; }

        [MaxLength(2, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Block Number")]
        public string BlockNumber { get; set; }


        public ICollection<Flower> Flowers { get; set; }


        public UP UP { get; set; }
    }


}
