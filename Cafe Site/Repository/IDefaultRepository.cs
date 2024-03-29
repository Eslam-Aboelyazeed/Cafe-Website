namespace Cafe_Site.Repository
{
	public interface IDefaultRepository<T>
	{
		public List<T> GetAll(string? include);

		public T GetElement(Func<T, bool> func, string? include);
		public List<T> GetElementsByFilter(Func<T, bool> func, string? include);


        public void Insert(T entity);

		public void Update(T entity);

		public void Delete(T entity);

		public void SaveChanges();
	}
}