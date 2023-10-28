using System.Text;

namespace BinaryProj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var x=new Services.ContractService();
        
            var contract = new Data.ContractModel {
                Name="sadat",
                FromAccount="sadat",
                ToAccount="moham",
                Balance=100,
            };
              x.CreateContract(contract);

            var rcontract = x.ReadContract("sadat");
            Console.WriteLine("From: {0} To: {1} Amout: {2} date: {3}", rcontract.FromAccount, rcontract.ToAccount, rcontract.Balance,rcontract.TransDate);

            Console.ReadKey();

            return;
        if(File.Exists("file.pdf"))
            {
                Console.WriteLine("File exists");
                using (var stream = File.Open("file.pdf", FileMode.Open))
                {

                    using (var reader = new BinaryReader(stream, Encoding.UTF8))
                    {
                        var str = reader.ReadBytes((int)stream.Length);
                    
                        using (var writer = new BinaryWriter(File.Open("file2.pdf", FileMode.Create), Encoding.UTF8))
                        {
                            writer.Write(str);
                        }

                    }
                }
            }
        else
            {
                Console.WriteLine("File does not exist");
            }
        using(var stream=File.Create("newfile.dat"))
            {
                using(var writer=new BinaryWriter(stream,Encoding.UTF8))
                {
                    writer.Write('a');
                    Console.WriteLine("Size is {0}",stream.Length);
                    writer.Write('\0');
                    writer.Write('b');
                    writer.Write('\0');

                    writer.Write('b');

                    Console.WriteLine("Size is {0}", stream.Length);
                }
       
            }
        //using(var stream=File.Open("newfile.dat",FileMode.Open))
        //    {
        //        using (var reader = new BinaryReader(stream, Encoding.UTF8))
        //        {
        //            stream.Position = 0;
        //          //  stream.Seek(0, SeekOrigin.Current);
        //            byte[] buffer = new byte[1];
        //            reader.Read(buffer, 0, 1);
        //            foreach(var item in buffer)
        //            {
        //                Console.WriteLine(item);
        //            }
        //            int result = BitConverter.ToInt32(buffer, 0);
        //            Console.WriteLine(result);
              
        //        }
        //    }
     
       Console.ReadKey();
        }

    }
}