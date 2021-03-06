﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeerflyPatches.Models;

namespace DeerflyPatches.ViewModels
{
    public class ShoppingCart
    {
        public DeerflyPatches.Models.Order _order;
        public List<OrderDetail> _shoppingCart { get; set; }
        public string payeeEmail { get; set; }
        public decimal TotalExtendedPrice
        {
            get
            {
                return _shoppingCart.Sum(p => p.ExtendedPrice);
            }
        }

        public decimal TotalShipping
        {
            get
            {
                return _shoppingCart.Sum(p => p.Shipping);
            }
        }

        public decimal GrandTotal
        {
            get
            {
                return TotalExtendedPrice + TotalShipping;
            }
        }

        public ShoppingCart()
        {
            _shoppingCart = new List<OrderDetail>();
            _order = new DeerflyPatches.Models.Order();
        }

        public List<OrderDetail> GetItems()
        {
            // TODO: return clone to prevent writing to the data outside the class?
            return _shoppingCart;
        }

        public Order GetOrder()
        {
            // TODO: return clone to prevent writing to the data outside the class?
            return _order;
        }

        public void AddOrderDetail(OrderDetail newItem)
        {
            _shoppingCart.Add(newItem);
        }

        public void AddProduct(Product product)
        {
            OrderDetail orderDetail = _shoppingCart.Find(p => p.Item.ID == product.ID);
            if (orderDetail == null)
            {
                orderDetail = new OrderDetail()
                {
                    Item = product,
                    PlacedInCart = DateTime.Now,
                    Quantity = 1,
                    UnitPrice = product.Price,
                    Shipping = product.Shipping
                };
                _shoppingCart.Add(orderDetail);
            }
            else
            {
                orderDetail.Quantity++;
            }
        }

        public void DecrementProduct(Product product)
        {
            OrderDetail orderDetail = _shoppingCart.Find(p => p.Item.ID == product.ID);
            if (!(orderDetail == null) && orderDetail.Quantity > 0)
            {
                orderDetail.Quantity--;
            }
            if (orderDetail.Quantity == 0)
            {
                RemoveProduct(product);
            }
        }

        public void RemoveProduct(Product product)
        {
            OrderDetail orderDetail = _shoppingCart.Find(p => p.Item.ID == product.ID);
            if (!(orderDetail == null))
            {
                _shoppingCart.Remove(orderDetail);
            }
        }



    }
}
