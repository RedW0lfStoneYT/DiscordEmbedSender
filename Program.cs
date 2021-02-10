using RestSharp;
using System;

namespace DiscordEmbed
{
    class Program
    {
        static void Main(string[] args)
        {
            Program app = new Program();

            app.runMain();
        }

        public void runMain()
        {
            Console.WriteLine(request());
        }

        public String request()
        {
            Console.WriteLine("Channel ID");
            var channelID = Console.ReadLine();
            var client = new RestClient("https://discord.com/api/v6/channels/{CHANID}/messages".Replace("{CHANID}", channelID));
            var request = new RestRequest(Method.POST);

            // Get title
            Console.WriteLine("Title content");
            var title = Console.ReadLine();

            // Get body
            Console.WriteLine("Body content");
            var body = Console.ReadLine();

            // Get color
            Console.WriteLine("RGB Color (format'R,G,B')");
            var rawColor = Console.ReadLine();
            String[] colorVars = rawColor.Split(",");
            int r = Int32.Parse(colorVars[0]);
            int g = Int32.Parse(colorVars[1]);
            int b = Int32.Parse(colorVars[2]);

            // MAFS
            String color = (256 * 256 * r + 256 * g + b) + "";

            // Get token
            String token = System.IO.File.ReadAllText("token.txt");

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("authorization", token);
            request.AddParameter("undefined", "{\n\t\"embed\": {\n\"title\": \"{TITLE}\",\n\"description\": \"{BODY}\",\n\t\t\"color\": \"{COLOR}\"\n\n  }\n}".Replace("{TITLE}", title).Replace("{BODY}", body).Replace("{COLOR}", color), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response.Content;
        }
    }
}
