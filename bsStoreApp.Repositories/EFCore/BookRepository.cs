using bsStoreApp.Entity.Models;
using bsStoreApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public IQueryable<Book> GetAllBook(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(b => b.ID);

        public Book GetOneBookById(int id, bool trackChanges) =>
            FindByCondition(b => b.ID.Equals(id), trackChanges)
            .FirstOrDefault();

        public void UpdateOneBook(Book book) => Update(book);
    }
}
