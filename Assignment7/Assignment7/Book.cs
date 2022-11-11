using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7
{
    internal class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public int AuthorId { get; set; }
        public List<string> Genres { get; set; }

        public Book(int id, string title, string description, int authorId, List<string> genres)
        {
            Id = id;
            Title = title;  
            Description = description;
            AuthorId = authorId;
            Genres = genres;
        }
    }
}
