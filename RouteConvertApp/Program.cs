using System.Net;

namespace RouteConvertApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "subnets.txt";
            const int MAX_LINES_PER_FILE = 1024; //Constraints of keenetic routers
            int fileCounter = 1;
            int lineCounter = 0;
            StreamWriter writer = new StreamWriter($"routes_{fileCounter}.txt", false);

            try
            {
                using (StreamReader sr = new StreamReader(inputPath))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Skip empty strings
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        try
                        {
                            IPNetwork2 ipnet = IPNetwork2.Parse(line);

                            // If line limit is reached, create a new file
                            if (lineCounter >= MAX_LINES_PER_FILE)
                            {
                                writer.Close();
                                fileCounter++;
                                lineCounter = 0;
                                writer = new StreamWriter($"routes_{fileCounter}.txt", false);
                            }

                            writer.WriteLine("route ADD {0} MASK {1} 0.0.0.0", ipnet.Network, ipnet.Netmask);
                            Console.WriteLine("route ADD {0} MASK {1} 0.0.0.0", ipnet.Network, ipnet.Netmask);
                            lineCounter++;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при обработке строки '{line}': {ex.Message}");
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
            finally
            {
                writer.Close();
            }
        }
    }
}