using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] float smooth, yMultiplier, xMultiplier;
    [SerializeField] bool invertX, invertY;

    Quaternion rot;

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * xMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * yMultiplier;

        if (invertY) mouseY = mouseY * -1;
        if (invertX) mouseX = mouseX * -1;

        Quaternion rotationPitch = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationYaw   = Quaternion.AngleAxis(mouseX, Vector3.up);

        rot = rotationPitch * rotationYaw;
    }

    private void FixedUpdate()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rot, smooth * Time.deltaTime);
    }
}
