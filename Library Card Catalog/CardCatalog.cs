using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CardCatalog
{
    public class CardCatalog
    {
        public CardCatalog(string fileName)
        {
            FileName = fileName;
            bookList = new List<Book>();
        }

        private string FileName { get; set; }
        private List<Book> bookList { get; set; }


        public void AddABook()
        {
            Console.WriteLine("Add a book");

            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            Console.Write("Enter author: ");
            string author = Console.ReadLine();

            Console.Write("Enter genre: ");
            string genre = Console.ReadLine();

            Console.Write("Enter year: ");
            int year;
            string yearString = Console.ReadLine();
            int.TryParse(yearString, out year);
                        
            bookList.Add(new Book(title, author, genre, year));
            Console.WriteLine("Everything not saved will be lost.");
        }

        public void ListAllBooks()
        {
            try
            {
                using (Stream stream = File.Open(FileName, FileMode.Open))
                {
                    Console.WriteLine("Current books");
                    Console.WriteLine();
                    BinaryFormatter bf = new BinaryFormatter();
                    bookList = (List<Book>)bf.Deserialize(stream);
                    stream.Close();

                    foreach (Book book in bookList)
                    {
                        Console.WriteLine("Title: {0}, Author: {1}, Genre: {2}, Year: {3}",
                            book.Title, book.Author, book.Genre, book.YearPublished);
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Please add more books.");
                Console.WriteLine();
            }
        }
        public void Save()
        {
            Stream stream = File.Open(FileName, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, bookList);
            stream.Close();
        }
    }
}