using System.Threading.Tasks;
using Toolbox.Attributes;

public class WaitPoint : Waypoint
{
    private bool _shouldStop = true;

    [Button]
    public void SetContinue()
    {
        _shouldStop = false;
    }
    
    public async Task WaitForGreenLight()
    {
        while (_shouldStop)
        {
            await Task.Yield();
        }
    }
}
