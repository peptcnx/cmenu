using System.Collections.Generic;

namespace CMenu
{
    public class SubmenuMenuItem : Menu, ISubmenuMenuItem
    {
        public SubmenuMenuItem()
        {
        }

        public SubmenuMenuItem(string title, IEnumerable<IMenuItem> items) : base(title, items)
        {
        }
    }
}