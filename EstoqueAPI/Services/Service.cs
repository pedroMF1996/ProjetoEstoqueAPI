using EstoqueAPI.Data;
using EstoqueAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueAPI.Services
{
    public class Service: IService
    {
        public DataContext context { get; }
        public Service(DataContext context)
        {
            this.context = context;
        }

        #region Generics

        public void Add<T>(T entity) where T : class
        {
            try
            {
                context.Add(entity);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Delete<T>(T entity) where T : class
        {
            try
            {
                context.Remove(entity);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }

        public async Task<T> FindByIdAsync<T>(T entity, int id) where T : class
        {
            if (!(entity is Category || entity is Product))
            {
                throw new Exception("Entidade não listada");
            }

            if (entity is Product)
            {
                var product = await context
                    .Products
                    .Include(x => x.Category)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                return product as T;
            }
            else
            {
                var category = await context
                    .Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                return category as T;
            }
        }


        public async Task<List<T>> FindAllAsync<T>(T entity) where T : class
        {

            if (!(entity is Category || entity is Product))
            {
                throw new Exception("Entidade não listada");
            }

            if (entity is Product)
            {
                var product = await context.Products
                    .Include(x => x.Category)
                    .ToListAsync();

                return product as List<T>;
            }
            else
            {
                var category = await context.Categories
                    .ToListAsync(); 

                return category as List<T>;
            }

        }

        #endregion

        #region Products
        
        public async Task<bool> AnyProductById(int id) =>
            await context.Products.AnyAsync(c => c.Id == id);
        public async Task<List<Product>> FindProductsByCategoryId(int id) =>
            await context
                .Products
                .Include(x => x.Category)
                .Where(x => x.CategoryId == id)
                .ToListAsync();

        #endregion
        
        public async Task<bool> SaveChangesAsync() => 
            (await context.SaveChangesAsync() > 0);
        
    }
}
