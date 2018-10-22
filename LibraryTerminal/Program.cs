using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*IMPORTANT MEMO:
 * In order to rent/return a book the user must reduce the 'checkout' option to 1 possibility.
 */
namespace LibraryTerminal
{
    class Program
    {
        static void loader(Dictionary<int, List<string>> library)
        {//strictly for loading base info at start
            library[0] = new List<string>() { "1","Behold a Pale Horse", "Milton William Cooper", "Fiction", "in", "" };
            library[1] = new List<string>() { "2","The Master and Margarita", "Mikhail Bulgakov", "Fiction", "in", "" };
            library[2] = new List<string>() { "3","Galveston: A Novel", "Nic Pizzolatto", "Fiction", "in", "" };
            library[3] = new List<string>() { "4","Infected: A Novel", "Scott Sigler", "Fiction", "in", "" };
            library[4] = new List<string>() { "5","Contagious (Infected Book 2)", "Scott Sigler", "Fiction", "in", "" };
            library[5] = new List<string>() { "6","Confessions of an Economic Hit Man", "John Perkins", "Biography", "in", "" };
            library[6] = new List<string>() { "7","How to Steal a Dog: A Novel", "Barbara O'Connor", "Fiction", "in", "" };
            library[7] = new List<string>() { "8","The Very Hungry Caterpillar", "Eric Carle", "Kids", "in", "" };
            library[8] = new List<string>() { "9","Goodnight Moon", "Margaret Wise Brown", "Kids", "in", "" };
            library[9] = new List<string>() { "10","Brown Bear, Brown Bear, What Do You See?", "Bill Marin Jr.", "Kids", "in", "" };
            library[10] = new List<string>() { "11","Full Disclosure", "Stormy Daniels", "Biography", "in", "" };
            library[11] = new List<string>() { "12","The Adventures of Captain Underpants", "Dav Pilkey", "Action/Adventure", "in", "" };
        }

        static void print(Dictionary<int, List<string>> library, string sorter)
        {
            Console.WriteLine($"\n*{sorter} sorted");     //let user know what category is being sorted at all times
            Console.WriteLine($"{"ID",-2}){"Title:",-40}|{"Auther:",-21}|{"Genre:",-16}|{"Status:",-8}|{"Return:",-6}|");              //
            Console.WriteLine("=".PadLeft(100, '='));                                                                                  //print categories
            foreach (var v in library)                                                                                                 //
                Console.Write($"{v.Value[0],2}){v.Value[1],-40}|{v.Value[2],-21}|{v.Value[3],-16}|{v.Value[4],-8}|{v.Value[5],-7}|\n");//
            Console.WriteLine();                                                                                                       //
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
                return library;    //Exits SORT() function when satisfied with category sorted
            else if (s > 6 || s < 0)
            {
                Console.WriteLine("BAD NUMBER");               //user entered wrong
                library = SORT(library, labels, ref sorter);   //
            }
            sorter = labels[s];
            if (s == 0)//list index 0 is the 'ID' category and needs to be treated as an integar for sorting purposes
                library = SORT(library.OrderBy(a => int.Parse(a.Value[0])).ToDictionary(a => a.Key, a => a.Value), labels, ref sorter); //inorder to keep sorted order i decided to use recursion
            else if (s >= 1 && s <= 5)//just sort the dictionary by the other list categories
                library = SORT(library.OrderBy(a => a.Value[s]).ToDictionary(a => a.Key, a => a.Value), labels, ref sorter);//inorder to keep sorted order i decided to use recursion
            return library; //want the sort option to remain as is.
        }

        static Dictionary<int, List<string>> Search(Dictionary<int, List<string>> library, List<string> labels, string sorter)
        {//Search uses 'tempD' as a filtered display but sends 'library' through all functions to be filtered
            Dictionary<int, List<string>> tempD = new Dictionary<int, List<string>>();
            int s = options(labels, "Search For");
            if (s == 6)
                return library;             //exit search
            else if (s >= 0 && s <= 5)
            {
                Console.Write($"Search {labels[s]} ");      //what category should be searched?
                string word = Console.ReadLine();           //
                if (s == 0)//Filter into 'tempD' with the user input 'word' and treat as a integer because 'ID' is a number
                    tempD = library.Where(a => a.Key == int.Parse(word)).ToDictionary(a =>a.Key, a => a.Value);
                else//Filter 'tempD' with user input 'word' in other categories
                    tempD = library.Where(a => a.Value[s].Contains(word)).ToDictionary(a => a.Key, a => a.Value);
                print(tempD.ToDictionary(a => a.Key, b => b.Value), sorter); //pump new dictionary into print function so user can see it.
                if (tempD.Count() == 1 && tempD[tempD.Keys.First()][4] == "in")//rent book
                    RENTER(ref library, labels, sorter, tempD.Keys.First(), "rent");
                else if (tempD.Count() == 1 && tempD[tempD.Keys.First()][4] == "out")//return book
                    RENTER(ref library, labels, sorter, tempD.Keys.First(), "return");
                Search(library, labels, sorter);                         //go back to search function.
            }
            else
            {
                Console.WriteLine("BAD NUMBER");
                SORT(library, labels, ref sorter);
            }
            return library;
        }
        static void RENTER(ref Dictionary<int,List<string>> library, List<string> labels, string sorter, int key, string rentReturn)
        {//This function assumes the user wants to rent/return but and asks. User can refuse by pressing anything else.
            Console.Write($"Enter 'y' to {rentReturn}: ");
            if (Console.ReadLine() == "y")
            {
                DateTime d = DateTime.Now.AddDays(14);
                library[key][4] = (rentReturn == "rent") ? "out" : "in";                                //if rentReturn == "rent" set to out
                library[key][5] = (rentReturn == "rent") ? d.ToString("MM/dd").ToString() : "";         //if rentReturn == "rent" set return date
                print(library.Where(a => a.Key == key).ToDictionary(a => a.Key, b => b.Value), sorter);
            }
        }

        static Dictionary<int, List<string>> ADDER(Dictionary<int,List<string>> library, List<string> labels)
        {//add a book
            library.Add(library.Keys.Last()+1, new List<string>());
            string temp = "";
            library[library.Keys.Last()].Add((library.Keys.Last()+1).ToString());//incrementing the key by 1 and added a new book selection
            for (int i = 1; i < labels.Count-2; i++)
            {
                ask1:
                Console.Write($"Gimme a {labels[i]} ");
                temp = Console.ReadLine();
                if (string.IsNullOrEmpty(temp))
                    goto ask1;
                library[library.Keys.Last()].Add(temp);
            }
            library[library.Keys.Last()].Add("in");
            library[library.Keys.Last()].Add("");
            print(library, "testing");
            return library;
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
                    Console.WriteLine("~MAIN MENU~\n1) Display\n2) Checkout\n3) New Book\n4) Exit\n----------\nChoose an option: ");
                    c = Console.ReadLine();
                    if (c == "4")
                        break;
                    else if (c == "3")
                        library = ADDER(library, labels);
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
    }
}