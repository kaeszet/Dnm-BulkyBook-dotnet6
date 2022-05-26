using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var obj = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (obj != null)
            {
                obj.Id = product.Id;
                obj.Title = product.Title;
                obj.Author = product.Author;
                obj.ISBN = product.ISBN;
                obj.Description = product.Description;
                obj.ListPrice = product.ListPrice;
                obj.Price = product.Price;
                obj.Price50 = product.Price50;
                obj.Price100 = product.Price100;
                obj.CategoryID = product.CategoryID;
                obj.CoverTypeID = product.CoverTypeID;

                if (product.ImageUrl != null) obj.ImageUrl = product.ImageUrl;
            }
        }
    }
}
