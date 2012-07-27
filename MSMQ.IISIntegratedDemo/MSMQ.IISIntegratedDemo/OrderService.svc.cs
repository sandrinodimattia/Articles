using System;
using System.Diagnostics;
using System.ServiceModel.MsmqIntegration;
using System.ServiceModel;

namespace MSMQ.IISIntegratedDemo
{
    public class OrderService : IOrderService
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SubmitNewOrder(MsmqMessage<Contract.NewOrderMessage> newOrder)
        {
            Debugger.Break();
        }
    }
}
