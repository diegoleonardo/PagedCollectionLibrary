using PagedCollection.Library.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace PagedCollection.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PrintExampleWithArray();
        }

        private static void PrintExampleWithStructType()
        {
            var listOfObjects = new List<int>();
            for (int index = 1; index <= 100; index++)
            {
                listOfObjects.Add(index);
            }
            var pagedList = listOfObjects.ToPagedCollection(1, 10);
            Console.WriteLine("");
            Console.WriteLine("Page:" + pagedList.CurrentPage);
            foreach (var item in pagedList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Total Records In Page:" + pagedList.TotalRecordsInPage);

            pagedList = listOfObjects.ToPagedCollection(2, 10);
            Console.WriteLine("");
            Console.WriteLine("Page:" + pagedList.CurrentPage);
            foreach (var item in pagedList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Total Records In Page:" + pagedList.TotalRecordsInPage);

            pagedList = listOfObjects.ToPagedCollection(3, 10);
            Console.WriteLine("");
            Console.WriteLine("Page:" + pagedList.CurrentPage);
            foreach (var item in pagedList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Total Records In Page:" + pagedList.TotalRecordsInPage);
            Console.WriteLine("");
            Console.WriteLine("Total Records:" + pagedList.TotalRecords);
            Console.ReadKey();
        }
        private static void PrintExampleWithArray()
        {
            int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var convertedArray = array.ToPagedCollection(1, 5);
            Console.WriteLine("Page:" + convertedArray.CurrentPage);
            foreach (var item in convertedArray)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Total Records In Page:" + convertedArray.TotalRecordsInPage);
            Console.WriteLine("");
            convertedArray = array.ToPagedCollection(2, 5);
            foreach (var item in convertedArray)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Total Records In Page:" + convertedArray.TotalRecordsInPage);
            Console.WriteLine("");
            convertedArray = array.ToPagedCollection(3, 5);
            foreach (var item in convertedArray)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Total Records In Page:" + convertedArray.TotalRecordsInPage);
            Console.WriteLine("");
            Console.WriteLine("Total Records:" + convertedArray.TotalRecords);
            Console.ReadKey();
        }
        private static void PrintExampleWithAbstractType()
        {
            var listOfObjects = new List<Person>();
            for (int index = 1; index <= 102; index++)
            {
                listOfObjects.Add(new Person() { Id = index, Name = "Diego" + index });
            }
            var pagedList = listOfObjects.ToPagedCollection(1);
            Console.WriteLine("");
            Console.WriteLine("Page:" + pagedList.CurrentPage);
            foreach (var item in pagedList)
            {
                Console.WriteLine("id:" + item.Id + ", Name:" + item.Name);
            }
            Console.WriteLine("Total Records In Page:" + pagedList.TotalRecordsInPage);

            pagedList = listOfObjects.ToPagedCollection(2);
            Console.WriteLine("");
            Console.WriteLine("Page:" + pagedList.CurrentPage);
            foreach (var item in pagedList)
            {
                Console.WriteLine("id:" + item.Id + ", Name:" + item.Name);
            }
            Console.WriteLine("Total Records In Page:" + pagedList.TotalRecordsInPage);

            pagedList = listOfObjects.ToPagedCollection(3);
            Console.WriteLine("");
            Console.WriteLine("Page:" + pagedList.CurrentPage);
            foreach (var item in pagedList)
            {
                Console.WriteLine("id:" + item.Id + ", Name:" + item.Name);
            }
            Console.WriteLine("Total Records In Page:" + pagedList.TotalRecordsInPage);
            Console.WriteLine("");
            Console.WriteLine("Total Records:" + pagedList.TotalRecords);
            Console.ReadKey();
        }
    }
}
