using MapLib;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text.Json;
using static MappedMemory.Program;

namespace MappedMemory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> list = new List<Person>();
            for(int i = 0; i < 10000; i++)
            {
            var p=new Person();
                p.Name = $"Sadat {i}";
list.Add(p);
            }
            var x=new MapService();
            x.CreateFileFromString("items","itemmutex", JsonSerializer.Serialize(list));
            var result= JsonSerializer.Deserialize < List < Person >>(x.ReadFileAsString("items"));

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Process A says: {0}", result?[i].Name);
               
            }
            Console.ReadLine();
            return;



           var stringperson= JsonSerializer.Serialize(list);
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("testmap", 10000))
            {
                bool mutexCreated;
                Mutex mutex = new Mutex(true, "testmapmutex", out mutexCreated);
                using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                {
                    BinaryWriter writer = new BinaryWriter(stream);
                   writer.Write(stringperson);
                    writer.Write("Hello");

                }
                mutex.ReleaseMutex();

                Console.WriteLine("Start Process B and press ENTER to continue.");
                Console.ReadLine();

                mutex.WaitOne();
                using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                {
                    BinaryReader reader = new BinaryReader(stream);
                 var dperson= JsonSerializer.Deserialize<List<Person>>(reader.ReadString());
                    var hello = reader.ReadString();
                    for (int i = 0; i < 10; i++)
                    {
                           Console.WriteLine("Process A says: {0}", dperson?[i].Name);
                        Console.WriteLine(hello);
                    }
                }
                mutex.ReleaseMutex();
            }
            Console.ReadLine();
        }
   
        public class Person
        {
            public string Name { get; set; } = "Sadat";
            public int Age { get; set; }= 25;

        }
    }
}