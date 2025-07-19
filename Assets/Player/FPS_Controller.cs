using UnityEngine;

public class FPS_Controller : MonoBehaviour
{
    public Transform character;  // Your body to rotate yaw
 

    Transform cam;

    public float yawSpeed = 180f;
    public float pitchSpeed = 180f;

    public float smoothing;

    public Vector2 pitchClampMinMax = new Vector2(-80, 80);
    public bool invertY = false;

    float yaw;
    float pitch;

    public void OnPlayerSpawn()
    {
      
        character = transform.root;
        cam = transform;

        if (character == null || cam == null) return;
        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initialize yaw/pitch from actual transforms
        Vector3 euler = character.rotation.eulerAngles;
        yaw = euler.y;

        pitch = 0;
 

 
    }

    void Update()
    {
        if (character == null || cam == null) return;

            float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        yaw += mouseX * yawSpeed;
        pitch += mouseY * pitchSpeed * (invertY ? 1 : -1);

        pitch = Mathf.Clamp(pitch, pitchClampMinMax.x, pitchClampMinMax.y);

        // Apply pitch

        cam.localRotation = Quaternion.Slerp(cam.localRotation, Quaternion.Euler(pitch, 0f, 0f), Time.deltaTime * smoothing);

        // Apply yaw

     //   cameraPivot.localRotation = Quaternion.Slerp(cameraPivot.localRotation, Quaternion.Euler(0f, yaw, 0f), Time.deltaTime * smoothing);

        // apply char rot

        character.rotation = Quaternion.Euler(0f, yaw, 0f);


    }

    private void FixedUpdate()
    {

    }
}