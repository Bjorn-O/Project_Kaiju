using Unity.Netcode;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    private bool _isHost = false;
    private ulong _clientID;
    
    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += HostingStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += ClientStarted;
    }
    
    private void HostingStarted()
    {
        _isHost = true;
        _clientID = NetworkManager.Singleton.LocalClientId;
        print("Host started with Client ID: " + _clientID);
        PlayerManager.Singleton.SetOwnershipServerRPC(PlayPositions.Shipper,_clientID);
    }
    
    private void ClientStarted(ulong paramId)
    {
        if (paramId != NetworkManager.Singleton.LocalClientId) return;
        _clientID = paramId;
    }

    public void GunOwnershipRequest()
    {
        if (_isHost) return;
        print("Client:" + _clientID + " connected as Gunner");
        PlayerManager.Singleton.SetOwnershipServerRPC(PlayPositions.Gunner,_clientID);
    }

    public void SpotlightOwnershipRequest()
    {
        if (_isHost) return;
        print("Client:" + _clientID + " connected as Spotter");
        PlayerManager.Singleton.SetOwnershipServerRPC(PlayPositions.Spotter,_clientID);
    }
}
