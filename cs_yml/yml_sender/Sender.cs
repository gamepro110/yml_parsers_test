using System;
using System.Net;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

using yml_lib;

namespace yml_sender
{
    internal class Sender
    {
        static void Main()
        {
            ISerializationHandler ymlHandler = new YmlHandler();

            Person p = new Person()
            {
                id = 1,
                name = "nomnom",
                salary = 648.3,
                tasks = new List<int>()
                { 5, 65, 45, 6, 5684 },
            };

            using WebClient client = new WebClient();

            string yml = ymlHandler.Serialize(p);

            NameValueCollection data = new NameValueCollection
            {
                { "yml", yml }
            };


            byte[] responce;
            string str;

#if false
            responce = client.UploadValues("http://localhost:60004/", "POST", data);
            str = Encoding.UTF8.GetString(responce);
#else
            str = client.UploadString("http://localhost:60004/", ymlHandler.Serialize(p));
#endif
            Console.WriteLine(string.Format("send: [\n{0}\n]\n", yml));
            Console.WriteLine(str == string.Empty ? "success" : $"[responce]: {str}");
            Console.ReadLine();
        }
    }
}
