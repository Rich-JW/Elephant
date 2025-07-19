using UnityEngine;

public class SaveFile : MonoBehaviour
{
    [SerializeField] string fileName;
    [SerializeField] bool empty;

    public string GetFileName()
    {
        return fileName;
    }

    public bool IsEmpty()
    {
        return empty;
    }

    void Start()
    {
        
    }
}
