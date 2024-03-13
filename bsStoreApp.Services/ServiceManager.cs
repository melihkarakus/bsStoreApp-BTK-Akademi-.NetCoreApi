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
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService loggerService)
        {
            _bookService = new Lazy<IBookService>(() => new BookManager(repositoryManager, loggerService));
        }
        public IBookService BookService => _bookService.Value;
    }
}
