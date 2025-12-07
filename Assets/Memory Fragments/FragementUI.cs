using UnityEngine;
using TMPro;

public class FragmentUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Open(string content)
    {
        gameObject.SetActive(true);
        text.text = content;
        Time.timeScale = 0f;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
