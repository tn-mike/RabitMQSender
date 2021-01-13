using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabitMQSender.Services
{
    public class SendService : ISendService
    {
        public string SendMessage()
        {
            //return CalTime();
            var factory = new ConnectionFactory() { HostName = "192.168.100.128" };
            using (var connection = factory.CreateConnection()) 
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "task_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                const Int32 BufferSize = 128;
                FileInfo[] listFile = new DirectoryInfo(@"D:\Develop\DataForTest\RabbitMQ\SMS").GetFiles("*.txt");
                foreach (var item in listFile)
                {
                    using (var fileStream = File.OpenRead(item.FullName))
                    {
                        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                        {
                            String line;
                            while ((line = streamReader.ReadLine()) != null) // remove faster
                            {
                                // rabbit proccess
                                var body = Encoding.UTF8.GetBytes(line);

                                channel.BasicPublish(exchange: "",
                                                     routingKey: "task_queue",
                                                     basicProperties: properties,
                                                     body: body);

                                Console.WriteLine(" [x] Sent {0}", line);
                            }
                        }
                    }
                }
            }

            Console.WriteLine(" Press [enter] to exit.");

            return "OK";
        }

        private string GetMessage(string[] args)
        {
            return "time." + args[0];
        }

        private string CalTime()
        {
            string path = @"D:\Develop\DataForTest\RabbitMQ\SMS\SMS005.txt";
            DirectoryInfo d = new DirectoryInfo(@"D:\Develop\DataForTest\RabbitMQ\SMS");
            FileInfo[] allFile = d.GetFiles("*.txt"); //Getting Text files
            //if (!File.Exists(path))
            //{
            //    // Create a file to write to.
            //    using (StreamWriter sw = File.CreateText(path))
            //    {
            //        for (int i = 40001; i <= 50000; i++)
            //        {
            //            sw.WriteLine(String.Format("คุณ ทดสอบ{0} ทดสอบ{0} 0801192770 ขอบคุณที่ใช้บริการ", i.ToString()));
            //        }
            //    }
            //}

            //return "OK";

            List<string> result = new List<string>();
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            const Int32 BufferSize = 128;
            foreach (var item in allFile)
            {
                using (var fileStream = File.OpenRead(item.FullName))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                    {
                        String line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                        }
                    }
                }
            }
            watch.Stop();
            result.Add($"Execution Time: {watch.ElapsedMilliseconds} ms");

            watch.Start();
            foreach (var item in allFile)
            {
                var lines = File.ReadLines(path);
                foreach (var line in lines)
                {

                }
            }
            watch.Stop();
            result.Add($"Execution Time: {watch.ElapsedMilliseconds} ms");

            watch.Start();
            foreach (var item in allFile)
            {
                var lines1 = File.ReadAllLines(path);
                for (var i = 0; i < lines1.Length; i += 1)
                {
                    var line = lines1[i];
                    // Process line
                }
            }
            watch.Stop();
            result.Add($"Execution Time: {watch.ElapsedMilliseconds} ms");

            return ((result.ToArray().Length > 0) ? string.Join(" ", result.ToArray()) : "Hello World!");
        }
    }
}
