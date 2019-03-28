using NLog;
using BlogsConsole.Models;
using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {
                bool cont = true;
                while (cont)
                {
                    var db = new BloggingContext();
                    Console.Write("Welcome to the Blog/Console~\n" +
                                  "1. Display all Blogs\n" +
                                  "2. Add Blog\n" +
                                  "3. Create Post\n" +
                                  "4. Exit\n" +
                                  "===");
                    string input = Console.ReadLine();
                    switch (Validate.ValidateMenuSelection(input, 4))
                    {
                        case "1":
                            // Display all Blogs from the database
                            var query = db.Blogs.OrderBy(b => b.Name);
                            Console.WriteLine("All blogs in the database:");
                            foreach (var item in query)
                            {
                                Console.WriteLine(item.Name);
                            }
                            break;
                        case "2":
                            // Create and save a new Blog
                            Console.Write("Enter a name for a new Blog: ");
                            var name = Console.ReadLine();

                            var blog = new Blog { Name = name };
                            
                            db.AddBlog(blog);
                            logger.Info("Blog added - {name}", name);
                            break;
                        case "3":
                            var listBlogs = db.Blogs.OrderBy(b => b.BlogId);
                            Console.WriteLine("All blogs in the database:");
                            foreach (var item in listBlogs)
                            {
                                Console.WriteLine("BlogId: " + item.BlogId + " - BlogName: " + item.Name);
                            }
                            Console.Write("Enter the BlogId of the Blog to post to:\n" +
                                          "=");
                            input = Console.ReadLine();
                            var intInput = int.Parse(input);

                            Blog chosenBlog = db.Blogs.Find(intInput);

                            Console.Write("Enter post title:\n" +
                                              "=");
                            var title = Validate.ValidateBlank(Console.ReadLine());
                            Console.Write("Enter post content:\n" +
                                          "=");
                            var content = Validate.ValidateBlank(Console.ReadLine());
                            var post = new Post() { Title = title, Content = content, BlogId = chosenBlog.BlogId , Blog = chosenBlog };
                            db.AddPost(post);
                            logger.Info("Post added - {name}", post.PostId);
                            break;
                        case "4":
                            cont = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}
