using UnityEngine;

public interface IInteractable 
{
    public string GetName();

    public void Interact();

    public void RegisterInteractable(int key, IInteractable value);

    public float GetInteractableDistance();
}
