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
            
            var bookInfoJson = JsonConvert.DeserializeObject(File.ReadAllText(IntegrationTestBase.samplesDir + "BookInfo.Json"));
            var bookInfoProperties = new List<string>()
            {
                "Price","Quantity"
            };
            var bookInfoData = JsonToStringConverter.ToJson(bookInfoJson, bookInfoProperties);
            var bookInfoList = new List<BookInfo>();
            BookInfo bookInfoToAdd;
            var i = 0;

            double price;
            int quantity;

            foreach (var bookInfo in bookInfoData)
            {
                price = double.Parse(bookInfo[0], CultureInfo.InvariantCulture);
                quantity = int.Parse(bookInfo[1]);
                
                bookInfoToAdd = BookInfo.Create(
                    price,
                    quantity,
                    books[i]
                    );
                bookInfoList.Add(bookInfoToAdd);
            }

            var orderList = new List<Order>();
            var orderCount = 10;
            for(var j=0; j < orderCount; j++)
            {
                orderList.Add(
                    Order.Create(
                        j % 3,
                        Guid.NewGuid()
                        ));
            }

            var orderItemPerOrder = 2;
            var orderItemCount = orderItemPerOrder * orderCount;
            var orderItemList = new List<OrderedItem>();
            Random rnd = new Random();
            for (var j = 0; j < orderItemCount; j++)
            {
                orderItemList.Add(
                    OrderedItem.Create(
                        bookInfoList[rnd.Next(bookInfoList.Count)],
                        orderList[j/orderItemPerOrder]
                        ));
            }

            context.Books.AddRange(books);
            context.BookInfo.AddRange(bookInfoList);
            context.Orders.AddRange(orderList);
            context.OrderedItems.AddRange(orderItemList);
            context.SaveChanges();
        }
    }
}
