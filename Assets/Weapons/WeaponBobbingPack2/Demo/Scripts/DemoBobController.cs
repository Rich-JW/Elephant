using UnityEngine;

namespace WeaponBobbingV2 {
	public class DemoBobController : MonoBehaviour {
		WeaponBobBase[] m_bobScripts;
		CharacterController m_CharacterController;

		void Start() {
			m_CharacterController = GetComponentInParent<CharacterController>();
			m_bobScripts = GetComponents<WeaponBobBase>();
		}

		void Update() {
			CheckGrounded();
			CheckRun();
		}

		void CheckGrounded() {
			if (m_CharacterController.isGrounded) {
				foreach(WeaponBobBase bobScript in m_bobScripts) {
					bobScript.enabled = true;
				}
			}
			else {
				foreach(WeaponBobBase bobScript in m_bobScripts) {
					bobScript.enabled = false;
				}
			}
		}

		void CheckRun() {
			if (Input.GetKey(KeyCode.LeftShift)) {
				foreach(WeaponBobBase bobScript in m_bobScripts) {
					if (!bobScript.enabled) continue;

					bobScript.BobMultiplier = 2.0f;
					bobScript.BobbingSpeed = bobScript.DefaultBobbingSpeed * 1.5f;
					bobScript.BobbingAmount = bobScript.DefaultBobbingAmount * 2.5f;
					bobScript.GetComponent<Animator>().SetBool("Run", true);
				}
			}
			else {
				foreach(WeaponBobBase bobScript in m_bobScripts) {
					if (!bobScript.enabled) continue;
					
					bobScript.BobMultiplier = 1f;
					bobScript.BobbingSpeed = bobScript.DefaultBobbingSpeed;
					bobScript.BobbingAmount = bobScript.DefaultBobbingAmount;
					bobScript.GetComponent<Animator>().SetBool("Run", false);
				}
			}
		}
	}
}