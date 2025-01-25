using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    public Train train;
    public int currentWagon = 0;
    public float timeLeft = 60f;
    
    public CameraController cameraController;
    public GhostController ghostController;
    public TextMeshProUGUI timerText;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Time Left\n" + timeLeft.ToString("F0") + " seconds";
        if (timeLeft <= 0)
        {
            GameLost();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ghostController.SetTarget(null);
        }
    }

    bool isParallelUniverse = false;
    Passenger lastFocusingPassenger;
    public void ParallelUniverseToggle(Passenger focusingPassenger)
    {
        isParallelUniverse = !isParallelUniverse;
        if (isParallelUniverse)
        {
            train.wagons[currentWagon].DisablePassengersExcept(focusingPassenger);
            train.wagons[currentWagon].EnablePassengerItems(focusingPassenger);
        }
        else
        {
            train.wagons[currentWagon].EnableAllPassengers();
            train.wagons[currentWagon].DisableAllItems();
        }
        ParallelUniverseHandler.Instance.ToggleParallelUniverse(focusingPassenger);
        lastFocusingPassenger = focusingPassenger;
        if (isParallelUniverse)
        {
            PhoneHandler.Instance.Show(focusingPassenger);
        }
        else
        {
            PhoneHandler.Instance.Hide();
        }
    }

    public void NextWagon()
    {
        Debug.Log("NextWagon");
        if (isParallelUniverse)
        {
            ParallelUniverseToggle(lastFocusingPassenger);
        }
        currentWagon++;
        if (currentWagon >= train.wagons.Count)
        {
            GameWin();
            return;
        }
        cameraController.SetCameraPosition(train.wagons[currentWagon].start.transform.position);
        PasswordHandler.Instance.SetPassword(train.wagons[currentWagon].passcode);
    }

    public void GameWin()
    {
        timerText.text = "You Win!";
        WinScreen.Instance.Show();
    }

    public void GameLost()
    {
        timerText.text = "You Lose!";
        LoseScreen.Instance.Show();
    }

    public string jsonContent;

    void Start()
    {
        TrainData trainData = APIHelper.GetWagons();

        foreach(var wagonData in trainData.wagons)
        {
            GameObject wagon = Instantiate(train.wagonPrefab, transform.position + Vector3.forward * train.wagons.Count * train.distanceBetweenWagons, Quaternion.identity);
            wagon.transform.parent = train.transform;
            Wagon w = wagon.GetComponent<Wagon>();
            w.passcode = wagonData.passcode;
            train.wagons.Add(w);
            foreach(var person in wagonData.people)
            {
                Vector3 worldPosition = NormalizedWagonSpaceToWorldSpace(w.transform.position, new Vector2(person.position[0], person.position[1]));
                w.CreatePassenger(person.uid, worldPosition, Quaternion.Euler(0, person.rotation, 0));

                // for each item spawn it randomly in the wagon
                if (person.items != null)
                {
                    foreach (var item in person.items)
                    {
                        Vector3 itemPosition = NormalizedWagonSpaceToWorldSpace(w.transform.position, new Vector2(Random.Range(0.15f, 0.85f), Random.Range(0.15f, 0.85f)));
                        itemPosition.y += 0.5f;
                        w.CreateItem(person.uid, itemPosition, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)), item);
                    }
                }
            }
        }

        currentWagon = -1;
        NextWagon();

        train.wagons[currentWagon].EnableAllPassengers();
        train.wagons[currentWagon].DisableAllItems();
    }

    float wagonWidth = 22.5f;
    float wagonHeight = 6f;
    float floorHeight = 2.01f; // 1 + eps
    Vector3 NormalizedWagonSpaceToWorldSpace(Vector3 wagonWorldSpace, Vector2 position)
    {
        // the position is the normalized position of the person in the wagon on the 2d floor
        // floors are 1 + eps units high
        // floors are 22.5 x 6 units
        // 0,0 is the bottom left corner of the wagon (from the perspective of the camera)
        // 1,1 is the top right corner of the wagon (from the perspective of the camera)
        // wagonWorldSpace is the center of the wagon, from the wheels

        float worldX = -wagonWorldSpace.z + (wagonWidth / 2) - position.x * wagonWidth;
        float worldY = wagonWorldSpace.y + floorHeight; // Assuming the person is on the first floor
        float worldZ = wagonWorldSpace.x + (wagonHeight / 2) - position.y * wagonHeight;

        return new Vector3(worldZ, worldY, -worldX);
    }

    // get min and max z position for the camera pan
    public Vector2 GetCameraPanZMinMax()
    {
        float wagonZ = train.wagons[currentWagon].start.transform.position.z;
        float margin = 2.5f;
        float minZ = wagonZ + margin;
        float maxZ = wagonZ + wagonWidth - margin;
        return new Vector2(minZ, maxZ);
    }
}
