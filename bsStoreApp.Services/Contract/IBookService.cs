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
        Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges);
        Task<BookDto> GetOneBookAsync(int id, bool trackChanges);
        Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDtoForInsertion);
        Task UpdateOneBookAsync(int id, BookDtoUpdate bookDtoUpdate, bool trackChanges);
        Task DeleteOneBookAsync(int id, bool trackChanges);
    }
}
