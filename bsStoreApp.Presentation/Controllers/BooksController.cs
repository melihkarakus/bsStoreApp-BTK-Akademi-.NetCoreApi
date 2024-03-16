using bsStoreApp.Entity.DataTransferObjects;
using bsStoreApp.Entity.Exceptions;
using bsStoreApp.Entity.Models;
using bsStoreApp.Presentation.ActionFilters;
using bsStoreApp.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
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
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var Getbooks = await _services.BookService.GetAllBooksAsync(false);
            return Ok(Getbooks);
        }
        [HttpGet("GetOneBook")]
        public async Task<IActionResult> GetOneBookAsync(int id)
        {
            var Getonebooks = await _services.BookService.GetOneBookAsync(id, false);
            if (Getonebooks is null)
            {
                throw new BookNotFound(id);
            }
            else
            {
                return Ok(Getonebooks);
            }
        }


        [ServiceFilter(typeof(ValidationFilterAttribute))]//Hatayı gösterebilmek için modelstate isvalid yapılmalı.
        [HttpPost]
        public async Task<IActionResult> AddBookAsync(BookDtoForInsertion bookDtoForInsertion)
        {
            await _services.BookService.CreateOneBookAsync(bookDtoForInsertion);
            return Ok("Kitap Eklendi.");
        }
        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            await _services.BookService.DeleteOneBookAsync(id, false);
            return Ok();
        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut]
        public async Task<IActionResult> UpdateBookAsync(int id, BookDtoUpdate bookDtoUpdate)
        {         
                await _services.BookService.UpdateOneBookAsync(id, bookDtoUpdate, false);
                return Ok(bookDtoUpdate);
        }
    }
}
