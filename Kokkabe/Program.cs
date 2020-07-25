using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Whois;

namespace Kokkabe
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = File.ReadAllLines("top-1m.csv");
            var whois = new WhoisLookup();

            Parallel.ForEach(list, item =>
            {
                try
                {
                    var response = whois.Lookup(item.Split(',')[1]).ParsedResponse;
                    var text = $"{item},{response.AdminContact.Address.Reverse().ToArray()[0]},{response.AdminContact.Organization}";
                    Console.WriteLine(text);
                    File.AppendAllText("whois.csv", text + Environment.NewLine);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }
}
