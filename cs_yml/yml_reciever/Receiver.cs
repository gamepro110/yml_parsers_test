using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using yml_lib;

await yml_reciever.Receiver.Run();

namespace yml_reciever
{
    internal class Receiver
    {
        public static async Task Run()
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            HttpListener listener = new HttpListener();
            
            listener.Prefixes.Add("http://localhost:60004/");
            listener.Start();
            Console.WriteLine("Listening...");

            bool run = true;

            while (listener.IsListening && run)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;

                string str;

                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    str = $"{reader.ReadToEnd()}\n";
                }

                ISerializationHandler serializer = new YmlHandler();
                Person p = serializer.Deserialize<Person>(str);

                Console.WriteLine(
                    string.Format(
                        "n: {0} id: {1} sal: {2} t: {3}",
                        p.name,
                        p.id,
                        p.salary,
                        string.Join(
                            ", ",
                            p.tasks
                        )
                    )
                );

                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                string responseString = "...";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                await output.WriteAsync(buffer, 0, buffer.Length);
                // You must close the output stream.

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            {
                                run = false;
                                output.Close();
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }

            listener.Stop();
        }
    }
}
