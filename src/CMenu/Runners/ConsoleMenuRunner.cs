using System;

namespace CMenu.Runners
{
    public class ConsoleMenuRunner : MenuRunner
    {
        public ConsoleMenuRunner() : base(Console.In, Console.Out)
        {
        }

        protected override void PrintSubmenuArrow()
        {
            Console.ForegroundColor = ConsoleColor.White;
            base.PrintSubmenuArrow();
            Console.ResetColor();
        }

        protected override void PrintOptionValue(string value)
        {
            Console.ForegroundColor = ConsoleColor.White;
            base.PrintOptionValue(value);
            Console.ResetColor();
        }

        protected override void PrintMenu(IMenu menu)
        {
            ConsoleWidth = Console.WindowWidth;
            Console.ForegroundColor = ConsoleColor.Yellow;
            base.PrintMenu(menu);
            Console.ResetColor();
        }

        protected override void PrintOptionTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            base.PrintOptionTitle(title);
            Console.ResetColor();
        }
    }
}