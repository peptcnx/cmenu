using System;
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
            Localization = new MenuRunnerLocalization();
        }

        public TextReader Reader { get; }
        public TextWriter Writer { get; }
        public MenuRunnerLocalization Localization { get; set; }


        protected int ConsoleWidth { get; set; } = -1;

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
                    case ISubmenuMenuItem submenu:
                        await Run(submenu);
                        continue;
                    case IActionMenuItem action:
                        if (action.Action == null)
                            await Writer.WriteLineAsync(Localization?.ActionIsNotDefined);
                        else
                            await action.Action();
                        continue;
                }

                await Writer.WriteLineAsync(Localization?.CannotHandleOption);
            }
        }
        
        protected virtual async Task<IMenuItem> GetOption(IMenu menu)
        {
            // print question
            string value = await Enter(Localization?.ChooseOption);

            var exitValue = Localization?.ExitOptionValue?.Trim().ToLower();

            while (true)
            {
                var option = value.Trim().ToLower();
                if (option == exitValue)
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

                await Writer.WriteLineAsync(string.Format(Localization?.OptionIsNotValid, value));
                value = await Enter(Localization?.TryAgainPrompt);
            }
        }

        /// <summary>
        /// Ask user for a value
        /// </summary>
        /// <param name="message">Display message for user</param>
        /// <returns></returns>
        public virtual async Task<string> Enter(string message)
        {
            await Writer.WriteAsync(message);
            return await Reader.ReadLineAsync();
        }

        /// <summary>
        /// Ask user for a value of particular type
        /// </summary>
        /// <typeparam name="T">Type of requested value</typeparam>
        /// <param name="message">Display message for user</param>
        /// <returns></returns>
        public virtual async Task<T> Enter<T>(string message)
        {
            var text = await Enter(message);
            var value = ConvertValue<T>(text);
            return value;
        }

        /// <summary>
        /// Convert text to particular type
        /// </summary>
        /// <typeparam name="T">The resulting type</typeparam>
        /// <param name="value">Input string</param>
        /// <returns></returns>
        protected virtual T ConvertValue<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
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
                if(item is SubmenuMenuItem)
                    PrintSubmenuArrow();
                await Writer.WriteLineAsync();
            }

            // exit option
            PrintOptionValue(Localization?.ExitOptionValue);
            PrintDelimiter();
            PrintOptionTitle(Localization?.ExitOptionTitle);
            await Writer.WriteLineAsync();
        }

        protected virtual async void PrintDelimiter()
        {
            await Writer.WriteAsync(Localization?.OptionDelimiter);
        }

        protected virtual async void PrintOptionTitle(string title)
        {
            await Writer.WriteAsync(title);
        }

        protected virtual async void PrintSubmenuArrow()
        {
            await Writer.WriteAsync(Localization?.SubmenuArray);
        }

        protected virtual async void PrintOptionValue(string value)
        {
            await Writer.WriteAsync(Localization?.OptionIndent);
            await Writer.WriteAsync(value);
        }

        protected virtual async void PrintHeader(IMenu menu)
        {
            int charCount = ConsoleWidth < 0 ? menu.Title.Length + 4 : ConsoleWidth - 1;
            int space1Width = (charCount - menu.Title.Length) / 2 - 1;
            int space2Width = charCount - 2 - space1Width - menu.Title.Length;
            string line = new string(Localization.MenuHeaderBorder, charCount);

            await Writer.WriteLineAsync(line);

            await Writer.WriteAsync(Localization.MenuHeaderBorder);
            await Writer.WriteAsync(new string(' ', space1Width));
            await Writer.WriteAsync(menu.Title);
            await Writer.WriteAsync(new string(' ', space2Width));
            await Writer.WriteLineAsync(Localization.MenuHeaderBorder);

            await Writer.WriteLineAsync(line);
        }
    }
}
