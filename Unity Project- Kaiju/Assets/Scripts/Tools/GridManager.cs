using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int columnLength, rowLength;
    [SerializeField] private float x_Space, y_Space;
    [SerializeField] private GameObject prefab;

    private Vector3 placeVector;

    void Start()
    {
        for (int i = 0; i < columnLength * rowLength; i++)
        {
            placeVector.x = x_Space * (i % columnLength);
            placeVector.z = y_Space * (i / columnLength);
            Instantiate(prefab, placeVector, Quaternion.identity);
        }
    }
}
