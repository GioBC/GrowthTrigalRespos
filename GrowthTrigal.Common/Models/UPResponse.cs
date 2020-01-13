using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SQLite;

namespace GrowthTrigal.Common.Models
{
    public class UPResponse
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string AliasFarm { get; set; }

        public string FarmName { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<HomeResponse> Homes { get; set; }


    }
}
