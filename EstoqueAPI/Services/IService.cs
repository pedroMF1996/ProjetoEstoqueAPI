using EstoqueAPI.Data;
using EstoqueAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueAPI.Services
{
    public interface IService
    {
        public void Add<T>(T entity) where T : class;

        public void Delete<T>(T entity) where T : class;

        public void Update<T>(T entity) where T : class;

        public Task<T> FindByIdAsync<T>(T entity, int id) where T : class;

        public Task<List<T>> FindAllAsync<T>(T entity) where T : class;

        public Task<List<Product>> FindProductsByCategoryId(int id);

        public Task<bool> AnyProductById(int id); 
    }
}
