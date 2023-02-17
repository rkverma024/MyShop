using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyShop.Core.Models
{
    public class Product
    {
        public string Id { get; set; }

        [DisplayName("Product Name")]
        [StringLength(20)]
        [Required]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required]
        public string Description { get; set; }
        [DisplayName("Price")]
        [Required]
        [Range(0, 1500)]
        public string Price { get; set; }
        [DisplayName("Category")]
        [Required]
        [StringLength(25)]
        public string Category { get; set; }
        [DisplayName("Image")]
        public string Image { get; set; }

        public Product()
        {
            this.Id = Guid.NewGuid().ToString();

        }
    }
}
