using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncher : MonoBehaviour
{



  

     void TryLoadGame(SaveFile saveFile)
    {

        string saveFileName = saveFile.GetFileName();

        GameStateManager.Instance.UpdateSaveFile(saveFileName);

        if (saveFile.IsEmpty())
        {
            Debug.Log("Load new game");
            SceneManager.LoadScene("test");
        }
        else
        {
            Debug.Log("Load from: " + saveFileName);

            // string area = ES3.Load<string>("area", saveFile);
            // Load correct area
            SceneManager.LoadScene("test");

        }
    }

    public void StartGame(SaveFile saveFile)
    {
        TryLoadGame(saveFile);
    }
}
