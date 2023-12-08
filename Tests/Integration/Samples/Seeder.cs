using Newtonsoft.Json;
using Tests.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Migrations;
using System.Globalization;

namespace Tests.Integration.Samples
{
    public class Seeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            var books = SeedBooks();
            var bookInfoList = SeedBookInfo(books);
            var orderList = SeedOrders(10);
            var orderItemList = SeedOrderItems(orderList, bookInfoList, 2);

            context.Books.AddRange(books);
            context.BookInfo.AddRange(bookInfoList);
            context.Orders.AddRange(orderList);
            context.OrderedItems.AddRange(orderItemList);
            context.SaveChanges();
        }
        public static List<Book> SeedBooks()
        {
            var bookJson = JsonConvert.DeserializeObject(File.ReadAllText(IntegrationTestBase.samplesDir + "Books.Json"));
            var bookProperties = new List<string>()
            {
                "title","author","description","pages"
            };
            var bookData = JsonToStringConverter.ToJson(bookJson, bookProperties);
            var books = new List<Book>();
            Book bookToAdd;

            foreach (var book in bookData)
            {
                bookToAdd = Book.Create(
                    book[0],
                    book[1],
                    book[2],
                    int.Parse(book[3])
                    );
                books.Add(bookToAdd);
            }
            return books;
        }
        public static List<BookInfo> SeedBookInfo(List<Book> books)
        {
            var bookInfoJson = JsonConvert.DeserializeObject(File.ReadAllText(IntegrationTestBase.samplesDir + "BookInfo.Json"));
            var bookInfoProperties = new List<string>()
            {
                "Price","Quantity"
            };
            var bookInfoData = JsonToStringConverter.ToJson(bookInfoJson, bookInfoProperties);
            var bookInfoList = new List<BookInfo>();
            BookInfo bookInfoToAdd;
            var i = 0;
            var bookCount = books.Count() > bookInfoData.Count() ? bookInfoData.Count() : books.Count();

            double price;
            int quantity;

            foreach (var bookInfo in bookInfoData.Take(bookCount))
            {
                price = double.Parse(bookInfo[0], CultureInfo.InvariantCulture);
                quantity = int.Parse(bookInfo[1]);

                bookInfoToAdd = BookInfo.Create(
                    price,
                    quantity,
                    books[i]
                    );
                bookInfoList.Add(bookInfoToAdd);
                i++;
            }
            return bookInfoList;
        }
        public static List<Order> SeedOrders(int orderCount)
        {
            var orderList = new List<Order>();
            for (var i = 0; i < orderCount; i++)
            {
                orderList.Add(
                    Order.Create(
                        i % 3,
                        Guid.NewGuid()
                        ));
            }
            return orderList;
        }
        public static List<OrderedItem> SeedOrderItems(List<Order> orders, List<BookInfo> bookInfoList , int orderItemPerOrder)
        {
            var orderItemCount = orderItemPerOrder * orders.Count;
            var orderItemList = new List<OrderedItem>();
            Random rnd = new Random();
            for (var i = 0; i < orderItemCount; i++)
            {
                orderItemList.Add(
                    OrderedItem.Create(
                        bookInfoList[rnd.Next(bookInfoList.Count)],
                        orders[i / orderItemPerOrder]
                        ));
            }
            return orderItemList;
        }
    }
}
