using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MappedMemory
{
    internal class MapHelper<T>:IDisposable
    {
        public MemoryMappedFile MapFile { get; set; }
        public Mutex Mtx { get; set; }
        public void CreateFileFromString(string filename, string mutexname, string obj)
        {
            var stringperson = obj;

            MemoryMappedFile mmf = MemoryMappedFile.CreateNew(filename, 10000);

            bool mutexCreated;
            Mutex mutex = new Mutex(true, mutexname, out mutexCreated);
            MemoryMappedViewStream stream = mmf.CreateViewStream();

            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(stringperson);


            mutex.ReleaseMutex();

          
        }
        public string ReadFileAsString(string filename, string mutexname)
        {

          string dperson="";
            using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(filename))
            {
                bool mutexCreated;
                Mtx = Mutex.OpenExisting(mutexname);
                Mtx.WaitOne();
                using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                {
                    BinaryReader reader = new BinaryReader(stream);
                    dperson = reader.ReadString();

                }
                Mtx.ReleaseMutex();
            }
            return dperson;
        }
        public void CreateFile(string filename, string mutexname, List<T> obj)
        {
            var stringperson = JsonSerializer.Serialize(obj);

            MapFile = MemoryMappedFile.CreateNew(filename, 10000);
            
                bool mutexCreated;
            Mtx = new Mutex(true, mutexname, out mutexCreated);
            MemoryMappedViewStream stream = MapFile.CreateViewStream();
                
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(stringperson);


            Mtx.ReleaseMutex();

            
        }
        public List<T> ReadFile(string filename, string mutexname)
        {

            List<T> dperson;
            using (MapFile = MemoryMappedFile.OpenExisting(filename))
            {
                bool mutexCreated;
                Mtx = Mutex.OpenExisting(mutexname);
                Mtx.WaitOne();
                using (MemoryMappedViewStream stream = MapFile.CreateViewStream())
                {
                    BinaryReader reader = new BinaryReader(stream);
                    dperson = JsonSerializer.Deserialize<List<T>>(reader.ReadString());

                }
                Mtx.ReleaseMutex();
            }
            return dperson;
        }

        public void Dispose()
        {
            MapFile.Dispose();
            Mtx.Dispose();
        }
    }
}
