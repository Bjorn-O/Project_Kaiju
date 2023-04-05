using System;
using System.Collections;
using System.Collections.Generic;
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
    }
    
    private void ClientStarted(ulong paramId)
    {
        if (paramId != NetworkManager.Singleton.LocalClientId) return;
        _clientID = paramId;
    }
}
