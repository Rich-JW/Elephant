using UnityEngine;

namespace WeaponBobbingV2
{
    public class BobStyle4 : WeaponRotatorBobBase
    {
        private const float m_BobbingRotationAmount = 0.75f;
        private Vector3 m_CurrentVelocity; // optional for SmoothDamp

        private void Start()
        {
            m_InitPos = transform.localPosition;
            m_InitRot = transform.localRotation.eulerAngles;
        }

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float aHorz = Mathf.Abs(horizontal);
            float aVert = Mathf.Abs(vertical);
            float xMovement = 0.0f;
            float yMovement = 0.0f;
            float yRotation = 0.0f;

            Vector3 calcPosition = m_InitPos;
            float rotatorValue = m_InitRot.y;

            if (aHorz == 0 && aVert == 0)
            {
                m_BobTimer = 0.0f;
            }
            else
            {
                xMovement = Mathf.Sin(m_BobTimer);
                yMovement = -Mathf.Abs(Mathf.Abs(xMovement) - 1) + 0.25f;
                yRotation = Mathf.Sin(m_BobTimer);

                m_BobTimer += m_BobbingSpeed * Time.deltaTime * InternalTimerMultiplier;

                if (m_BobTimer > Mathf.PI * 2)
                {
                    m_BobTimer -= Mathf.PI * 2;
                }
            }

            float totalMovement = Mathf.Clamp01(aVert + aHorz) * 1.5f;

            // Apply bob to position
            if (xMovement != 0)
            {
                calcPosition.x = m_InitPos.x + xMovement * totalMovement * m_BobbingAmount;
            }
            if (yMovement != 0)
            {
                calcPosition.y = m_InitPos.y + yMovement * m_BobbingAmount;
            }

            // Apply bob to rotation
            if (yRotation != 0)
            {
                rotatorValue = m_InitRot.y + yRotation * totalMovement * m_BobbingRotationAmount;
            }

            // Apply smoothed movement and rotation
            transform.localPosition = Vector3.Lerp(transform.localPosition, calcPosition, Time.deltaTime * InternalMultiplier * m_BobMultiplier);
            m_Rotator.AngleChanges = rotatorValue * m_BobMultiplier;
        }
    }
}
