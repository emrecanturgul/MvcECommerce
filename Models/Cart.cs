using MvcECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcECommerce.Models
{
    public class Cart
    {
        private List<CartLine> _cartLines = new List<CartLine>();
        public List<CartLine> CartLines { 
            get { return _cartLines; }
        }
        public void AddProduct(Product product , int quantity){
            var line = _cartLines.FirstOrDefault(i=>i.Product.Id == product.Id);
            if(line == null)
            {
                _cartLines.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public int TotalQuantity()
        {
            return _cartLines.Sum(l => l.Quantity);
        }
        public void DecreaseProductQuantity(int productId)
        {
            var line = _cartLines.FirstOrDefault(l => l.Product.Id == productId);
            if (line != null)
            {
                if (line.Quantity > 1)
                {
                    line.Quantity--;
                }
                else
                {
                
                    _cartLines.Remove(line);
                }
            }
        }
        public void RemoveProduct(Product product)
        {
            var remove = _cartLines.FirstOrDefault(p=>p.Product.Id == product.Id);
            _cartLines.Remove(remove);

        }
        public double Total()
        {
            return _cartLines.Sum(i => i.Product.Price * i.Quantity);
        }
        public void Clear()
        {
            _cartLines.Clear();
        }
    }
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}