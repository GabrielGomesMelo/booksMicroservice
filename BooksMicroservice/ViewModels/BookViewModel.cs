using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.ViewModels
{
    public class BookViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}