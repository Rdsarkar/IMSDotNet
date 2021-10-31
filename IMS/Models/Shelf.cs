using System;
using System.Collections.Generic;

#nullable disable

namespace IMS.Models
{
    public partial class Shelf
    {
        public decimal? Sid { get; set; }
        public decimal? Floor { get; set; }
        public string Buildingname { get; set; }
        public string Zone { get; set; }
        public decimal? Area { get; set; }
    }
}
