using UnityEngine;

public class DecisionButton : MonoBehaviour
{

    public DecisionType type;

    public enum DecisionType
    {
        Revenue,
        Avatar,
        Published,
        UnreleasedAd
    }


    public void MakeDecision(bool allow)
    {
        Debug.Log($"{type} Decision: {allow}");

        var gm = GameManager.Instance;

    switch (type)
        {
            case DecisionType.Revenue:
                gm.allowRevenue = allow;
                gm.revenueDecided = true;
                break;

            case DecisionType.Avatar:
                gm.allowAvatar = allow;
                gm.avatarDecided = true;
                break;

            case DecisionType.Published:
                gm.allowPublished = allow;
                gm.publishedDecided = true;
                break;

            case DecisionType.UnreleasedAd:
                gm.allowUnreleasedAd = allow;
                gm.unreleasedAdDecided = true;
                break;
        }
        
        gm.CheckEnding();
    }
}
