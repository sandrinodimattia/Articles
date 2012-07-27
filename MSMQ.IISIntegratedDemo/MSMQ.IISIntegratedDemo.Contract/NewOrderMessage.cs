using System;

namespace MSMQ.IISIntegratedDemo.Contract
{
    [Serializable]
    public class NewOrderMessage
    {
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }
    }
}
