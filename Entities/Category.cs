using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcECommerce.Entities
{
    public class Category
    {
        public int Id   { get; set; }
        [StringLength(maximumLength:20,ErrorMessage ="you can enter up to 20 characters")]
        public string Name { get; set; }

        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}