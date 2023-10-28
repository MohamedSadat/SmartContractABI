using System.Text;

namespace FileProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
          var file=  File.Create("test.txt");
            file.Close();
            
            File.AppendAllLines("test.txt", new string[] { "Hello, World!" });
           try
            {
                File.Copy("test.txt", "test2.txt");

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
              //  throw;
            }
            Console.WriteLine("Hello, World!");
           
            using(var file1 = File.Open("test1.txt", FileMode.OpenOrCreate))
            {
               //using(var writer = new StreamWriter(file1))
               // {
                  
               //     writer.WriteLine("Hello, World!");
               // }
                char[] buffer ;
                using(var reader = new StreamReader(file1))
                {
                    buffer = new char[reader.BaseStream.Length];
                    reader.Read(buffer,0,(int) reader.BaseStream.Length);
                    Console.WriteLine("buffer " +new string(buffer));
                    //Console.WriteLine(reader.ReadToEnd());

                }
            }
            BinRead();
            Console.ReadLine();
            BinWrite();

        }
      static  void BinWrite()
        {
            using (var stream = File.Open("file1.dat", FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    writer.Write("Hello, World!");
                  
                }
            }
        }
        static void BinRead(string fileName="test.txt")
        {
            float aspectRatio;
            string tempDirectory;
            int autoSaveTime;
            bool showStatusBar;

            if (File.Exists(fileName))
            {
                using (var stream = File.Open(fileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        //Read all bytes from the file
                        var b=reader.ReadBytes((int)stream.Length);
                        foreach(var VARIABLE in b)
                        {
                            Console.WriteLine(VARIABLE);
                        }
                        //convert bytes to string
                        var encode =Encoding.UTF8.GetString(b);
                        Console.WriteLine(encode);
                        return;

                        aspectRatio = reader.ReadSingle();
                        tempDirectory = reader.ReadString();
                        autoSaveTime = reader.ReadInt32();
                        showStatusBar = reader.ReadBoolean();
                    }
                }

                Console.WriteLine("Aspect ratio set to: " + aspectRatio);
                Console.WriteLine("Temp directory is: " + tempDirectory);
                Console.WriteLine("Auto save time set to: " + autoSaveTime);
                Console.WriteLine("Show status bar: " + showStatusBar);
            }
        }
    }
}