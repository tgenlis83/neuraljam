using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public GameObject wagonPrefab;
    public GameObject locomotivePrefab;
    public List<Wagon> wagons = new List<Wagon>();
    public float distanceBetweenWagons = 1f;
}
