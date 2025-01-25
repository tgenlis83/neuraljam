using UnityEngine;

public class OutlineInteraction : MonoBehaviour
{
    private Outline outline;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        outline = GetComponent<Outline>();
    }

    public string interactionName;

    public AnimationCurve outlineAnimation;
    public float min, max;
    public float speed;

    float time = 0f;
    void Update()
    {
        if (!outline.enabled)
            return;
        time += Time.deltaTime * speed;
        if (time > 1f)
        {
            time = 0f;
        }
        outline.OutlineWidth = outlineAnimation.Evaluate(time) * (max - min) + min;
    }

    public virtual void OnInteract()
    {
        Debug.Log("Interacted with " + interactionName);
    }

    public void OnHovered()
    {
        outline.enabled = true;
    }

    public void OnNotHovered()
    {
        outline.enabled = false;
    }
}
