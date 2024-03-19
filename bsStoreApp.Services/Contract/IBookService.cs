using bsStoreApp.Entity.DataTransferObjects;
using bsStoreApp.Entity.Models;
using bsStoreApp.Entity.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Services.Contract
{
    public interface IBookService 
    {
        Task<(IEnumerable<ExpandoObject> booksDto, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges);
        Task<BookDto> GetOneBookAsync(int id, bool trackChanges);
        Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDtoForInsertion);
        Task UpdateOneBookAsync(int id, BookDtoUpdate bookDtoUpdate, bool trackChanges);
        Task DeleteOneBookAsync(int id, bool trackChanges);
    }
}
