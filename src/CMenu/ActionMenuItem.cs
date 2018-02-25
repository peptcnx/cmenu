using System;
using System.Threading.Tasks;

namespace CMenu
{
    public class ActionMenuItem : IActionMenuItem
    {
        public string Title { get; set; }
        public Func<Task> Action { get; set; }
    }
}