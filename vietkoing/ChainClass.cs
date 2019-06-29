using System;
using System.Collections.Generic;
using System.Text;

namespace vietkoing
{
    class BlockChain
    {
        private List<Block> Chain;
        private List<Transaction> pendingTransaction;
        private int difficulty = 2, miningReward = 100;
        BlockChain()
        {
            Chain.Add(CreateFirstBlock());
        }

        BlockChain(int _difficulty, int _miningReward)
        {
            Chain.Add(CreateFirstBlock());
            difficulty = _difficulty;
            miningReward = _miningReward;
        }

        private Block CreateFirstBlock()
        {
            return (new Block(new DateTime(1997, 7, 3),new List<Transaction>(), "0"));
        }

        private Block GetLastestBlock()
        {
            return (Chain[Chain.Count - 1]);
        }


        private bool CompareBlocks(Block a, Block b)
        {
            if ((a.GetTimeStamp() != b.GetTimeStamp()) || (a.GetHash() != b.GetHash()))
                return false;
            if ((a.GetPreviousHash() != b.GetPreviousHash()) || (a.GetNonce() != b.GetNonce()))
                return false;
            var ta = a.GetTransactions();
            var tb = b.GetTransactions();
            if (ta.Count != tb.Count)
                return false;
            for (int i= 0; i < ta.Count; i++)
            {
                if (ta[i] != tb[i])
                    return false;
            }
            return true;
        }
        public bool ChainValidate() 
        {
            if (!CompareBlocks(Chain[0],CreateFirstBlock()))
                return false;
            for (int i = 1; i < Chain.Count; i++)
            {
                if (Chain[i].GetPreviousHash() != Chain[i - 1].GetHash())
                    return false;
                if (Chain[i].GetHash() != Chain[i].CalculateHash())
                    return false;
            }

            
            return true;
        }

        public void MiningPendingTransaction(string minerAddress)
        {
            var newBlock = new Block(DateTime.Now, pendingTransaction, GetLastestBlock().GetHash());
            newBlock.MineMe(difficulty);

            Chain.Add(newBlock);
            Console.WriteLine("Block is mined and added to the Chain");

            pendingTransaction = new List<Transaction>();
            pendingTransaction.Add(new Transaction(null, minerAddress, miningReward));
            Console.WriteLine("Added reward of miner to the new block to be mined.");
        }

        public int CalculateBalance(string wallet)
        {
            int balance = 0;
            for (int i=1; i < Chain.Count; i++)
            {
                var transactions = Chain[i].GetTransactions();
                for (int j = 0; j< transactions.Count; j++)
                {
                    var amount = transactions[j].GetAmount();
                    if (transactions[j].GetTransferer() == wallet)
                    {
                        balance -= amount;
                    }

                    if (transactions[j].GetTransferee() == wallet)
                    {
                        balance += amount;
                    }
                }
            }

            Console.WriteLine("Balance is " + balance.ToString());
            return balance;
        }

    }
}
