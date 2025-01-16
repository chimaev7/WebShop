﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using WebShop2024.Core.Contracts;
using WebShop2024.Infrastructure.Data;
using WebShop2024.Infrastructure.Data.Entities;

namespace WebShop2024.Core.Services
{
    public class OrderService:IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context; 
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.OrderByDescending(x=>x.OrderDate).ToList();
        }

        public List<Order> GetOrdersByUser(string userId)
        {
            return _context.Orders.Where(x=>x.UserId==userId)
                .OrderByDescending(x=>x.OrderDate)
                .ToList();
        }

        public bool Create(int productId, string userId, int quantity)
        {
           
            var product = this._context.Products.SingleOrDefault(x => x.Id == productId);

           
            if (product == null)
                return false;

            
            Order item = new Order
            {
                OrderDate = DateTime.Now,
                ProductId = productId,
                UserId = userId,
                Quantity = quantity,
                Price = product.Price,
                Discount = product.Discount
            };

           
            product.Quantity -= quantity;

           
            this._context.Products.Update(product);
            this._context.Orders.Add(item);

            return this._context.SaveChanges() != 0;
        }
    }
}
