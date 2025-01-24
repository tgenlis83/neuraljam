using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public GameObject passengerPrefab;
    public List<GameObject> passengers = new List<GameObject>();
    public Transform passengerParent;
    public WagonStart start;

    public void CreatePassenger(Vector2 position, Quaternion rotation)
    {
        GameObject passenger = Instantiate(passengerPrefab, position, rotation);
        passenger.transform.parent = passengerParent;
        passengers.Add(passenger);
    }
}
