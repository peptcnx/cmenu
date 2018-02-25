using System.Threading.Tasks;

namespace CMenu
{
    public static class Extensions
    {
        public static IMenuRunner MenuRunner { get; set; } = new ConsoleMenuRunner();

        public static async void Run(this IMenu menu)
        {
            await MenuRunner.Run(menu);
        }
        public static Task RunAsync(this IMenu menu)
        {
            return MenuRunner.Run(menu);
        }
    }
}