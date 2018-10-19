using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        static Dictionary<int, List<string>> SORT(Dictionary<int, List<string>> library, List<string> labels, ref string sorter)
        {
            print(library, sorter);
            for (int i = 0; i < labels.Count; i++)              //
                Console.WriteLine($"{i+1}) {labels[i]}");       //Print all labels for user to choose from
            Console.Write($"6) Exit\n----------\nSort By: ");   //
            Console.WriteLine();                                //
            int s = int.Parse(Console.ReadLine())-1;
            if (s == 5)
                return library;                                 //Exits SORT() function when satisfied
            else if (s >= 0 && s <= 4)
            {
                sorter = labels[s];               //set sorter equal to the new label to sort by then pass lambda sort as dictionary back into sort
                SORT(library.OrderBy(a => a.Value[s]).ToDictionary(a => a.Key, a => a.Value), labels, ref sorter);
            }
            else
            {                                                   //
                Console.WriteLine("BAD NUMBER");                //user entered wrong
                SORT(library, labels, ref sorter);              //
            }
            return library;
        }

        static void print(Dictionary<int, List<string>> library, string sorter)
        {
            int i = 1;
            Console.WriteLine($"*{sorter} sorted");     //let user know what category is being sorted
            Console.WriteLine($"{"Title:", -44}|{"Auther:", -21}|{"Genre:", -16}|{"Status:", -8}|{"Date:", -6}|");               //
            Console.WriteLine("=".PadLeft(96, '='));                                                                             //print categories
            foreach (var v in library)                                                                                           //
                Console.Write($"{i++,2}) {v.Value[0],-40}|{v.Value[1],-21}|{v.Value[2],-16}|{v.Value[3],-8}|{v.Value[4],-6}|\n");//
            Console.WriteLine();
        }
        static void Search(Dictionary<int,List<string>> library, List<string> labels, string sorter)
        {
            for (int i = 0; i < labels.Count; i++)                                              //
                Console.WriteLine($"{i + 1}) {labels[i]}");                                     //user choosed a label
            Console.Write($"6) Exit\n----------\nSearch For: ");                                //
            int s = int.Parse(Console.ReadLine()) - 1;
            if (s == 5)
                return;
            else if (s >= 0 && s <= 4)
            {
                Console.Write($"Search {labels[s]} ");                                          //Prompt user for
                string word = Console.ReadLine();                                               //word to search in label[s].
                var k = library.OrderBy(a => a.Value[s]).Where(a => a.Value[s].Contains(word)); //sort label[s] then sub list collections containing that word[word] in label.
                print(k.ToDictionary(a=>a.Key,b =>b.Value), sorter);                            //pump new dictionary into print function so user can see it.
                Search(library, labels, sorter);                                                //go back to search function.
            }
            else
            {
                Console.WriteLine("BAD NUMBER");
                SORT(library, labels, ref sorter);
            }
        }

        static void Main(string[] args)
        {
            Dictionary<int, List<string>> library = new Dictionary<int, List<string>>();
            List<string> labels = new List<string>() { "Title:", "Author:", "Genre:", "Status:", "Date:" };
            DateTime d = new DateTime();
            loader(library);
            string sorter = "Name";
            try
            {
                //library = SORT(library, labels, ref sorter);                                                  //Done
                //print(library, sorter);                                                                       //Done
                Search(library, labels, sorter);                                                              //Done
                //Console.WriteLine(DateTime.Today.Day + "/" + DateTime.Today.Month);
                print(library, sorter);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

