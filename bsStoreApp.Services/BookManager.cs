using AutoMapper;
using bsStoreApp.Entity.DataTransferObjects;
using bsStoreApp.Entity.Exceptions;
using bsStoreApp.Entity.Models;
using bsStoreApp.Repositories.Contracts;
using bsStoreApp.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;
        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDtoForInsertion)
        {
            var values = _mapper.Map<Book>(bookDtoForInsertion);
            _repositoryManager.Book.CreateOneBook(values);
            await _repositoryManager.SaveAsync();
            return _mapper.Map<BookDto>(values);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            var entity = await _repositoryManager.Book.GetOneBookByIdAsync(id, trackChanges);
            if (entity is null)
            {
                throw new BookNotFound(id);
            }
            else
            {
                _repositoryManager.Book.DeleteOneBook(entity);
                await _repositoryManager.SaveAsync();
            }
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges)
        {
            var books = await _repositoryManager.Book.GetAllBookAsync(trackChanges);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetOneBookAsync(int id, bool trackChanges)
        {
            var values = await _repositoryManager.Book.GetOneBookByIdAsync(id, trackChanges);
            return _mapper.Map<BookDto>(values);
        }

        public async Task UpdateOneBookAsync(int id, BookDtoUpdate bookDtoUpdate, bool trackChanges)
        {
            var entity = await _repositoryManager.Book.GetOneBookByIdAsync(id, trackChanges);
            if (entity is null)
            {
                throw new BookNotFound(id);
            }
            else
            {
                entity = _mapper.Map<Book>(bookDtoUpdate);
                _repositoryManager.Book.Update(entity);
                await _repositoryManager.SaveAsync();
            }
        }
    }
}
