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
            interactionName = "Leave {name}'s mind";
        }
        else
        {
            interactionName = firstInteractionText;
        }
    }
}
