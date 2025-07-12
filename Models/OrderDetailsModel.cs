using MvcECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcECommerce.Models
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }
        public virtual List<OrderLineModel> OrderLines { get; set; }
        public string FullName { get; set; }
        public string AddressName { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }


        public string ZipCode { get; set; }
    }
    public class OrderLineModel
    {
        public string Image { get; set; }   
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        
    }
}