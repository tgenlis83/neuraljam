using UnityEngine;

public static class JSONHelper
{
    public static TrainData ParseTrain(string json)
    {
        TrainData trainData = JsonUtility.FromJson<TrainData>(json);

        // Output parsed data
        foreach (var wagon in trainData.wagons)
        {
            Debug.Log($"Wagon ID: {wagon.id}, Passcode: {wagon.passcode}");

            foreach (var person in wagon.people)
            {
                Debug.Log($"UID: {person.uid}, Character: {person.character}, Position: ({string.Join(", ", person.position)})");
            }
        }
        return trainData;
    }

    public static WagonData ParseWagon(string json)
    {
        WagonData wagonData = JsonUtility.FromJson<WagonData>(json);

        // Output parsed data
        Debug.Log($"Wagon ID: {wagonData.id}, Passcode: {wagonData.passcode}");

        foreach (var person in wagonData.people)
        {
            Debug.Log($"UID: {person.uid}, Character: {person.character}, Position: ({string.Join(", ", person.position)})");
        }
        return wagonData;
    }
}
