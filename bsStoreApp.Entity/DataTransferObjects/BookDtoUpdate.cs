using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsStoreApp.Entity.DataTransferObjects
{
    public class BookDtoUpdate : BookDtoForManipulation
    {
        [Required]
        public int Id { get; set; }
    }
}
