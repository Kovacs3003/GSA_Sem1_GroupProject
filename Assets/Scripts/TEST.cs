using UnityEngine;

public class ProjectionBridge : MonoBehaviour
{
    public Transform endpointA;
    public Transform endpointB;
    public GameObject bridge;

   
    public float screenAlignThreshold = 40f;

    public float bridgeShrink = 0.3f; // ±‹√‚Ωª¥Ì÷ÿµ˛

    void Update()
    {
        if (!Camera.main) return;

        Vector2 A = Camera.main.WorldToScreenPoint(endpointA.position);
        Vector2 B = Camera.main.WorldToScreenPoint(endpointB.position);

        // ”æıæ‡¿Î≈–∂®
        bool visuallyAligned = Vector2.Distance(A, B) < screenAlignThreshold;

        bridge.SetActive(visuallyAligned);

        if (visuallyAligned)
        {
            PlaceBridge();
        }
    }

    void PlaceBridge()
    {
        Vector3 posA = endpointA.position;
        Vector3 posB = endpointB.position;

        Vector3 mid = (posA + posB) / 2f;
        float length = Vector3.Distance(posA, posB) - bridgeShrink;

        bridge.transform.position = mid;
        bridge.transform.LookAt(posB);

        Vector3 scl = bridge.transform.localScale;
        scl.z = length;
        bridge.transform.localScale = scl;
    }
}