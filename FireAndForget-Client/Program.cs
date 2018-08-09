using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace FireAndForget_Client
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://mqjoseph.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=5o/AWvaCUclEUShZk6NKocdDXT97/+5t3EP5arVICwA=";
        const string QueueName = "queue1";
        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            try
            {
                //connect to queue
                queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

                for (int i = 0; i < 100; i++)
                {
                    //prepare message to send
                    var messageBody = "Mensaje Prueba Joseph #" + i.ToString();
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    //send message to queue
                    queueClient.SendAsync(message).Wait();

                    Console.WriteLine(messageBody);

                    System.Threading.Thread.Sleep(1500);
                }              

                //close connection
                queueClient.CloseAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);                
            }

            Console.ReadKey();
        }        
    }
}
