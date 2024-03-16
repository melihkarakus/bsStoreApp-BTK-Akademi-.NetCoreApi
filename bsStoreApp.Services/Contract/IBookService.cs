using bsStoreApp.Entity.DataTransferObjects;
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
        IEnumerable<BookDto> GetAllBooks(bool trackChanges);
        BookDto GetOneBook(int id, bool trackChanges);
        BookDto CreateOneBook(BookDtoForInsertion bookDtoForInsertion);
        void UpdateOneBook(int id, BookDtoUpdate bookDtoUpdate, bool trackChanges);
        void DeleteOneBook(int id, bool trackChanges);
    }
}
