using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(NetworkObject))]
public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager Singleton { get; private set; }

    public UnityEvent gunConnected;
    public UnityEvent lightConnected;
    
    public UnityEvent gunDisconnected;
    public UnityEvent lightDisconnected;

    public UnityEvent playersReady;

    [SerializeField] private GameObject shipInstance;
    [SerializeField] private GameObject gunInstance;
    [SerializeField] private GameObject spotlightInstance;
    [SerializeField] private NetworkObject networkObject;

    private bool _isShipOccupied;
    private bool _isGunOccupied;
    private bool _isSpotlightOccupied;

    //Make Networked Variables
    private ulong _shipPlayerId;
    private ulong _gunPlayerId;
    private ulong _spotlightPlayerId; 


    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
        }
        else
        {
            Singleton = this;
        }

        if (networkObject) return;
        networkObject = GetComponent<NetworkObject>();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetOwnershipServerRPC(PlayPositions playPos, ulong clientId)
    {
        GameObject targetObject = null;
        var isOccupied = false;
        
        switch (playPos)
        {
            case PlayPositions.Shipper:
                targetObject = shipInstance;
                isOccupied = _isShipOccupied;
                break;
            case PlayPositions.Gunner:
                targetObject = gunInstance;
                isOccupied = _isGunOccupied;
                break;
            case PlayPositions.Spotter:
                targetObject = spotlightInstance;
                isOccupied = _isSpotlightOccupied;
                break;
        }
        
        if (isOccupied || !targetObject) return;
        if (!targetObject.TryGetComponent<NetworkObject>(out var networkComponent))
        {
            NetworkObjectNotFound(targetObject);
        }
        networkComponent.ChangeOwnership(clientId);
    }
    
    private void CheckOccupation()
    {
        if (!_isGunOccupied || _isSpotlightOccupied || _isShipOccupied) return;
        playersReady?.Invoke();
    }

    [ServerRpc]
    private void SetPlayerSlotServerRPC(PlayPositions setPosition, ulong clientId)
    {
        switch (setPosition)
        {
            case PlayPositions.Shipper:
                _isShipOccupied = true;
                _shipPlayerId = clientId;
                break;
            case PlayPositions.Gunner:
                _isGunOccupied = true;
                _gunPlayerId = clientId;
                GunOwnershipSetClientRPC();
                break;
            case PlayPositions.Spotter:
                _isSpotlightOccupied = true;
                _spotlightPlayerId = clientId;
                SpotlightOwnershipSetClientRPC();
                break;
        }
        CheckOccupation();
    }

    [ClientRpc]
    private void GunOwnershipSetClientRPC()
    {
        gunConnected?.Invoke();
    }

    [ClientRpc]
    private void SpotlightOwnershipSetClientRPC()
    {
        lightConnected?.Invoke();
    }

    private void NetworkObjectNotFound(GameObject obj)
    {
        print(obj + " doesn't contain NetworkObject");
    }
}