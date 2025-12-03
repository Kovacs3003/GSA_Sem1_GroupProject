using UnityEngine;

public class RoadPuzzleChecker : MonoBehaviour
{
    public Transform A_End;      // 碎片 A 的结束点
    public Transform B_Start;    // 碎片 B 的起始点
    public float screenThreshold = 15f;  // 屏幕距离阈值（像素）

    public GameObject ghostRoads; // 所有的 stencil 碎片
    public GameObject realRoad;   // 最终真实道路

    private bool solved = false;

    void Update()
    {
        if (solved) return;

        Vector3 screenA = Camera.main.WorldToScreenPoint(A_End.position);
        Vector3 screenB = Camera.main.WorldToScreenPoint(B_Start.position);

        // Z 若为负值说明物体在摄像机后面
        if (screenA.z < 0 || screenB.z < 0) return;

        float distance = Vector2.Distance(
            new Vector2(screenA.x, screenA.y),
            new Vector2(screenB.x, screenB.y)
        );

        // 判断是否在屏幕上 "重叠"
        if (distance < screenThreshold)
        {
            PuzzleSolved();
        }
    }

    void PuzzleSolved()
    {
        solved = true;

        ghostRoads.SetActive(false);  // 关掉 stencil 道路碎片
        realRoad.SetActive(true);     // 打开真实道路
    }
}
