using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : MonoBehaviour
{

    [SerializeField] float defaultIntensity;

    [SerializeField] float minimumIntensity;
    Camera cam;
    [SerializeField] float lightDampRange;
    [SerializeField] KeyCode toggleFlashlight;
    [SerializeField] Vector2 fadeInOutSmoothness;

    float intensity;
    Light light;
    [SerializeField] float spherecastRadius;

    [SerializeField] AnimationCurve intensityCurve;

    bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        light = GetComponent<Light>();

        if (light != null) light.enabled = active;
        intensity = defaultIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRaycast();
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(toggleFlashlight))
        {
            active = !active;

            if (light != null) light.enabled = active;
        }
    }

    void HandleRaycast()
    {
        if (cam == null || light == null) return;

        
        if (Physics.SphereCast(cam.transform.position, spherecastRadius, cam.transform.forward, out RaycastHit hit, 1000f))
        {
            float distance = Vector3.Distance(transform.position, hit.point);

            float t = Mathf.InverseLerp(0, lightDampRange, distance); // Normalize distance (adjust max range if needed)
            float curveValue = intensityCurve.Evaluate(t); // Apply curve
            float targetIntensity = Mathf.Lerp(defaultIntensity, minimumIntensity, curveValue); // Curve controls blend

            light.intensity = Mathf.Lerp(light.intensity, targetIntensity, Time.deltaTime * fadeInOutSmoothness.x);
        }
        else
        {
            light.intensity = Mathf.Lerp(light.intensity, defaultIntensity, Time.deltaTime * fadeInOutSmoothness.y);
        }
 
    }
}
