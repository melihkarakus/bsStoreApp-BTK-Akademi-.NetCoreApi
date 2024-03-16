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

        public BookDto CreateOneBook(BookDtoForInsertion bookDtoForInsertion)
        {
            var values = _mapper.Map<Book>(bookDtoForInsertion);
            _repositoryManager.Book.CreateOneBook(values);
            _repositoryManager.Save();
            return _mapper.Map<BookDto>(values);
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                throw new BookNotFound(id);
            }
            else
            {
                _repositoryManager.Book.DeleteOneBook(entity);
                _repositoryManager.Save();
            }
        }

        public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
        {
            var books = _repositoryManager.Book.GetAllBook(trackChanges);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public BookDto GetOneBook(int id, bool trackChanges)
        {
            var values = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            return _mapper.Map<BookDto>(values);
        }

        public void UpdateOneBook(int id, BookDtoUpdate bookDtoUpdate, bool trackChanges)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                throw new BookNotFound(id);
            }
            else
            {
                entity = _mapper.Map<Book>(bookDtoUpdate);
                _repositoryManager.Book.UpdateOneBook(entity);
                _repositoryManager.Save();
            }
        }
    }
}
