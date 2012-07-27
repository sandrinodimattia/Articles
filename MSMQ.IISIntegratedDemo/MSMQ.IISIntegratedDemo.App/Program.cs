using System;
using System.Messaging;
using System.Transactions;

using MSMQ.IISIntegratedDemo.Contract;

namespace MSMQ.IISIntegratedDemo.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\r\nPress ENTER to send a message to the queue.");
            Console.ReadLine();

            // Init queue.
            var queue = new MessageQueue(@"FormatName:Direct=OS:.\private$\IISIntegratedDemo/OrderService.svc");

            // Create the message.
            var message = new Message();
            message.Body = new NewOrderMessage()
            {
               CustomerId = 2029,
               Date = DateTime.Now,
               Price = 2918.59M,
               ProductId = 99843
            };

            // Send message in a transaction.
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                queue.Send(message, MessageQueueTransactionType.Automatic);
                scope.Complete();
            }

            Console.WriteLine("Press ENTER to close the application.");
            Console.ReadLine();
        }
    }
}
