using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float stopDistance = 1f;

    Transform target;
    Vector3 initialPosition;

    public AnimationCurve floatingAnimation;
    public float floatSpeed;
    public float floatAmount;
    public float rotationOffset;
    public float rotationSpeed;

    float time = 0f;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        Vector3 final = initialPosition;
        if (target != null)
        {
            final = target.position;
        }
        transform.position = new Vector3(transform.position.x, final.y + floatingAnimation.Evaluate(time) * floatAmount, transform.position.z);
        
        time += Time.deltaTime * floatSpeed;
        if (time > 1f)
        {
            time = 0f;
        }
        
        if (target && Vector3.Distance(transform.position, target.position) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, final, maxSpeed * Time.deltaTime);
        }
        
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0; // Look direction only on the y axis
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation *= Quaternion.Euler(0, -90, rotationOffset + floatingAnimation.Evaluate(time) * floatAmount); // Add 90 degrees offset on the y axis and use z axis to animate and offset
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
