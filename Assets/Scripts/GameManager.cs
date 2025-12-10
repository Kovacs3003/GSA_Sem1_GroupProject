using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Decisions")]
    public bool allowRevenue;
    public bool allowAvatar;
    public bool allowPublished;
    public bool allowUnreleasedAd;

    [Header("Decision State")]
    public bool revenueDecided;
    public bool avatarDecided;
    public bool publishedDecided;
    public bool unreleasedAdDecided;

    [Header("Ending UI Panels")]
    public GameObject endingAPanel;
    public GameObject endingBPanel;
    public GameObject endingCPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AllDecisionsMade()
    {
        return revenueDecided &&
               avatarDecided &&
               publishedDecided &&
               unreleasedAdDecided;
    }

    public void CheckEnding()
    {
        if (!AllDecisionsMade())
            return; 

        HideAllEndings();

        if (allowAvatar || allowUnreleasedAd)
        {
            endingAPanel.SetActive(true);
        }
        else if (allowRevenue || allowPublished)
        {
            endingBPanel.SetActive(true);
        }
        else
        {
            endingCPanel.SetActive(true);
        }
    }

    void HideAllEndings()
    {
        endingAPanel.SetActive(false);
        endingBPanel.SetActive(false);
        endingCPanel.SetActive(false);
    }
}
