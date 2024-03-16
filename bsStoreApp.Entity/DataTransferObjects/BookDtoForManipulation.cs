using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Entity.DataTransferObjects
{
    public abstract class BookDtoForManipulation
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is a reqired field.")]
        [MinLength(2, ErrorMessage = "Title must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Title must consist of at maximum 50 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Title is a required field.")]
        [Range(10, 1000)]
        public decimal Price { get; set; }
    }
}
