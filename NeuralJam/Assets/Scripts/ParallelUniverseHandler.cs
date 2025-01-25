using UnityEngine;
using System.Collections;

public class ParallelUniverseHandler : MonoBehaviour
{
    private static ParallelUniverseHandler instance;

    public static ParallelUniverseHandler Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public Transform sun;
    public Light sunLight;
    public Light cameraSpotlight;
    private Coroutine rotateSunCoroutine;

    private Quaternion originalRotation;
    private bool isParallel = false;

    void Start()
    {
        originalRotation = sun.rotation;
    }

    public void ToggleParallelUniverse(Passenger focusingPassenger)
    {
        if (rotateSunCoroutine != null)
        {
            StopCoroutine(rotateSunCoroutine);
        }
        rotateSunCoroutine = StartCoroutine(RotateSun(focusingPassenger));
        isParallel = !isParallel;
    }

    public AnimationCurve sunRotationCurve;
    public float sunRotationDuration = 1f;
    public Gradient sunColorGradient;
    public IEnumerator RotateSun(Passenger focusingPassenger)
    {
        float time = 0f;
        Quaternion startRotation = sun.rotation;
        Quaternion endRotation = isParallel ? originalRotation : originalRotation * Quaternion.Euler(0f, 180f * (isParallel ? 1 : -1), 0f);

        Light passengerLight = focusingPassenger.passengerLight;
        Light passengerSpotLight = focusingPassenger.passengerSpotlight;

        passengerLight.gameObject.SetActive(true);
        passengerSpotLight.gameObject.SetActive(true);
        cameraSpotlight.gameObject.SetActive(true);


        while (time < 1f)
        {
            time += Time.deltaTime / sunRotationDuration;
            float curveValue = sunRotationCurve.Evaluate(time);
            sun.rotation = Quaternion.Lerp(startRotation, endRotation, curveValue);
            sunLight.intensity = isParallel ? sunRotationCurve.Evaluate(1f - time) * 0.7f + 0.3f : curveValue * 0.7f + 0.3f;
            sunLight.color = sunColorGradient.Evaluate(isParallel ? 1f - curveValue : curveValue);

            passengerLight.intensity = isParallel ? curveValue * 2f : sunRotationCurve.Evaluate(1f - time) * 2f;
            passengerSpotLight.intensity = isParallel ? curveValue * 1f : sunRotationCurve.Evaluate(1f - time) * 1f;
            RenderSettings.ambientIntensity = !isParallel ? curveValue * 0.9f + 0.1f : sunRotationCurve.Evaluate(1f - time) * 0.9f + 0.1f;
            cameraSpotlight.intensity = isParallel ? curveValue * 1f : sunRotationCurve.Evaluate(1f - time) * 1f;
            yield return null;
        }
        rotateSunCoroutine = null; // Reset the coroutine reference when done
        passengerLight.gameObject.SetActive(isParallel);
        cameraSpotlight.gameObject.SetActive(isParallel);
        passengerSpotLight.gameObject.SetActive(isParallel);
    }
}
