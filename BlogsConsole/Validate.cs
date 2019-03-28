using System;
using System.Text.RegularExpressions;

namespace BlogsConsole
{
    public static class Validate
    {
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //Loops asking for input until valid input is given, then returns as a string
        //Ex. MainMenu of 5 items, if user enters 1,2,3,4 or 5 it is valid input
        public static string ValidateMenuSelection(string s, int n)
        {
            bool invalid = true;
            while (invalid)
            {
                if (s.Length > 0)
                {
                    if (s.Length == 1)
                    {
                        if (int.TryParse(s.Substring(0, 1), out var sel))
                        {
                            if (sel <= n && sel >= 1)
                            {
                                invalid = false;
                            }
                            else
                            {
                                logger.Warn("Selection out of bounds.");
                                Console.Write("Selection out of bounds.\n" +
                                              "===");
                                s = Console.ReadLine();
                            }
                        }
                        else
                        {
                            logger.Warn("Input includes non-numbers.");
                            Console.Write("Input includes non-numbers.\n" +
                                          "===");
                            s = Console.ReadLine();
                        }
                    }
                    else
                    {
                        logger.Warn("Single digit input required.");
                        Console.Write("Single digit input required.\n" +
                                      "===");
                        s = Console.ReadLine();
                    }
                }
                else
                {
                    logger.Warn("Void input.");
                    Console.Write("Void input.\n" +
                                      "===");
                    s = Console.ReadLine();
                }
            }
            return s;
        }

        //Checks input for Full Name standard form(First Name [Space] Last Name)
        //Loop until valid name is given, then return name
        public static string ValidateName(string s)
        {
            Regex rx = new Regex("^[A-Za-z]{1,15}(\\s{1})[A-Za-z]{1,15}$");
            Regex rx2 = new Regex("\\P{L}+");
            bool invalid = true;
            while (invalid)
            {
                if (rx.IsMatch(s))
                {
                    if (!rx2.IsMatch(string.Join("", s.Split(' '))))
                    {
                        invalid = false;
                    }
                    else
                    {
                        logger.Warn("Names cannot have special characters.");
                        Console.Write("Names cannot have special characters.\n" +
                                          "===");
                        s = Console.ReadLine();
                    }
                }
                else
                {
                    logger.Warn("Name must match the following format: \"FirstName{Space}LastName\"");
                    Console.Write("Name must match the following format: \"FirstName{Space}LastName\"\n" +
                                      "===");
                    s = Console.ReadLine();
                }
            }

            return s;
        }

        //Verify input for a single word with only letters
        public static string ValidateWord(string s)
        {
            Regex rx = new Regex("^[A-Za-z]+$");
            bool invalid = true;
            while (invalid)
            {
                if (rx.IsMatch(s))
                {
                    invalid = false;
                }
                else
                {
                    logger.Warn("Invalid word, english letters only.");
                    Console.Write("Invalid word, english letters only.\n" +
                                      "===");
                    s = Console.ReadLine();
                }
            }

            return s;
        }

        //Verify string input is a valid double
        public static double ValidateDouble(string s)
        {
            bool invalid = true;
            double outDouble = 1.01;
            while (invalid)
            {
                if (double.TryParse(s, out outDouble))
                {
                    if (outDouble > 0)
                    {
                        invalid = false;
                    }
                    else
                    {
                        logger.Warn("Number must be positive.");
                        Console.Write("Number must be positive.\n" +
                                          "===");
                        s = Console.ReadLine();
                    }
                }
                else
                {
                    logger.Warn("Not a number.");
                    Console.Write("Not a number.\n" +
                                      "===");
                    s = Console.ReadLine();
                }
            }

            return outDouble;
        }

        //Verify string input is number greater than Zero
        //Loop until valid number is given, then return the string
        public static string ValidateNumber(string s)
        {
            Regex rx = new Regex("\\D+");
            bool invalid = true;
            while (invalid)
            {
                if (!rx.IsMatch(s))
                {
                    if (int.Parse(s) >= 0)
                    {
                        invalid = false;
                    }
                    else
                    {
                        logger.Warn("Number must be positive.");
                        Console.Write("Number must be positive.\n" +
                                          "===");
                        s = Console.ReadLine();
                    }
                }
                else
                {
                    logger.Warn("Non-Digit detected.");
                    Console.Write("Non-Digit detected.\n" +
                                      "===");
                    s = Console.ReadLine();
                }
            }

            return s;
        }

        //Verify string does not have a , or a "
        public static string ValidateForChars(string s)
        {
            Regex rx = new Regex(",+");
            Regex rx2 = new Regex("\"+");
            bool invalid = true;
            while (invalid)
            {
                if (s.Length > 0)
                {
                    if (!rx.IsMatch(s))
                    {
                        if (!rx2.IsMatch(s))
                        {
                            invalid = false;
                        }
                        else
                        {
                            logger.Warn("Quotation Mark detected.");
                            Console.Write("Quotation Mark detected.\n" +
                                              "===");
                            s = Console.ReadLine();
                        }
                    }
                    else
                    {
                        logger.Warn("Comma detected.");
                        Console.Write("Comma detected.\n" +
                                          "===");
                        s = Console.ReadLine();
                    }
                }
                else
                {
                    logger.Warn("Void input.");
                    Console.Write("Void input.\n" +
                                  "===");
                    s = Console.ReadLine();
                }
            }

            return s;
        }

        public static DateTime ValidateDate(string s)
        {
            bool invalid = true;
            DateTime date = new DateTime();
            while (invalid)
            {
                if (DateTime.TryParse(s, out date))
                {
                    invalid = false;
                }
                else
                {
                    logger.Warn("Invalid date.");
                    Console.Write("Invalid date. Ex) MM/DD/YYYY\n" +
                                  "===");
                    s = Console.ReadLine();
                }
            }

            return date;
        }

        public static string ValidateBlank(string s)
        {
            bool invalid = true;
            while (invalid)
            {
                if (s.Length != 0)
                {
                    invalid = false;
                }
                else
                {
                    logger.Warn("Input may not be blank.");
                    Console.Write("Input may not be blank.\n" +
                                  "===");
                    s = Console.ReadLine();
                }
            }

            return s;
        }
    }
}