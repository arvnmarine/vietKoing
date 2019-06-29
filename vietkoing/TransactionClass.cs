using System;
using System.Collections.Generic;
using System.Text;

namespace vietkoing
{
    class Transaction
    {
        private string transferer, transferee;
        private int amount;
        public Transaction(string _transferer, string _transferee, int _amount)
        {
            transferer = _transferer;
            transferee = _transferee;
            amount = _amount;
        }

        public string GetTransferer()
        {
            return transferer;
        }

        public string GetTransferee()
        {
            return transferee;
        }

        public int GetAmount()
        {
            return amount;
        }
    }
}
