﻿using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        
       int IncrementCount(ShoppingCart shoppingCart, int count);
        int DecrementCoutn(ShoppingCart shoppingCart, int count);   
    }
}