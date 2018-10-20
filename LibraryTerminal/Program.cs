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
            library[0] = new List<string>() { "0","Behold a Pale Horse", "Milton William Cooper", "Fiction", "in", "" };
            library[1] = new List<string>() { "1","The Master and Margarita", "Mikhail Bulgakov", "Fiction", "in", "" };
            library[2] = new List<string>() { "2","Galveston: A Novel", "Nic Pizzolatto", "Fiction", "in", "" };
            library[3] = new List<string>() { "3","Infected: A Novel", "Scott Sigler", "Fiction", "in", "" };
            library[4] = new List<string>() { "4","Contagious (Infected Book 2)", "Scott Sigler", "Fiction", "in", "" };
            library[5] = new List<string>() { "5","Confessions of an Economic Hit Man", "John Perkins", "Biography", "in", "" };
            library[6] = new List<string>() { "6","How to Steal a Dog: A Novel", "Barbara O'Connor", "Fiction", "in", "" };
            library[7] = new List<string>() { "7","The Very Hungry Caterpillar", "Eric Carle", "Kids", "in", "" };
            library[8] = new List<string>() { "8","Goodnight Moon", "Margaret Wise Brown", "Kids", "in", "" };
            library[9] = new List<string>() { "9","Brown Bear, Brown Bear, What Do You See?", "Bill Marin Jr.", "Kids", "in", "" };
            library[10] = new List<string>() { "10","Full Disclosure", "Stormy Daniels", "Biography", "in", "" };
            library[11] = new List<string>() { "11","The Adventures of Captain Underpants", "Dav Pilkey", "Action/Adventure", "in", "" };
            library[12] = new List<string>() { "12","The Adventures of Captain Underpants", "Dav Pilkey", "Action/Adventure", "in", "" };
        }

        static void print(Dictionary<int, List<string>> library, string sorter)
        {
            Console.WriteLine($"*{sorter} sorted");     //let user know what category is being sorted
            Console.WriteLine($"{"ID",-2}){"Title:",-40}|{"Auther:",-21}|{"Genre:",-16}|{"Status:",-8}|{"Return:",-6}|");         //
            Console.WriteLine("=".PadLeft(100, '='));                                                                             //print categories
            foreach (var v in library)                                                                                            //
                Console.Write($"{v.Value[0],2}){v.Value[1],-40}|{v.Value[2],-21}|{v.Value[3],-16}|{v.Value[4],-8}|{v.Value[5],-7}|\n");//
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
                return library;    //Exits SORT() function when satisfied
            sorter = labels[s];
            if (s == 0)
                library = SORT(library.OrderBy(a => int.Parse(a.Value[0])).ToDictionary(a => a.Key, a => a.Value), labels, ref sorter);
            else if (s >= 1 && s <= 5)//(s >= 1 && s <= 5)
                library = SORT(library.OrderBy(a => a.Value[s]).ToDictionary(a => a.Key, a => a.Value), labels, ref sorter);
            else
            {                                                  //
                Console.WriteLine("BAD NUMBER");               //user entered wrong
                library = SORT(library, labels, ref sorter);   //
            }
            return library;
        }

        static Dictionary<int, List<string>> Search(Dictionary<int, List<string>> library, List<string> labels, string sorter)
        {
            Dictionary<int, List<string>> tempD = new Dictionary<int, List<string>>();
            int s = options(labels, "Search For");
            if (s == 6)
                return library;
            else if (s >= 0 && s <= 5)//(s >= 1 && s <= 5)
            {
                Console.Write($"Search {labels[s]} ");
                string word = Console.ReadLine();                        //word to search in label[s].
                if (s == 0)
                    tempD = library.Where(a => a.Key == int.Parse(word)).ToDictionary(a =>a.Key, a => a.Value);//sort label[s] then sub list collections containing that word[word] in label.
                else
                    tempD = library.Where(a => a.Value[s].Contains(word)).ToDictionary(a => a.Key, a => a.Value); ;//sort label[s] then sub list collections containing that word[word] in label.
                print(tempD.ToDictionary(a => a.Key, b => b.Value), sorter); //pump new dictionary into print function so user can see it.
                if (tempD.Count() == 1 && tempD[tempD.Keys.First()][3] == "in")
                    RENTER(ref library, labels, sorter, tempD.Keys.First());
                Search(library, labels, sorter);                         //go back to search function.
            }
            else
            {
                Console.WriteLine("BAD NUMBER");
                SORT(library, labels, ref sorter);
            }
            return library;
        }
        static void RENTER(ref Dictionary<int,List<string>> library, List<string> labels, string sorter, int key)
        {
            Console.Write("Enter 'y' to rent: ");
            if (Console.ReadLine() == "y")
            {
                DateTime d = new DateTime();
                d = DateTime.Now.AddDays(14);
                library[key][3] = "out";
                library[key][4] = d.ToString("MM/dd").ToString();
                print(library.Where(a => a.Key == key).ToDictionary(a => a.Key, b => b.Value), sorter);
                return;
            }
        }

        static void Main(string[] args)
        {
            Dictionary<int, List<string>> library = new Dictionary<int, List<string>>(), temp = new Dictionary<int, List<string>>();
            List<string> labels = new List<string>() { "ID", "Title:", "Author:", "Genre:", "Status:", "Date:" };
            loader(library);
            string sorter = "ID";
            string c = "";
            while (true)
            {
                try
                {
                    Console.WriteLine("~MAIN MENU~\n1) Display\n2) Checkout\n3) Exit\n----------\nChoose an option: ");
                    c = Console.ReadLine();
                    if (c == "3")
                        break;
                    else if (c == "2")
                        Search(library, labels, sorter);
                    else if (c == "1")
                        library = SORT(library, labels, ref sorter);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        //static Dictionary<string, List<string>> RENT(Dictionary<string, List<string>> library, List<string> labels, string sorter, DateTime d)
        //{
        //    string temp = "";
        //    Console.WriteLine("~RENT BY ID~");
        //    Console.Write("Enter ID ");
        //    temp = Console.ReadLine();
        //    if (!int.TryParse(temp, out int n) || int.Parse(temp) > library.Count() - 1 || int.Parse(temp) < 0)//validate temp
        //    {                                                                                                  //
        //        Console.WriteLine("BAD INPUT");                                                                //
        //        library = RENT(library, labels, sorter, d);                                                    //
        //    }
        //    if (library[n][3] == "out")                                                                        //If already checked out cry :(
        //    {                                                                                                  //
        //        Console.WriteLine("That books already Checked out :(");                                        //
        //        return library;                                                                                //
        //    }
        //    d = DateTime.Now.AddDays(14);
        //    Console.WriteLine(d.ToString());
        //    library[n][3] = "out";
        //    library[n][4] = d.ToString("MM/dd").ToString();
        //    print(library.Where(a => a.Key == n).ToDictionary(a => a.Key, b => b.Value), sorter);
        //    return library;
        //}

    }
}