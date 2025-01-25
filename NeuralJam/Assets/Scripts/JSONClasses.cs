using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TrainData
{
    public List<WagonData> wagons;
}

[Serializable]
public class WagonData
{
    public int id;
    public string passcode;
    public List<Person> people;
}

[Serializable]
public class Person
{
    public string uid;
    public float[] position;  // Use array for JSON array mapping
    public float rotation; // Use array for JSON array mapping
    public string character;
    public string model_type;
}
