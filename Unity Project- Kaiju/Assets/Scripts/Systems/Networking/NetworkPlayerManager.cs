using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class NetworkPlayerManager : NetworkBehaviour
{
    public static NetworkPlayerManager Singleton { get; private set; }
    
    [SerializeField] private GameObject shipGameObject;
    [SerializeField] private GameObject gunGameObject;
    [SerializeField] private GameObject lightGameObject;

    private bool _isShipOccupied;
    private bool _isGunOccupied;
    private bool _isLightOccupied;


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
    }

    public void SetShipOwnership(ulong clientId)
    {
        if (_isShipOccupied) return;
        shipGameObject.GetComponent<NetworkObject>().ChangeOwnership(clientId);
        _isShipOccupied = true;
    }

    public void SetGunOwnership(ulong clientId)
    {
        if (_isGunOccupied) return;
        gunGameObject.GetComponent<NetworkObject>().ChangeOwnership(clientId);
        _isGunOccupied = true;
    }

    public void SetLightOwnership(ulong clientId)
    {
        if (_isLightOccupied) return;
        lightGameObject.GetComponent<NetworkObject>().ChangeOwnership(clientId);
        _isLightOccupied = true;
    }
}
