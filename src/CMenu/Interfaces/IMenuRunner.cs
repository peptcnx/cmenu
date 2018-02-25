using System.Threading.Tasks;

namespace CMenu
{
    public interface IMenuRunner
    {
        Task Run(IMenu menu);
    }
}