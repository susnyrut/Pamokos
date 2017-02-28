using System;

namespace MyWebApplication.Models.CatViewModels
{
    public class CatListViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}