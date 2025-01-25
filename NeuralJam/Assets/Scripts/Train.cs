using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public GameObject wagonPrefab;
    public List<Wagon> wagons = new List<Wagon>();
    public float distanceBetweenWagons = 1f;

    public void CreateTrain(List<Vector2> passengerPositions, List<Quaternion> passengerRotations, int trainLength)
    {
        for (int i = 0; i < trainLength; i++)
        {
            GameObject wagon = Instantiate(wagonPrefab, transform.position + Vector3.right * i * distanceBetweenWagons, Quaternion.identity);
            wagon.transform.parent = this.transform;
            Wagon w = wagon.GetComponent<Wagon>();
            wagons.Add(w);
            for (int j = 0; j < passengerPositions.Count; j++)
            {
                w.CreatePassenger(passengerPositions[j], passengerRotations[j]);
            }
        }
    }
}
