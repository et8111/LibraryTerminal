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
        {
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

        static Dictionary<int, List<string>> SORT(Dictionary<int, List<string>> library, int s, ref string sorter)
        {
            sorter = (s == 0) ? "Name" : "Author";
            return library.OrderBy(a => a.Value[s]).ToDictionary(a => a.Key, a => a.Value);
        }

        static void print(Dictionary<int, List<string>> library, string sorter)
        {
            Console.WriteLine($"*{sorter} sorted");
            Console.WriteLine($"{"Title:", -40}|{"Auther:", -21}|{"Genre:", -16}|{"Status:", -8}|{"Date:", -6}|");
            Console.WriteLine("=".PadLeft(96, '='));
            foreach (var v in library)
                Console.Write($"{v.Value[0],-40}|{v.Value[1],-21}|{v.Value[2],-16}|{v.Value[3],-8}|{v.Value[4],-6}|\n");
            Console.WriteLine();
        }
        static void Search(List<string> v)
        {
            Console.WriteLine($"{"Title:",-40}|{"Auther:",-21}|{"Genre:",-16}|{"Status:",-8}|{"Date:",-6}|");
            Console.WriteLine("=".PadLeft(96, '='));
            Console.Write($"{v[0],-40}|{v[1],-21}|{v[2],-16}|{v[3],-8}|{v[4],-6}|\n");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Dictionary<int, List<string>> library = new Dictionary<int, List<string>>();
            string sorter = "Name";
            loader(library);
            library = SORT(library, 0,ref sorter);

            print(library, sorter);
            Search(library[0]);

            var k = library.OrderBy(a => a.Value[0]).Where(a => a.Value[0].Contains("B"));
            foreach(var v in k)
                Console.WriteLine(v.Value[0]);
        }
    }
}

