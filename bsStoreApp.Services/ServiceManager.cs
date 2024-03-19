using AutoMapper;
using bsStoreApp.Entity.DataTransferObjects;
using bsStoreApp.Repositories.Contracts;
using bsStoreApp.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper, IDataShaper<BookDto> shaper)
        {
            _bookService = new Lazy<IBookService>(() => new BookManager(repositoryManager, loggerService, mapper, shaper));
        }
        public IBookService BookService => _bookService.Value;
    }
}
