using System.Threading.Tasks;

namespace StreamlootsSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                IStreamlootsService service = new StreamlootsService(args[0]);
                await service.Connect();

                while (true)
                {
                    await Task.Delay(60000);
                }
            }).Wait();
        }
    }
}
