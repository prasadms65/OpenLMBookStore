using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Dto
{
    public class BooksDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
