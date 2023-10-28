using BinaryProj.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryProj.Services
{
    public class ContractService 
    {
        public event EventHandler<EventArgs> DepositEvent;
        public event EventHandler<EventArgs> WithdrawEvent;

        protected virtual void OnDeposit()
        {
            if (DepositEvent != null)
            {
                DepositEvent(this, new EventArgs());
            }
        }
        protected virtual void OnWithdraw()
        {
            if (WithdrawEvent != null)
            {
                WithdrawEvent(this, new EventArgs());
            }
        }
        public ContractService()
        {

        }
        public ContractService(MessageModel msg,ContractModel contract)
        {
            contract.Owner = msg.Sender;
            contract.Balance = msg.Amount;
        }
        public void CreateContract(ContractModel contract)
        {
            using (var stream = File.Create($"{contract.Name}.dat"))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8))
                {
                    writer.Write(contract.FromAccount);
                    writer.Write(contract.ToAccount);
                    writer.Write(contract.Balance);
                    writer.Write(contract.TransDate.ToString());

                    Console.WriteLine("Size is {0}", stream.Length);
                }

            }
        }
        public ContractModel ReadContract(string name)
        {
            var contract = new ContractModel();
            using (var stream = File.Open($"{name}.dat", FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8))
                {
                    contract.FromAccount = reader.ReadString();
                    contract.ToAccount = reader.ReadString();
                    contract.Balance = reader.ReadUInt32();
                    contract.TransDate = Convert.ToDateTime(reader.ReadString());

                }
            }
            return contract;
        }
        public void Deposit(ContractModel contract,MessageModel msg)
        {
            if(msg.Amount>0)
            {

            }
           // var contract = ReadContract(name);
         //   contract.Balance += amount;
        //    CreateContract(contract);
            OnDeposit();
        }
        public void Withdraw(ContractModel contract, MessageModel msg, uint amount)
        {
            if (msg.Sender==contract.Owner)
            {
                if(contract.Balance>=msg.Amount)
                {
                    contract.Balance -= msg.Amount;
                }
            }
            //  var contract = ReadContract(name);
            //   contract.Balance -= amount;
            //  CreateContract(contract);
            OnWithdraw();
        }
    }
}
