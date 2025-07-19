using UnityEngine;

public class CubeTest : MonoBehaviour, IInteractable
{

    [SerializeField] string name;
    [SerializeField] float interactableDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RegisterInteractable(gameObject.GetInstanceID(), this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterInteractable(int key, IInteractable value)
    {
        Debug.Log("Registering!");
        GameStateManager.Instance.RegisterInteractable(key, value);
    }
    public string GetName()
    {
        return name;
    }

    public void Interact()
    {

    }

    public float GetInteractableDistance()
    {
        return interactableDistance;    
    }
}
