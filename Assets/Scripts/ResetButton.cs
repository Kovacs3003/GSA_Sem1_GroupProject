using UnityEngine;
using UnityEngine.SceneManagement;

public class HardResetButton : MonoBehaviour
{
    public void HardReset()
    {
        foreach (var go in Object.FindObjectsOfType<GameObject>())
        {
            if (go.scene.name == "DontDestroyOnLoad")
            {
                Destroy(go);
            }
        }

        SceneManager.LoadScene("Menu");
    }
}


