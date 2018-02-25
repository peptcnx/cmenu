using System;
using System.Threading.Tasks;

namespace CMenu
{
    public interface IActionMenuItem : IMenuItem
    {
        Func<Task> Action { get; set; }
    }
}