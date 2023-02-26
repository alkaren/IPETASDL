using System;
using System.Collections.Generic;

namespace LALAPATAPA.services.Models
{
    public partial class Product
    {
        public Product()
        {
            DetailTransaction = new HashSet<DetailTransaction>();
        }

        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string IsAvailable { get; set; }
        public int? Version { get; set; }
        public string ImageUrl { get; set; }
        public string FileUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string MapType { get; set; }
        public string Scale { get; set; }
        public string Commodity { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string SubDistrict { get; set; }
        public string AvailableDescription { get; set; }

        public virtual ICollection<DetailTransaction> DetailTransaction { get; set; }
    }
}
