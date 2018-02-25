using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using CMenu.Runners;

namespace CMenu.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Extensions.MenuRunner = new MenuRunner(System.Console.In, System.Console.Out);
            GetMenu().Run();
        }

        private static Func<string, Task> Print => System.Console.Out.WriteLineAsync;

        private static IMenu GetMenu()
        {
            return new Menu
            {
                Title = "Sample application",
                MenuItems =
                {
                    new ActionMenuItem
                    {
                        Title = "Greet me",
                        Action = () => Print("Ahoj")
                    },
                    new ActionMenuItem
                    {
                        Title = "Say hello",
                        Action = () => Print("HELLO")
                    },
                    new SubmenuMenuItem("item title", CreateItems(5))
                }
            };
        }

        private static ActionMenuItem[] CreateItems(int count)
        {
            return Enumerable.Range(1, count).Select(num => new ActionMenuItem
            {
                Title = $"Menu item {num}",
                Action = () => Print($"Action from item {num}")
            }).ToArray();
        }
    }
}
