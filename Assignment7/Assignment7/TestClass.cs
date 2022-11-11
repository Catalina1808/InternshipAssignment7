using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7
{
    internal class TestClass
    {
        public static void Main()
        {
            int[] numbers = { 120, 77, 92, 81, 66, 52 };

            IEnumerable<int> numbersQuery =
                from number in numbers
                where number > 70
                select number;

            WriteItems<int>(numbersQuery);
            Console.WriteLine(numbers.Max());
            Console.WriteLine(numbers.Min());
            Console.WriteLine(numbers.Average());
            Console.WriteLine(numbers.Sum());

            var sortedNumbers = numbersQuery.OrderBy(x => x).ToList();

            WriteItems<int>(sortedNumbers);

            WriteItems<int>(sortedNumbers.OrderByDescending(x => x).ToList());

            var genres1 = new List<string>
            {
                "genre1",
                "genre2",
            };
            var genres2 = new List<string>
            {
                "genre3",
                "genre1",
            };

            var books = new List<Book> {
                new Book(1, "title1", "description1", 1, genres1),
                new Book(2, "title2", "description2", 2, genres2),
                new Book(3, "title3", "description3", 2, genres1),
                new Book(4, "title4", "description4", 1, genres2),
                new Book(5, "title5", "description5", 2, genres1),
                };

            var selectedTitles = books.Skip(2).Select(b => b.Title).Reverse().Cast<string>();
            WriteItems<string>(selectedTitles);

            selectedTitles = selectedTitles.TakeWhile(title => title.Contains("3")).ToList();
            WriteItems<string>(selectedTitles);

            var totalGenres = books.Take(3).SelectMany(b => b.Genres).AsEnumerable();
            WriteItems<string>(totalGenres);

            totalGenres = totalGenres.Distinct();
            WriteItems<string>(totalGenres);

            var authors = new List<Author>
            {
                new Author(1, "name1"),
                new Author(2, "name2")
            };

            var innerJoin = from book in books
                            join author in authors
                            on book.AuthorId equals author.Id
                            select new
                            {
                                BookTitle = book.Title,
                                AuthorName = author.Name
                            };
            WriteItems<object>(innerJoin);
            Console.WriteLine();

            var groupBy = from book in books
                          group book by book.AuthorId
                          into newBooks
                          select newBooks;
;
            foreach (var book in groupBy)
            {
                foreach (var item in book)
                {
                    Console.Write(item.Title + " " );
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            IEnumerable<string> concatQuery = books.Select(book => book.Title).Concat(authors.Select(author => author.Name));
            WriteItems<string>(concatQuery);

            IEnumerable<string> unionQuery = genres1.Select(genre => genre).Union(genres2.Select(genre => genre));
            WriteItems<string>(unionQuery);

            IEnumerable<string> exceptQuery = genres1.Select(genre => genre).Except(genres2.Select(genre => genre));
            WriteItems<string>(exceptQuery);

            var firstVeryShortTitle = books.Select(b => b.Title).FirstOrDefault(name => name.Length <7);
            var lastVeryShortTitle = books.Select(b => b.Title).LastOrDefault(name => name.Length < 7);
            Console.WriteLine(firstVeryShortTitle+ " " + lastVeryShortTitle);

            var aggregate = totalGenres.Aggregate((g1, g2) => g1 + ", " + g2);
            Console.WriteLine(aggregate);
            Console.WriteLine(totalGenres.Count());
            Console.WriteLine(totalGenres.ElementAt(2));

            if (totalGenres.Contains("genre1"))
            {
                Console.WriteLine("There is a genre called genre1.");
            }

            Console.WriteLine(genres1.SequenceEqual(genres2));

            var emptyCollection = Enumerable.Empty<string>();
            emptyCollection = Enumerable.Repeat("string", 10);
            WriteItems<string> (emptyCollection);

            int[] List = { };
            var result = List.DefaultIfEmpty();
            Console.Write("List without values:");
            foreach (var val in result)
            {
                Console.WriteLine(val);
            }

            IEnumerable<int> numberSequence = Enumerable.Range(1, 10);
            WriteItems<int> (numberSequence);
        }

        public static void WriteItems<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Console.Write(item + " " );
            }
            Console.WriteLine();
        }
    }
}
