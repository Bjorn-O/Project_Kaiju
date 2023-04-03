using Unity.Netcode;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BoatManager : NetworkBehaviour
{
    public UnityEvent onInitialize;
    
    private BoatMovement _boatMovement;

    private void Awake()
    {
        _boatMovement = GetComponent<BoatMovement>();
        
    }

    public override void OnGainedOwnership()
    {
        base.OnGainedOwnership();
        onInitialize.Invoke();
        print("Hey, I'm not a slave, baka!");
    }
}
