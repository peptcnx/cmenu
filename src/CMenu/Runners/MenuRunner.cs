using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace CMenu.Runners
{
    public class MenuRunner : IMenuRunner
    {
        public MenuRunner(TextReader reader, TextWriter writer)
        {
            Reader = reader;
            Writer = writer;
        }

        public TextReader Reader { get; }
        public TextWriter Writer { get; }

        protected int ConsoleWidth { get; set; } = 80;

        public async Task Run(IMenu menu)
        {
            while (true)
            {
                PrintMenu(menu);
                var item = await GetOption(menu);

                switch (item)
                {
                    case null:
                        return;
                    case ISubMenuMenuItem submenu:
                        await Run(submenu);
                        continue;
                    case IActionMenuItem action:
                        if (action.Action == null)
                            await Writer.WriteLineAsync("Action for item is not defined.");
                        else
                            await action.Action();
                        continue;
                }

                await Writer.WriteLineAsync("Cannot handle this option!");
            }

        }

        protected virtual async Task<IMenuItem> GetOption(IMenu menu)
        {
            while (true)
            {
                var option = (await Reader.ReadLineAsync()).Trim().ToLower();
                if (option == "x")
                {
                    return null;
                }

                if (int.TryParse(option, NumberStyles.None, CultureInfo.InvariantCulture, out int number))
                {
                    if (number > 0 && number <= menu.MenuItems.Count)
                    {
                        var menuItem = menu.MenuItems[number - 1];
                        return menuItem;
                    }
                }

                await Writer.WriteLineAsync($"Value \"{option}\" is not valid. Please enter a valid option.");
                await Writer.WriteAsync(": ");
            }
        }

        protected virtual async void PrintMenu(IMenu menu)
        {
            // print header
            PrintHeader(menu);

            // print items
            int number = 0;
            foreach (var item in menu.MenuItems)
            {
                PrintOptionValue((++number).ToString());
                PrintDelimiter();
                PrintOptionTitle(item.Title);
                if(item is SubMenuMenuItem)
                    PrintSubmenuArrow();
                await Writer.WriteLineAsync();
            }

            // exit option
            PrintOptionValue("X");
            PrintDelimiter();
            PrintOptionTitle("Exit");
            await Writer.WriteLineAsync();

            // print question
            await Writer.WriteAsync("Choose option: ");
        }

        protected virtual async void PrintDelimiter()
        {
            await Writer.WriteAsync(" - ");
        }

        protected virtual async void PrintOptionTitle(string title)
        {
            await Writer.WriteAsync(title);
        }

        protected virtual async void PrintSubmenuArrow()
        {
            await Writer.WriteAsync(" -->");
        }

        protected virtual async void PrintOptionValue(string value)
        {
            await Writer.WriteAsync("  ");
            await Writer.WriteAsync(value);
        }

        protected virtual async void PrintHeader(IMenu menu)
        {
            int charCount = ConsoleWidth - 1;
            int space1Width = (charCount - menu.Title.Length) / 2 - 1;
            int space2Width = charCount - 2 - space1Width - menu.Title.Length;
            string line = new string('#', charCount);

            await Writer.WriteLineAsync(line);

            await Writer.WriteAsync('#');
            await Writer.WriteAsync(new string(' ', space1Width));
            await Writer.WriteAsync(menu.Title);
            await Writer.WriteAsync(new string(' ', space2Width));
            await Writer.WriteLineAsync('#');

            await Writer.WriteLineAsync(line);
        }
    }
}
