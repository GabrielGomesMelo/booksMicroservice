using System;
using System.Linq;
using AutoMapper;
using BooksApi.Models;
using BooksApi.ViewModels;

namespace BooksApi.AutoMapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookViewModel>();
            CreateMap<Book, UpdateBookViewModel>().ReverseMap();
        }
    }
}