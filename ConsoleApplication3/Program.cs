using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument xDoc = XDocument.Load("http://users.nik.uni-obuda.hu/hp/people.xml");

            var people =
                xDoc.Element("people").Elements("person").Select(
                    element =>
                        new Person()
                        {
                            Name = element.Element("name").Value,
                            Dept = element.Element("dept").Value,
                            Mail = element.Element("email").Value,
                            Phone = element.Element("phone").Value,
                            Rank = element.Element("rank").Value,
                            Room = element.Element("room").Value
                        });

            // Number of AII members
            var q1 = people.Where(person => person.Dept == "Alkalmazott Informatikai Intézet").Count();
            Console.WriteLine(q1);
            Console.WriteLine("---------------------------------------");

            //Jelenítsük meg azokat, akiknek a harmadik emeleten van irodája!
            var q2 = people.Where(person => person.Room.StartsWith("BA.3."));
            Print(q2);
            Console.WriteLine("---------------------------------------");

            // Who have the shortest names?
            var q3 = from person in people
                     let minNameLength = people.Min(p => p.Name.Length)
                     let maxNameLength = people.Max(p => p.Name.Length)
                     where person.Name.Length == minNameLength || person.Name.Length == maxNameLength
                     select person;
            Print(q3);
            Console.WriteLine("---------------------------------------");

            //Határozzuk meg intézetenként a dolgozók darabszámát!
            var q4 = from person in people
                     group person by person.Dept into g
                     select new { Department = g.Key, People = g.Count() };
            Print(q4);
            Console.WriteLine("---------------------------------------");

            //Határozzuk meg a legnagyobb intézetet!
            var q5 = q4.Where(g => g.People == q4.Max(f => f.People));
            Print(q5);
            Console.WriteLine("---------------------------------------");

            //Listázzuk a legnagyobb intézet dolgozóit!
            var q6 = from person in people
                     where q5.Select(g => g.Department).Contains(person.Dept)
                     select person;
            Print(q6);
            Console.WriteLine("---------------------------------------");

            //Listázzuk a harmadik legnagyobb intézet dolgozóit szobaszám szerint csökkenő sorrendben!
            var third = q4.OrderBy(g => g.People).Reverse().ElementAt(3);
            var q7 = from person in people
                     where person.Dept == third.Department
                     orderby person.Room
                     select person;
            Print(q7);
            Console.WriteLine("---------------------------------------");

            Console.ReadLine();
        }

        static void Print<T>(IEnumerable<T> enumerable)
        {
            foreach (var t in enumerable)
            {
                Console.WriteLine(t.ToString());
            }
        }
    }
}
