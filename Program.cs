using KafkaClient;
using System;

namespace KafkaChat
{
    class Program
    {
        static bool ProccessMessage(string message)
        {
            Console.WriteLine($"Received: {message}");
            return true;
        }

        static async void Loop(KafkaClientAPI kafkaClient)
        {
            Console.WriteLine("Listening to messages...");
            await kafkaClient.SubscribeAsync<string>("chat", (message) => ProccessMessage(message));
            while (true)
            {
                Console.WriteLine("Type your message!");
                var message = Console.ReadLine();
                await kafkaClient.PublishAsync("chat", message);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Loop(new KafkaClientAPI());
        }
    }
}
