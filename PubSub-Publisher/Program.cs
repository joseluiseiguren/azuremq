using System;
using System.Text;
using System.Threading;
using Microsoft.Azure.ServiceBus;

namespace PubSub_Publisher
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://topi-test-1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Ctk5KTyaGg36Bk7vEJWqDTC+RGWgd31LrCfnz2YEBXA=";
        const string TopicName = "topic-1";
        static ITopicClient topicClient;

        static void Main(string[] args)
        {
            try
            {
                //connect to topic
                topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

                for (int i = 0; i < 100; i++)
                {
                    //prepare message to send
                    var messageBody = "Mensaje Prueba Joseph #" + i.ToString();
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    //send message to topic
                    topicClient.SendAsync(message).Wait();

                    Console.WriteLine(messageBody);

                    Thread.Sleep(3000);
                }

                //close connection
                topicClient.CloseAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }
    }
}
