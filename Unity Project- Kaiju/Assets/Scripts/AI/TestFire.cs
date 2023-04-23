using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFire : MonoBehaviour
{
    [SerializeField] private AttackSystem monsterAttack;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(monsterAttack.FloorMarkerAttack(new Vector3(0, 10, 0), Quaternion.Euler(90, 0, 0), 5));
        }
    }
}
