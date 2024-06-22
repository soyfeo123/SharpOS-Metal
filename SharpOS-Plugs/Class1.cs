using IL2CPU.API.Attribs;
using static Cosmos.Core.INTs;
namespace SharpOS.Plugs
{
    [Plug(Target = typeof(Cosmos.Core.INTs))]
    public class SystemExceptions
    {
        public static void HandleException(uint aEIP, string aDescription, string aName, ref IRQContext ctx, uint lastKnownAddressValue = 0)
        {
            Kernel.instance.DrawErrorScreen(new Exception("System Exception! AEIP: " + aEIP.ToString() + "; Description: " + aDescription + "; Name" + aName));
        }
    }
}
