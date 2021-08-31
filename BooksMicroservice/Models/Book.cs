using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApi.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(13)]
        public string Isbn { get; set; }
        
        [MaxLength(100)]
        public string Author { get; set; }
        
        [MaxLength(200)]
        public string Synopsis { get; set; }    
        
        public long Stock { get; set; }
    }
}