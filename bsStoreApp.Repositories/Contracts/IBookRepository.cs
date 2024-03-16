using bsStoreApp.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Repositories.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<IEnumerable<Book>> GetAllBookAsync(bool trackChanges);
        Task<Book> GetOneBookByIdAsync(int id, bool trackChanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);

    }
}
