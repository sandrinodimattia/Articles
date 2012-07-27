using System;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;

using MSMQ.IISIntegratedDemo.Contract;

namespace MSMQ.IISIntegratedDemo
{
    [ServiceContract]
    [ServiceKnownType(typeof(NewOrderMessage))]
    public interface IOrderService
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void SubmitNewOrder(MsmqMessage<NewOrderMessage> newOrder);
    }
}
