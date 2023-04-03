using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayerManager : NetworkBehaviour
{
    [SerializeField] private GameObject shipGameObject;
    [SerializeField] private GameObject gunGameObject;
    [SerializeField] private GameObject spotlightGameObject;
    
    [SerializeField] private Transform gunPosition;
    [SerializeField] private Transform spotlightPosition; 

    public void InstantiateShip()
    {
        shipGameObject.GetComponent<NetworkObject>().ChangeOwnership(NetworkManager.Singleton.LocalClient.ClientId);
    }
}
