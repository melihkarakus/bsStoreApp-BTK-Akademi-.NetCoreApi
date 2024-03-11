using bsStoreApp.Entity.Models;
using bsStoreApp.Repositories;
using bsStoreApp.Repositories.EFCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bsStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public BooksController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var Getbooks = _repositoryContext.Books.ToList();
            return Ok(Getbooks);
        }
        [HttpGet("GetOneBook")]
        public IActionResult GetOneBook(int id)
        {
            var Getonebooks = _repositoryContext.Books.FirstOrDefault(x => x.ID == id);
            if (Getonebooks == null)
            {
                return NotFound("Girilen ID Geçersiz!!");
            }
            else
            {
                return Ok(Getonebooks);
            }
        }
        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            _repositoryContext.Books.Add(book);
            _repositoryContext.SaveChanges();
            return Ok("Kitap Eklendi.");
        }
        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            var Deletebooks = _repositoryContext.Books.FirstOrDefault(x => x.ID == id);
            _repositoryContext.Books.Remove(Deletebooks);
            _repositoryContext.SaveChanges();
            return Ok("Kitap Silindi.");
        }
        [HttpPut]
        public IActionResult UpdateBook(int id, Book book)
        {
            var IDbooks = _repositoryContext.Books.FirstOrDefault(x => x.ID == id);
            if(IDbooks == null)
            {
                return NotFound();
            }
            else
            {
                _repositoryContext.Books.Remove(IDbooks);
                book.ID = IDbooks.ID;
                _repositoryContext.Books.Add(book);
                _repositoryContext.SaveChanges();
                return Ok(book);
            }
        }
    }
}
