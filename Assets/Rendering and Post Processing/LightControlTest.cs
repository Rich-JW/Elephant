using UnityEngine;
using System.Collections;

public class LightControlTest : MonoBehaviour
{
    [SerializeField] Color newAmbient;
    [SerializeField] float intensityMultiplier = 1.0f;
    [SerializeField] float transitionDuration = 1.0f;

    private Color originalAmbient;
    private Coroutine currentTransition;

    void Start()
    {
        originalAmbient = RenderSettings.ambientLight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Color targetColor = newAmbient * intensityMultiplier;
            StartAmbientTransition(targetColor);
            Debug.Log("Enter volume");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartAmbientTransition(originalAmbient);
            Debug.Log("Exit volume");
        }
    }

    void StartAmbientTransition(Color targetColor)
    {
        if (currentTransition != null)
        {
            StopCoroutine(currentTransition);
        }
        currentTransition = StartCoroutine(LerpAmbientLight(RenderSettings.ambientLight, targetColor, transitionDuration));
    }

    IEnumerator LerpAmbientLight(Color from, Color to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            RenderSettings.ambientLight = Color.Lerp(from, to, t);
            yield return null;
        }
        RenderSettings.ambientLight = to;
        currentTransition = null;
    }
}
