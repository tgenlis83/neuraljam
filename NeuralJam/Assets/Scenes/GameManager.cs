using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
            cameraController.target = null;
            ghostController.SetTarget(null);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextWagon();
        }

        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (i < train.wagons[currentWagon].passengers.Count)
                {
                    cameraController.target = train.wagons[currentWagon].passengers[i].transform;
                    ghostController.SetTarget(train.wagons[currentWagon].passengers[i].transform);
                }
            }
        }
    }


    public void NextWagon()
    {
        currentWagon++;
        if (currentWagon >= train.wagons.Count)
        {
            GameWin();
            return;
        }
        cameraController.target = train.wagons[currentWagon].start.transform;
    }

    public void GameWin()
    {
        timerText.text = "You Win!";
    }

    public void GameLost()
    {
        timerText.text = "You Lose!";
    }

    void Start()
    {
        // train.CreateTrain(
        //     new List<Vector2> { new Vector2(0, 0) },
        //     new List<Quaternion> { Quaternion.identity },
        //     10
        // );
        cameraController.target = train.wagons[currentWagon].start.transform;
    }
}
