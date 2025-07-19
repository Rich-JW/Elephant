using UnityEngine;

namespace WeaponBobbing
{
    public class WeaponBob : MonoBehaviour
    {
        [Header("Bobbing Settings")]
        public float bobSpeed = 7f;                  // Speed of the bob cycle
        public float bobAmount = 0.05f;              // How far it moves up/down
        public float bobRotationAmount = 2f;         // How much rotation to apply
        public float movementThreshold = 0.1f;       // How much input is needed to start bobbing
        public float smoothing = 8f;                 // How smoothly the bob resets

        private float bobTimer = 0f;
        private Vector3 initialPos;
        private Quaternion initialRot;

        void Start()
        {
            initialPos = transform.localPosition;
            initialRot = transform.localRotation;
        }

        public void UpdateBob()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            bool isMoving = new Vector2(horizontal, vertical).sqrMagnitude > movementThreshold * movementThreshold;

            Vector3 targetPos = initialPos;
            Quaternion targetRot = initialRot;

            if (isMoving)
            {
                bobTimer += Time.deltaTime * bobSpeed;

                float bobOffsetY = Mathf.Sin(bobTimer) * bobAmount;
                float bobOffsetX = Mathf.Cos(bobTimer * 0.5f) * bobAmount * 0.5f; // subtle sideways sway
                float rotZ = Mathf.Sin(bobTimer) * bobRotationAmount;

                targetPos += new Vector3(bobOffsetX, bobOffsetY, 0f);
                targetRot = initialRot * Quaternion.Euler(0f, 0f, rotZ);
            }
            else
            {
                bobTimer = 0f; // reset timer for consistency
            }

            // Smoothly interpolate toward bobbed or idle state
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * smoothing);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * smoothing);
        }
    }
}
