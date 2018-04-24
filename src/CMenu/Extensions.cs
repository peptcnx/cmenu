using System;
using System.Globalization;
using System.Threading.Tasks;
using CMenu.Runners;

namespace CMenu
{
    public static class Extensions
    {
        public static IMenuRunner MenuRunner { get; set; } = new ConsoleMenuRunner();

        public static async Task Run(this IMenu menu)
        {
            await MenuRunner.Run(menu);
        }
    }
}