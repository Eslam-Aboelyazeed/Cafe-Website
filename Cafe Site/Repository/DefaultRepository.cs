using Cafe_Site.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace Cafe_Site.Repository
{
	public class DefaultRepository<T> : IDefaultRepository<T> where T : class
	{
		protected readonly CafeSiteContext db;

		public DefaultRepository(CafeSiteContext cafeSiteContext)
        {
			this.db = cafeSiteContext;
		}

        public List<T> GetAll(string? include)
		{
			try
			{
                return (include != null) ? db.Set<T>().Include(include).ToList() : db.Set<T>().ToList();
            }
            catch (Exception)
			{
				return db.Set<T>().ToList();
			}
		}

		public T GetElement(Func<T,bool> func, string? include)
		{
			try
			{
                return (include != null) ? db.Set<T>().Include(include).FirstOrDefault(func) : db.Set<T>().FirstOrDefault(func);
            }
            catch (Exception)
			{
				return db.Set<T>().FirstOrDefault(func);
            }
		}

        public List<T> GetElementsByFilter(Func<T, bool> func, string? include)
        {
            try
            {
                return (include != null) ? db.Set<T>().Include(include).Where(func).ToList() : db.Set<T>().Where(func).ToList();
            }
            catch (Exception)
            {
                return db.Set<T>().Where(func).ToList();
            }
        }

        public void Insert(T entity)
		{
			db.Add(entity);
		}

		public void Update(T entity)
		{
			db.Update(entity);
		}

		public void Delete(T entity)
		{
			db.Remove(entity);
		}

		public void SaveChanges()
		{
			db.SaveChanges();
		}
	}
}
