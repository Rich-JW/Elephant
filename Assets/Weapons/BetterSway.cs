using UnityEngine;

public class BetterSway : MonoBehaviour
{
    [SerializeField] [Range(-1 ,1)] float input_x;
    [SerializeField] float swaySensitivity;
    [SerializeField] float swayDamping;
    Vector3 fwd, cam_fwd;
    Camera cam;

    private void Start()
    {
       
     cam = Camera.main;

    }
    private void Update()
    {

        if (cam == null) return;

        fwd = transform.forward;
        cam_fwd = transform.forward;

        fwd.x = 0;
        cam_fwd.x = 0;
        fwd.z = 0;
        cam_fwd.z = 0;

        Quaternion rot = Quaternion.Slerp(transform.localRotation, cam.transform.rotation, Time.deltaTime * swayDamping);



        float difference = Vector3.SignedAngle(fwd, cam_fwd, transform.up);
        input_x = Mathf.Clamp(difference / 180f, -1f, 1f) * swaySensitivity;

        Debug.Log("Fwd: " + fwd + " + Cam: " + cam_fwd + " Difference: " + difference);


        transform.localRotation = Quaternion.Euler(0, difference, 0);
    }
}
