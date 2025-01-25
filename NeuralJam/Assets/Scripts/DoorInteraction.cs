using Unity.VisualScripting;
using UnityEngine;

public class DoorInteraction : OutlineInteraction
{
    public override void OnInteract()
    {
        base.OnInteract();
        PasswordHandler.Instance.Show();
    }
}
