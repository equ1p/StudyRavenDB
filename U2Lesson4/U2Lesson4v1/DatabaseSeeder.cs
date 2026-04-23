using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Raven.Client.Documents;
using U2Lesson4.Models;

namespace U2Lesson4;

public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IDocumentStore store)
        {
            using (var session = store.OpenAsyncSession())
            {
                // Перевіряємо, чи є вже дані в базі
                var anyProductExists = await session.Query<Product>().AnyAsync();
                if (anyProductExists)
                {
                    Console.WriteLine("Data already exists. Skipping generation.");
                    return;
                }

                Console.WriteLine("Generating mock data...");

                // 1. Створюємо категорії
                var electronics = new Category { Name = "Electronics" };
                var books = new Category { Name = "Books" };
                var clothing = new Category { Name = "Clothing" };
                var groceries = new Category { Name = "Groceries" };

                await session.StoreAsync(electronics);
                await session.StoreAsync(books);
                await session.StoreAsync(clothing);
                await session.StoreAsync(groceries);

                // 2. Створюємо продукти
                var products = new List<Product>
                {
                    // Electronics
                    new Product { Name = "Apple MacBook Air Laptop", Category = electronics.Id },
                    new Product { Name = "Samsung Galaxy Smartphone", Category = electronics.Id },
                    new Product { Name = "Sony WH-1000XM5 Headphones", Category = electronics.Id },
                    new Product { Name = "Dell 27\" Monitor", Category = electronics.Id },
                    new Product { Name = "Logitech Wireless Mouse", Category = electronics.Id },

                    // Books
                    new Product { Name = "Clean Code - Robert C. Martin", Category = books.Id },
                    new Product { Name = "The Lord of the Rings - J.R.R. Tolkien", Category = books.Id },
                    new Product { Name = "C# 12 in a Nutshell", Category = books.Id },

                    // Clothing
                    new Product { Name = "Basic White T-Shirt", Category = clothing.Id },
                    new Product { Name = "Levi's 501 Jeans", Category = clothing.Id },
                    new Product { Name = "The North Face Winter Jacket", Category = clothing.Id },
                    new Product { Name = "Nike Air Max Sneakers", Category = clothing.Id },

                    // Groceries
                    new Product { Name = "Lavazza Coffee Beans", Category = groceries.Id },
                    new Product { Name = "Milka Chocolate", Category = groceries.Id },
                    new Product { Name = "Cheddar Cheese", Category = groceries.Id },
                    new Product { Name = "Extra Virgin Olive Oil", Category = groceries.Id },
                    new Product { Name = "Organic Avocado", Category = groceries.Id },
                    new Product { Name = "Freshly Baked Sourdough Bread", Category = groceries.Id }
                };

                // Зберігаємо всі продукти
                foreach (var product in products)
                {
                    await session.StoreAsync(product);
                }

                // 3. Зберігаємо зміни в базу
                await session.SaveChangesAsync();
                Console.WriteLine("Mock data successfully added to the RavenDB database!");
            }
        }
    }