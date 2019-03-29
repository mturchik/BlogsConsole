using NLog;
using BlogsConsole.Models;
using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

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
                int i;
                while (cont)
                {
                    var db = new BloggingContext();
                    Console.Write("Welcome to the Blog/Console:\n" +
                                  "=1 Display all Blogs\n" +
                                  "=2 Create Blog\n" +
                                  "=3 Create Post\n" +
                                  "=4 Display Posts\n" +
                                  "=5 Exit\n" +
                                  "===");
                    string input = Console.ReadLine();
                    switch (Validate.ValidateMenuSelection(input, 5))
                    {
                        case "1":
                            // Display all Blogs from the database
                            var query = db.Blogs.OrderBy(b => b.Name);
                            Console.WriteLine("All blogs in the database:");
                            i = 1;
                            foreach (var item in query)
                            {
                                Console.WriteLine(i++ + ") " + item);
                            }
                            break;
                        case "2":
                            // Create and save a new Blog
                            Console.Write("Enter a name for the new Blog:\n" +
                                "===");
                            var name = Console.ReadLine();
                            db.AddBlog(new Blog { Name = name });
                            logger.Info("Blog added - {name}", name);
                            break;
                        case "3":
                            //Query Blogs and sort by name
                            var listBlogs = db.Blogs.OrderBy(b => b.Name);
                            //Make list of custom menu sorting objects
                            List<MenuBlogId> menuBlogIds = new List<MenuBlogId>();
                            i = 1;
                            Console.WriteLine("All blogs in the database:");
                            foreach (var item in listBlogs)
                            {
                                //create menu sorting object with index value i, and corresponding blogId
                                menuBlogIds.Add(new MenuBlogId { menuId = i, blogId = item.BlogId });
                                //Display index value and Blog name
                                Console.WriteLine(i++ + ") " + item.Name);
                            }
                            Console.Write("Enter the BlogId of the Blog to post to:\n" +
                                          "===");
                            input = Validate.ValidateMenuSelection(Console.ReadLine(), i);
                            var intInput = int.Parse(input);
                            //Get element at specific index from the custom list, 
                            //and find the blog in the UNSORTED db with the blog ID that matches
                            int mBlogId = 0;
                            foreach(MenuBlogId m in menuBlogIds)
                            {
                                if(m.menuId == intInput)
                                {
                                    mBlogId = m.blogId;
                                }
                            }
                            Blog chosenBlog = db.Blogs.Find(mBlogId);

                            Console.Write("Enter post title:\n" +
                                              "=");
                            var title = Validate.ValidateBlank(Console.ReadLine());
                            logger.Info("Title chosen - {title}", title);
                            Console.Write("Enter post content:\n" +
                                          "=");
                            var content = Validate.ValidateBlank(Console.ReadLine());
                            logger.Info("Content chosen - {content}", content);
                            var post = new Post() { Title = title, Content = content, BlogId = chosenBlog.BlogId , Blog = chosenBlog };
                            db.AddPost(post);
                            logger.Info("Post added - {name}", post.PostId);
                            break;
                        case "4":
                            break;
                        case "5":
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
