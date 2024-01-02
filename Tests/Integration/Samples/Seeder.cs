using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Enums;
using Infrastructure.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Globalization;

namespace Tests.Integration.Samples
{
    public class Seeder
    {
        public static void Seed(ApplicationDbContext context, Guid id)
        {
            var books = SeedBooks();
            var bookInfoList = SeedBookInfo(books);
            var orderList = SeedOrders(10, id);
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
        public static List<Order> SeedOrders(int orderCount, Guid id)
        {
            var orderList = new List<Order>();
            for (var i = 0; i < orderCount; i++)
            {
                orderList.Add(
                    Order.Create(
                        i % 3,
                        id
                        ));
            }
            return orderList;
        }
        public static List<OrderedItem> SeedOrderItems(List<Order> orders, List<BookInfo> bookInfoList, int orderItemPerOrder)
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
        public static async Task<Guid> SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var user = new IdentityUser
            {
                UserName = "user",
                Email = "user@gmail.com"
            };
            var manager = new IdentityUser
            {
                UserName = "manager",
                Email = "manager@gmail.com"
            };
            var admin = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@gmail.com"
            };
            await userManager.CreateAsync(user, "zaq1@WSX");
            await userManager.CreateAsync(manager, "zaq1@WSX");
            await userManager.CreateAsync(admin, "zaq1@WSX");

            var userRole = new IdentityRole
            {
                Name = Roles.User.ToString()
            };
            var managerRole = new IdentityRole
            {
                Name = Roles.Manager.ToString()
            };
            var adminRole = new IdentityRole
            {
                Name = Roles.Admin.ToString()
            };
            await roleManager.CreateAsync(userRole);
            await roleManager.CreateAsync(managerRole);
            await roleManager.CreateAsync(adminRole);

            await userManager.AddToRoleAsync(user, userRole.Name);
            await userManager.AddToRolesAsync(manager, new List<string> {
                userRole.Name,
                managerRole.Name
            });
            await userManager.AddToRolesAsync(admin, new List<string> {
                userRole.Name,
                managerRole.Name,
                adminRole.Name
            });
            return Guid.Parse(user.Id);
        }
    }
    public class SeedToDb
    {
        //[Test]
        public async Task Seed()
        {
            var services = new ServiceCollection();
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 5; i++)
            {
                directory = Path.GetDirectoryName(directory);
            }
            directory = Path.Combine(directory, "BookStore");
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
            services.RegisterDbContext(configuration);
            services.RegisterRepositories();
            services.AddScoped<UserManager<IdentityUser>>();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddDataProtection();
            var serviceProvider = services.BuildServiceProvider();//tutaj
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var id = await Seeder.SeedUsers(userManager, roleManager);
            Seeder.Seed(context, id);
            
        }
        
    }
}