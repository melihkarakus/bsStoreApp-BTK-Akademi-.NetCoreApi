﻿using bsStoreApp.Entity.Exceptions;
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
        public BookManager(IRepositoryManager repositoryManager, ILoggerService loggerService)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;
        }

        public Book CreateOneBook(Book book)
        {
            _repositoryManager.Book.CreateOneBook(book);
            _repositoryManager.Save();
            return book;
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

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _repositoryManager.Book.GetAllBook(trackChanges);
        }

        public Book GetOneBook(int id, bool trackChanges)
        {
            return _repositoryManager.Book.GetOneBookById(id, trackChanges);
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                throw new BookNotFound(id);
            }
            else
            {
                _repositoryManager.Book.UpdateOneBook(entity);
                _repositoryManager.Save();
            }
        }
    }
}
