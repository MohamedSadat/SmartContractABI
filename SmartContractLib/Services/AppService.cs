
using SmartContractLib.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartContractLib.Services
{
    public class AppService
    {
        public void CreateDB()
        {
            var f=new FileInfo($"db.dat");
            if(f.Exists==true)
            {
               // File.Delete($"db.dat");
            }
            else
            {
              var a=  f.Create();
               a.Close();
            }
            //using (var stream = File.Create($"db.dat"))
            //{
             

            //}
        }
        public void UpdateMessage(MessageModel msg)
        {
            var msgService = new MessageService();
            msgService.HashMessage(msg);
            using (var stream = File.Open($"db.dat", FileMode.Open))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    //Seek the cursor to amount location
                    writer.Seek(12, SeekOrigin.Begin);
                    writer.Write(msg.Amount);
          
                }
            }
        }
        public void AddMessage(MessageModel msg)
        {
            var msgService = new MessageService();
            msgService.HashMessage(msg);
            using (var stream = File.Open($"db.dat", FileMode.Append))
            {
                //Append will add to the end of the file
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                 //   writer.Seek(0, SeekOrigin.End);
                    writer.Write(msg.Sender);
                    writer.Write(msg.Reseiver);
                    writer.Write(msg.Amount);
                    writer.Write(msg.MessageHash);

                }
            }
        }
        public void PublishContract(ContractModel contract)
        {
            if(GetContract(contract.Name)==true)
            {
                Console.WriteLine("Contract already exist");
                return;
            }
            using (var stream = File.Open($"contracts.dat", FileMode.Append))
            {
                //Append will add to the end of the file
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    //   writer.Seek(0, SeekOrigin.End);
                    writer.Write(contract.Name);
                    writer.Write(contract.Owner);
                    writer.Write(contract.Balance);
         
                }
            }
        }
        public bool GetContract(string name)
        {
            var contraact=new ContractModel();
            using (var stream = File.Open($"contracts.dat", FileMode.Open))
            {
                //Append will add to the end of the file
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    while (reader.PeekChar() > -1)
                    {
                        var contractname = reader.ReadString();
                        if(contractname==name)
                        {
                            contraact.Name= contractname;
                        //    contraact.Owner = reader.ReadString();
                            contraact.Balance = reader.ReadUInt32();
                            return true;
                        }
                        stream.Seek(10, SeekOrigin.Current);
                      
                    }
                    return false;
                }
            }
        }
        public List<MessageModel> GetMessages()
        {
            var messages = new List<MessageModel>();
            using (var stream = File.Open($"db.dat", FileMode.Open))
            {
                Console.WriteLine("Size is {0}", stream.Length);
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    while (reader.PeekChar() > -1)
                    {
                        var msg = new MessageModel();
                        msg.Sender = reader.ReadString();
                        msg.Reseiver = reader.ReadString();
                        msg.Amount = reader.ReadUInt32();
                        msg.MessageHash = reader.ReadString();
                        messages.Add(msg);
                    }
                }
            }
            return messages;

        }
        public MessageModel GetMessage(string hash)
        {
            var messages = new List<MessageModel>();
            using (var stream = File.Open($"db.dat", FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    while (reader.PeekChar() > -1)
                    {
                        var msg = new MessageModel();
                        msg.Sender = reader.ReadString();
                        msg.Reseiver = reader.ReadString();
                        msg.Amount = reader.ReadUInt32();
                        msg.MessageHash = reader.ReadString();
                        messages.Add(msg);
                    }
                }
            }
            return messages.FirstOrDefault(x => x.MessageHash == hash);

        }
        public uint GetBalance(string name)
        {
            var balance = 0u;
            var messages = new List<MessageModel>();
            using (var stream = File.Open($"db.dat", FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    while (reader.PeekChar() > -1)
                    {
                        var msg = new MessageModel();
                        msg.Sender = reader.ReadString();
                        msg.Reseiver = reader.ReadString();
                        msg.Amount = reader.ReadUInt32();
                        msg.MessageHash = reader.ReadString();
                        messages.Add(msg);
                    }
                }
            }
            var sent = messages.Where(x => x.Sender == name).Sum(x => x.Amount);
            var received = messages.Where(x => x.Reseiver == name).Sum(x => x.Amount);
            balance =Convert.ToUInt32(received - sent);
            return balance;
        }
        public uint GetBalanceEx(string name)
        {
            var balance = 0u;
            var messages = new List<MessageModel>();
            using (var stream = File.Open($"db.dat", FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    while (reader.PeekChar() > -1)
                    {
                        var msg = new MessageModel();
                        msg.Sender = reader.ReadString();
                        msg.Reseiver = reader.ReadString();
                        msg.Amount = reader.ReadUInt32();
                        stream.Seek(65, SeekOrigin.Current);
                      //  msg.MessageHash = reader.ReadString();
                        messages.Add(msg);
                    }
                }
            }
            var sent = messages.Where(x => x.Sender == name).Sum(x => x.Amount);
            var received = messages.Where(x => x.Reseiver == name).Sum(x => x.Amount);
            balance = Convert.ToUInt32(received - sent);
            return balance;
        }
        public int GetMsgCount()
        {
            var count = 0;
            using (var stream = File.Open($"db.dat", FileMode.Open))
            {
                var size = stream.Length/81;
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    while (reader.PeekChar() > -1)
                    {

                        stream.Seek(81, SeekOrigin.Current);
                        count++;
                    
                    }
                }
            }
            return count;
        }
    }
}
