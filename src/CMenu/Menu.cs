using System.Collections.Generic;

namespace CMenu
{
    public class Menu : IMenu
    {
        public string Title { get; set; }

        public IList<IMenuItem> MenuItems { get; } = new List<IMenuItem>();
    }
}