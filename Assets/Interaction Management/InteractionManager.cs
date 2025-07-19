using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Camera cam;
    [SerializeField] LayerMask mask;

  //  [ReadOnly]
    [SerializeField] string interactingWith;

    IInteractable currentInteractionHover;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRaycast();
    }

    /*
    void HandleRaycast()
    {
        if (cam == null) return;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 1000f, mask))
        {
            IInteractable interactable = GameStateManager.Instance.TryGetInteractable(hit.transform.gameObject.GetInstanceID());

            if (interactable != null)
            {
                if (Vector3.Distance(cam.transform.position, hit.transform.position) <= interactable.GetInteractableDistance())
                {
                    currentInteractionHover = interactable;
                    interactingWith = interactable.GetName();
                    
                }
                else
                {
                    currentInteractionHover = null;
                    interactingWith = string.Empty;
                }
            }
            else
            {
                currentInteractionHover = null;
                interactingWith = string.Empty;
            }
        }
        else
        {
            currentInteractionHover = null;
            interactingWith = string.Empty;
        }
    }
    */

    void HandleRaycast()
    {
        if (cam == null) return;

        currentInteractionHover = null;
        interactingWith = string.Empty;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 1000f, mask))
        {

            IInteractable interactable = null;

            if (currentInteractionHover == null)
            {
                interactable = GameStateManager.Instance.TryGetInteractable(hit.transform.gameObject.GetInstanceID());
            }

            if (interactable != null && Vector3.Distance(cam.transform.position, hit.transform.position) < interactable.GetInteractableDistance())
            {
                currentInteractionHover = interactable;
                interactingWith = interactable.GetName();
            }
            else
            {
                currentInteractionHover = null;
                interactingWith = string.Empty;
            }
        }
    }
}
