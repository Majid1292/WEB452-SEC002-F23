using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovie.Models
{

    public class TodoItem
    {
        public int Id  { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}