using System.Text;
using SmartContractLib.Services;
using SmartContractLib.Data;
namespace BinaryProj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var x = new SmartContractLib.Services.ContractService();
            var xapp = new AppService();
            xapp.CreateDB();
            xapp.AddMessage(new MessageModel { Sender = "owner", Reseiver = "sadat", Amount = 1000000 });
            xapp.AddMessage(new MessageModel { Sender = "sadat", Reseiver = "moham", Amount = 100 });
            xapp.AddMessage(new MessageModel { Sender = "sadat", Reseiver = "moham", Amount = 200 });
            xapp.AddMessage(new MessageModel { Sender = "sadat", Reseiver = "Ahmed", Amount = 50000 });
            //Get message size in bytes
          
            var messages = xapp.GetMessages();
            //All messages
            foreach (var msg in messages)
            {
                Console.WriteLine("Sender: {0} Reseiver: {1} Amount: {2} Hash: {3}", msg.Sender, msg.Reseiver, msg.Amount, msg.MessageHash);
            }

            //Get balance
            var balance = xapp.GetBalanceEx("sadat");
            Console.WriteLine("Balance: {0}", balance);

            //Get Trans account
            Console.WriteLine($"Msg count:  {xapp.GetMsgCount()}");
            xapp.UpdateMessage(new MessageModel { Sender = "sadat", Reseiver = "moham", Amount = 290000 });
           
            //Create contract
            Console.WriteLine("Create contract");
            var contract = new ContractModel
            {
                Owner = "sadat",
                Name = "tezos",
                FromAccount = "sadat",
                ToAccount = "moham",
                Balance = 200,
            };
            x.CreateContract(contract);
            xapp.PublishContract(contract);

            //Read contract
            Console.WriteLine("Read contract");
            var rcontract = x.ReadContract("app1");
            Console.WriteLine("Owner: {0} Balance: {1} date: {2}", rcontract.Owner, rcontract.Balance, rcontract.TransDate);

            contract.Balance = 100000;
            x.CreateContract(contract);
            rcontract = x.ReadContract("app1");
            Console.WriteLine("Owner: {0} Balance: {1} date: {2}", rcontract.Owner, rcontract.Balance, rcontract.TransDate);

            Console.ReadKey();

        }
    }
}