using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSpawnPoint : MonoBehaviour
{

    bool active = false;
    [SerializeField] KeyCode saveKey;
    public Vector3 Position { get { return transform.position; } }
    public Quaternion Rotation { get { return transform.localRotation; } }

    public void OnTriggerStay(Collider other)
    {
        active = other.CompareTag("Player");
       
        if (active)
        {
            if (Input.GetKeyDown(saveKey))
            {
                GameStateManager.Instance.UpdatePlayerSpawnPoint(this);
                // GameStateManager easysave savegame
            }
        }

    }
}
