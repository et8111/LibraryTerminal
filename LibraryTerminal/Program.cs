using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Still need to return a book. Possibly return a book via 'search()' instead of using 'RENT()'.
 * Set something (one or 2 itmes) equal to rented out at initialization so return can be tested.
 */

namespace LibraryTerminal
{
    class Program
    {
        static void loader(Dictionary<int, List<string>> library)
        {//strictly for loading base info at start
            library[0] = new List<string>() { "Behold a Pale Horse", "Milton William Cooper", "Fiction", "in", "" };
            library[1] = new List<string>() { "The Master and Margarita", "Mikhail Bulgakov", "Fiction", "in", "" };
            library[2] = new List<string>() { "Galveston: A Novel", "Nic Pizzolatto", "Fiction", "in", "" };
            library[3] = new List<string>() { "Infected: A Novel", "Scott Sigler", "Fiction", "in", "" };
            library[4] = new List<string>() { "Contagious (Infected Book 2)", "Scott Sigler", "Fiction", "in", "" };
            library[5] = new List<string>() { "Confessions of an Economic Hit Man", "John Perkins", "Biography", "in", "" };
            library[6] = new List<string>() { "How to Steal a Dog: A Novel", "Barbara O'Connor", "Fiction", "in", "" };
            library[7] = new List<string>() { "The Very Hungry Caterpillar", "Eric Carle", "Kids", "in", "" };
            library[8] = new List<string>() { "Goodnight Moon", "Margaret Wise Brown", "Kids", "in", "" };
            library[9] = new List<string>() { "Brown Bear, Brown Bear, What Do You See?", "Bill Marin Jr.", "Kids", "in", "" };
            library[10] = new List<string>() { "Full Disclosure", "Stormy Daniels", "Biography", "in", "" };
            library[11] = new List<string>() { "The Adventures of Captain Underpants", "Dav Pilkey", "Action/Adventure", "in", "" };
        }

        static void print(Dictionary<int, List<string>> library, string sorter)
        {
            Console.WriteLine($"*{sorter} sorted");     //let user know what category is being sorted
            Console.WriteLine($"{"ID",-2}){"Title:",-40}|{"Auther:",-21}|{"Genre:",-16}|{"Status:",-8}|{"Return:",-6}|");         //
            Console.WriteLine("=".PadLeft(100, '='));                                                                             //print categories
            foreach (var v in library)                                                                                            //
                Console.Write($"{v.Key,2}){v.Value[0],-40}|{v.Value[1],-21}|{v.Value[2],-16}|{v.Value[3],-8}|{v.Value[4],-7}|\n");//
            Console.WriteLine();
        }
        static int options(List<string> labels, string msg)
        {
            Console.WriteLine($"~{msg.ToUpper()}~");
            for (int i = 0; i < labels.Count; i++)            //
                Console.WriteLine($"{i + 1}) {labels[i]}");   //Print all labels for user to choose from
            Console.Write($"7) Exit\n----------\n{msg}: ");   //
            Console.WriteLine();                              //
            return int.Parse(Console.ReadLine()) - 1;
        }
        static Dictionary<int, List<string>> SORT(Dictionary<int, List<string>> library, List<string> labels, ref string sorter)
        {
            print(library, sorter);
            int s = options(labels, "Sort By");
            if (s == 6)
                return library;              //Exits SORT() function when satisfied
            else if (s == 0)
            {
                sorter = labels[s+1];               //set 'sorter' equal to the new label to sort by then pass lambda sort as dictionary back into SORT() if ID
                library = SORT(library.OrderBy(a => a.Key).ToDictionary(a => a.Key, a => a.Value), labels, ref sorter);
            }
            else if (s >= 1 && s <= 5)
            {
                s--;
                sorter = labels[s+1];               //set 'sorter' equal to the new label to sort by then pass lambda sort as dictionary back into sort anything else
                library = SORT(library.OrderBy(a => a.Value[s]).ToDictionary(a => a.Key, a => a.Value), labels, ref sorter);
            }
            else
            {                                                   //
                Console.WriteLine("BAD NUMBER");                //user entered wrong
                library = SORT(library, labels, ref sorter);   //
            }
            return library;
        }

        static void Search(Dictionary<int, List<string>> library, List<string> labels, string sorter)
        {
            int s = options(labels, "Search For");
            if (s == 6)
                return;
            else if (s == 0)
            {
                Console.Write($"Search {labels[s]} ");                                    //Sort if label == 'ID' (Key vs Value)      
                string word = Console.ReadLine();                                         //      
                var k = library.Where(a => a.Key == int.Parse(word)); //
                print(k.ToDictionary(a => a.Key, b => b.Value), sorter);                  //      
                Search(library, labels, sorter);                                          // 
            }
            else if (s >= 1 && s <= 5)
            {
                s--;
                Console.Write($"Search {labels[s + 1]} ");                                        //Prompt user for
                string word = Console.ReadLine();                                               //word to search in label[s].
                var k = library.Where(a => a.Value[s].Contains(word)); //sort label[s] then sub list collections containing that word[word] in label.
                print(k.ToDictionary(a => a.Key, b => b.Value), sorter);                            //pump new dictionary into print function so user can see it.
                Search(library, labels, sorter);                                                //go back to search function.
            }
            else
            {
                Console.WriteLine("BAD NUMBER");
                SORT(library, labels, ref sorter);
            }
            return;
        }

        static Dictionary<int, List<string>> RENT(Dictionary<int, List<string>> library, List<string> labels, string sorter, DateTime d)
        {
            string temp = "";
            //print(library, sorter);
            Console.WriteLine("~RENT BY ID~");
            Console.Write("Enter ID ");
            temp = Console.ReadLine();
            if (!int.TryParse(temp, out int n) || int.Parse(temp) > library.Count() - 1 || int.Parse(temp) < 0)//validate temp
            {                                                                                                  //
                Console.WriteLine("BAD INPUT");                                                                //
                library = RENT(library, labels, sorter, d);                                                    //
            }
            if (library[n][3] == "out")                                                                        //If already checked out cry :(
            {                                                                                                  //
                Console.WriteLine("That books already Checked out :(");                                        //
                return library;                                                                                //
            }
            d = DateTime.Now.AddDays(14);
            Console.WriteLine(d.ToString());
            library[n][3] = "out";
            library[n][4] = d.ToString("MM/dd").ToString();
            print(library.Where(a => a.Key == n).ToDictionary(a => a.Key, b => b.Value), sorter);
            return library;
        }

        static void Main(string[] args)
        {
            Dictionary<int, List<string>> library = new Dictionary<int, List<string>>(), temp = new Dictionary<int, List<string>>();
            List<string> labels = new List<string>() { "ID", "Title:", "Author:", "Genre:", "Status:", "Date:" };
            DateTime d = new DateTime();
            loader(library);
            string sorter = "Name";
            string c = "";
            while (true)
            {
                try
                {
                    Console.WriteLine("~MAIN MENU~\n1) Sort\n2) Search\n3) Rent\n4) Exit\n----------\nChoose an option: ");
                    c = Console.ReadLine();
                    if (c == "4")
                        break;
                    else if (c == "3")
                        library = RENT(library, labels, sorter, d);
                    else if (c == "2")
                        Search(library, labels, sorter);
                    else if (c == "1")
                        library = SORT(library, labels, ref sorter);
                    //library = SORT(library, labels, ref sorter);
                    //print(library, sorter);
                    //Search(library, labels, sorter);
                    //library = RENT(library, labels, sorter, d);
                    //library = RENT(library, labels, sorter, d);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}