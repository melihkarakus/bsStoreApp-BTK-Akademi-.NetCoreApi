﻿using bsStoreApp.Entity.Models;
using bsStoreApp.Entity.RequestFeatures;
using bsStoreApp.Repositories.Contracts;
using bsStoreApp.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Repositories.EFCore
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public async Task<PagedList<Book>> GetAllBookAsync(BookParameters bookParameters, bool trackChanges)
        {
            var books = await FindAll(trackChanges)
                .FilterBooks(bookParameters.MinPrice, bookParameters.MaxPrice)//FilterBooks Extensions yazıldığı için bu şekilde tanımlandı.
                .Search(bookParameters.SearchTerm)//Search Extensions yazıldığı için bu şekilde tanımlandı.
            .Sort(bookParameters.OrderBy)// Orderby Extensions yazıldığı için bu şekilde tanımlandı. Repository => extensions
            .ToListAsync();

            return PagedList<Book>
                .ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.ID.Equals(id), trackChanges)
            .FirstOrDefaultAsync();

        public void UpdateOneBook(Book book) => Update(book);
    }
}
