using System;
using System.Threading.Tasks;

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

        private static IMenu GetMenu() => new Menu
        {
            Title = "Sample application",
            MenuItems =
            {
                new ActionMenuItem
                {
                    Title = "Greet me",
                    Description = "Print greetings",
                    Action = () => Print("Ahoj")
                },
                new ActionMenuItem
                {
                    Title = "xxxx",
                    Description = "jfdklsajdklsaj dsla dlk sajlkd",
                    Action = () => Print("rrrr")
                },
                new SubMenuMenuItem
                {
                    Title = "item title",
                    Description = "item description",
                    MenuItems =
                    {
                        new ActionMenuItem
                        {
                            Title = "xxxx",
                            Action = () => Print("www")
                        }
                    }
                }

            }
        };
    }
}
