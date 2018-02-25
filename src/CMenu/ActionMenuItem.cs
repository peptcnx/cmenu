using System;
using System.Threading.Tasks;

namespace CMenu
{
    public class ActionMenuItem : IActionMenuItem
    {
        public string Title { get; set; }
        public Func<Task> Action { get; set; }

        public ActionMenuItem()
        {    
        }

        public ActionMenuItem(string title, Func<Task> action)
        {
            Title = title;
            Action = action;
        }

        public ActionMenuItem(string title, Action action)
        {
            Title = title;
            if (action != null)
            {
                Action = () => Task.Run(action);
            }
        }
    }
}