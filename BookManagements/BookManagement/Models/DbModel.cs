using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Models
{
    public enum BookType { pdf = 1, EBook }
    public class Book
    {
   
        public int BookId { get; set; }
        [Required, StringLength(100)]
        public string BookName { get; set; } = default!;
        [Required, StringLength(100)]
        public string Picture { get; set; } = default!;
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [EnumDataType(typeof(BookType))]
        public BookType BookType { get; set; }
        public virtual ICollection<Publisher> Publishers { get; set; } = new List<Publisher>();
    }
    public class Publisher
    {

        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = default!;
        [Column(TypeName = "date"), DataType(DataType.Date)]
        public DateTime PublisherDate { get; set; }
        [Required]
        public int TotalPublisher { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book? Books { get; set; } = default!;
    }
    public class BookOrder
    {
        public int BookOrderId { get; set; }
        public string OrderName { get; set; } = default!;
        [Column(TypeName = "date"), DataType(DataType.Date)]
        public DateTime BookOrderDate { get; set; }
    }
    public class BookManagementDbContext: DbContext
    {
        public BookManagementDbContext(DbContextOptions<BookManagementDbContext> option) : base(option) { }
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Publisher> Publishers { get; set; } = default!;
        public DbSet<BookOrder> BookOrders { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           for(int i = 1; i <= 7; i++)
            {
                Book book1 = new Book { BookId = i, 
                    BookName = $"Book{i:00}",
                    Price = i * 950.00M,
                    BookType = BookType.pdf, 
                    Picture = $"{i}.jpg"
                };
                modelBuilder.Entity<Book>().HasData(book1);
            }
            for (int i = 1; i <= 7; i++)
            {

                Publisher publisher = new Publisher {
                    PublisherId = i, 
                    PublisherName= $"Publisher{i:00}",
                    PublisherDate = DateTime.Now.AddDays(-23 * i), 
                    TotalPublisher = 50 * i,
                    BookId = i 
                };

                modelBuilder.Entity<Publisher>().HasData(publisher);
            }
            for (int i = 8; i <= 10; i++)
            {
                Publisher pi = new Publisher {
                    PublisherId = i,
                    PublisherName = $"Publisher{i:00}",
                    PublisherDate = DateTime.Now.AddDays(-23 * i),
                    TotalPublisher = 50 * i,
                    BookId = i - 7
                };

                modelBuilder.Entity<Publisher>().HasData(pi);


            }
        }
    }
}
