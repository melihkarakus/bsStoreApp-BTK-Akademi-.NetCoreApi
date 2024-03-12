using bsStoreApp.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Services.Contract
{
    public interface IBookService 
    {
        IEnumerable<Book> GetAllBooks(bool trackChanges);
        Book GetOneBook(int id, bool trackChanges);
        Book CreateOneBook(Book book);
        void UpdateOneBook(int id, Book book, bool trackChanges);
        void DeleteOneBook(int id, bool trackChanges);
    }
}
