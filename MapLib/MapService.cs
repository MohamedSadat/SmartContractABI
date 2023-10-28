using MapLib.Data;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace MapLib
{
    public class MapService:IDisposable
    {
        public MemoryMappedFile MapFile { get; set; }
        public Mutex Mtx { get; set; }
        public bool CreateFileFromString(string filename, string mutexname, string obj)
        {
            try
            {
                var stringperson = obj;
                //Create a memory mapped file
                MapFile = MemoryMappedFile.CreateNew(filename, obj.Length,MemoryMappedFileAccess.ReadWriteExecute);

                //Create a mutex so that we can synchronize access to our memory mapped file
                bool mutexCreated;
                Mtx = new Mutex(true, mutexname, out mutexCreated);
                MemoryMappedViewStream stream = MapFile.CreateViewStream();

                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(stringperson);

                //Release the mutex
                Mtx.ReleaseMutex();
                File.WriteAllText(filename, stringperson);
               Config.FileDic.TryAdd(filename, new Data.MapModel {FileName=filename,MutexName=mutexname,Length=obj.Length ,
               
               });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public string ReadFileAsString(string filename)
        {
            var mutexname = new MapModel { FileName=filename};
          var check=  Config.FileDic.TryGetValue(filename, out mutexname);
            if(check==false)
            {
                return "";
            }
            Console.WriteLine($"Open {mutexname.FileName} , {mutexname.MutexName} Len {mutexname.Length} size {mutexname.Size}");
            string dperson = "";
            using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(filename))
            {
                bool mutexCreated;
                Mtx = Mutex.OpenExisting(mutexname.MutexName);
                Mtx.WaitOne();
                using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                {
                    BinaryReader reader = new BinaryReader(stream);
                    dperson=reader.ReadString();

                }
                Mtx.ReleaseMutex();
            }
            return dperson;
        }

 

        public void Dispose()
        {
            Console.WriteLine("Dispose will be called");
            MapFile.Dispose();
            Mtx.Dispose();
            Console.WriteLine("Dispose  called");

        }
    }
}