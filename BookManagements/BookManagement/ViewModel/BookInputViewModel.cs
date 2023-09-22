using BookManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.ViewModel
{
    public class BookInputViewModel
    {
        public int BookId { get; set; }
        [Required, StringLength(100)]
        public string BookName { get; set; } = default!;
        [Required]
        public IFormFile? Picture { get; set; } 
  
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [EnumDataType(typeof(BookType))]
        public BookType BookType { get; set; }
        public virtual List<Publisher> Publishers { get; set; } = new List<Publisher>();
    }
}
