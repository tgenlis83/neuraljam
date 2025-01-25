using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public GameObject passengerPrefab;
    public GameObject[] itemPrefabs;
    public Dictionary<string, GameObject> passengers = new Dictionary<string, GameObject>();
    public Dictionary<string, List<GameObject>> items = new Dictionary<string, List<GameObject>>();
    public Transform passengerParent;
    public WagonStart start;
    public string passcode;

    public void CreatePassenger(string uid, string username, Vector3 position, Quaternion rotation)
    {
        GameObject passenger = Instantiate(passengerPrefab, position, rotation);
        passenger.transform.parent = passengerParent;

        Passenger p = passenger.GetComponent<Passenger>();
        p.uid = uid;
        p.username = username;

        passengers.Add(uid, passenger);
    }

    public void CreateItem(string passengerUID, Vector3 position, Quaternion rotation, string itemName)
    {
        GameObject item = Instantiate(Resources.Load<GameObject>("Items/" + itemName), position, rotation);
        item.transform.parent = passengerParent;
        if (!items.ContainsKey(passengerUID))
        {
            items.Add(passengerUID, new List<GameObject>());
        }
        items[passengerUID].Add(item);
    }

    public void DisablePassengersExcept(Passenger focusingPassenger)
    {
        foreach (var passenger in passengers.Values)
        {
            if (passenger != focusingPassenger.gameObject)
            {
                passenger.gameObject.SetActive(false);
            }
        }
        focusingPassenger.gameObject.SetActive(true);
    }

    public void EnablePassengerItems(Passenger passenger)
    {
        if (items.ContainsKey(passenger.uid))
        {
            foreach (var item in items[passenger.uid])
            {
                foreach (var renderer in item.GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = true;
                }
            }
        }
    }

    public void EnableAllPassengers()
    {
        foreach (var passenger in passengers.Values)
        {
            passenger.SetActive(true);
        }
    }

    public void DisableAllItems()
    {
        foreach (var passengerItems in items.Values)
        {
            foreach (var item in passengerItems)
            {
                foreach (var renderer in item.GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = false;
                }
            }
        }
    }
}
