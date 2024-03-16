    using bsStoreApp.Entity.DataTransferObjects;
using bsStoreApp.Entity.Exceptions;
using bsStoreApp.Entity.Models;
using bsStoreApp.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/Books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _services;

        public BooksController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var Getbooks = _services.BookService.GetAllBooks(false);
            return Ok(Getbooks);
        }
        [HttpGet("GetOneBook")]
        public IActionResult GetOneBook(int id)
        {
            var Getonebooks = _services.BookService.GetOneBook(id, false);
            if (Getonebooks is null)
            {
                throw new BookNotFound(id);
            }
            else
            {
                return Ok(Getonebooks);
            }
        }
        [HttpPost]
        public IActionResult AddBook(BookDtoForInsertion bookDtoForInsertion)
        {
            //Hatayı gösterebilmek için modelstate isvalid yapılmalı.
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            else
            {
                _services.BookService.CreateOneBook(bookDtoForInsertion);
                return Ok("Kitap Eklendi.");
            }
        }
        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            _services.BookService.DeleteOneBook(id, false);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateBook(int id, BookDtoUpdate bookDtoUpdate)
        {
            if (bookDtoUpdate == null)
            {
                return NotFound();
            }
            else
            {
                _services.BookService.UpdateOneBook(id, bookDtoUpdate, false);
                return Ok(bookDtoUpdate);
            }
        }
    }
}
