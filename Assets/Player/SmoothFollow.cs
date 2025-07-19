using UnityEngine;

public class SmoothFollow : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed;
    [SerializeField] Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null) transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed * Time.deltaTime);
    }
}
