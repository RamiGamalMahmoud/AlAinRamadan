using AlAinRamadan.Core.Models;

namespace AlAinRamadan.Core.Abstraction.Contexts
{
    public interface IFamilyContext
    {
        Family CurrentFamily { set; }

        Family GetCurrentFamily();
    }
}
