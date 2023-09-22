using BookManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.ViewModel
{
    public class BookEditViewModel
    {
        public int BookId { get; set; }
        [Required, StringLength(100)]
        public string BookName { get; set; } = default!;
        [Required]
        public IFormFile? Picture { get; set; } = default!;
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [EnumDataType(typeof(BookType))]
        public BookType BookType { get; set; }
        public virtual List<Publisher> Publishers { get; set; } = new List<Publisher>();
    }
}
