using System.Collections.Generic;

namespace CMenu
{
    public interface IMenu
    {
        string Title { get; set; }
        IList<IMenuItem> MenuItems { get; }
    }
}