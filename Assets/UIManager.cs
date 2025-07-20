using UnityEngine;

public class UIManager : Singleton<UIManager>   
{
    [SerializeField] MenuCanvas gameMenu; 
    public void ShowPauseMenu(bool b)
    {
        Debug.Log("Show pause");
        gameMenu?.gameObject.SetActive(b);
    }
}
