using UnityEngine;

public class PassengerInteraction : OutlineInteraction
{
    [SerializeField]
    Passenger passenger;
    string firstInteractionText;
    protected override void Start()
    {
        base.Start();
        passenger = GetComponent<Passenger>();
        // in the string, replace {name} with the passenger's name
        interactionName = interactionName.Replace("{name}", passenger.username);
        firstInteractionText = interactionName;
    }

    bool inParallelUniverse = false;
    public override void OnInteract()
    {
        base.OnInteract();
        GameManager.Instance.ParallelUniverseToggle(passenger);
        inParallelUniverse = !inParallelUniverse;
        if (inParallelUniverse)
        {
            interactionName = "Leave {name}'s mind".Replace("{name}", passenger.username);
        }
        else
        {
            interactionName = firstInteractionText;
        }
    }
}
