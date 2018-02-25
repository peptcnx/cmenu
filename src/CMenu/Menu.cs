using System.Collections.Generic;

namespace CMenu
{
    public class Menu : IMenu
    {
        public string Title { get; set; }

        public IList<IMenuItem> MenuItems { get; } = new List<IMenuItem>();

        public Menu()
        {
            
        }

        public Menu(string title, IEnumerable<IMenuItem> items)
        {
            Title = title;
            if (items != null)
            {
                foreach (var item in items)
                {
                    MenuItems.Add(item);
                }
            }
        }
    }
}