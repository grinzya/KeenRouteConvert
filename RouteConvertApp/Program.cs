using System.Net;

namespace RouteConvertApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPNetwork2 ipnet = new IPNetwork2();
            string inputPath = "subnets.txt";
            string outputPath = "routes.txt";
            using (StreamReader sr = new StreamReader(inputPath))
            {
                string? line;
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        ipnet = IPNetwork2.Parse(line);

                        Console.WriteLine("route ADD {0} MASK {1} 0.0.0.0", ipnet.Network, ipnet.Netmask);
                        writer.WriteLine("route ADD {0} MASK {1} 0.0.0.0", ipnet.Network, ipnet.Netmask);
                        
                    }
                }

               
            }
            
        }
    }
}
