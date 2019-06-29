using System;
using System.Collections.Generic;
using System.Text;
using System.Data.HashFunction.Blake2;

namespace vietkoing
{
    class Block
    {
        private DateTime timeStamp;
        private List<Transaction> transactions;
        private string Hash;
        private string previousHash;
        private int nonce;

         public Block(DateTime _timeStamp, List<Transaction> _transactions, string _previousHash )
        {
            timeStamp = _timeStamp;
            transactions = _transactions;
            previousHash = _previousHash;
            Hash = CalculateHash();
            nonce = 0;
        }

        public string CalculateHash() //BLAKE2b to-be-implemented
        {
            //Blake2BFactory blakeHash = 
            return ("");
        }

        public DateTime GetTimeStamp()
        {
            return timeStamp;
        }

        public int GetNonce()
        {
            return nonce;
        }
        public string GetHash()
        {
            return Hash;
        }

        public string GetPreviousHash()
        {
            return previousHash;
        }

        public List<Transaction> GetTransactions()
        {
            return transactions;
        }

        public void MineMe(int difficulty) //changing nonce to achieve desireable 0-prefix for the hash
        {
            string difStr = "";
            for (int i = 0; i< difficulty; i++)
            {
                difStr += "0";
            }
            while (Hash.Substring(0,difficulty) != difStr)
            {
                nonce++;
                Hash = CalculateHash();
            }
        }
    }
}
