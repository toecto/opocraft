using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace test_database
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("It works!");
            Database db = new Database(@"Data Source=|DataDirectory|\..\..\MyDatabase.sdf");
            DataTable results = db.Query("SELECT * FROM books");
            foreach (DataRow row in results.Rows)
            {
                Console.WriteLine("> " + row["id"] + " " + row["name"] + " " + row["price"]);
            }

            Console.ReadLine();
        }

    }
}
