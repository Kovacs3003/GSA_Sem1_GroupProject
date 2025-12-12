using UnityEngine;

public class RoadConnection : MonoBehaviour
{
    [Header("端点")]
    public Transform pointA;
    public Transform pointB;

    [Header("拼接后的道路")]
    public GameObject newRoad;

    [Header("判定参数(按需调节)")]
    public float screenThreshold = 40f;    //屏幕距离 < 40 像素
    public float worldMaxDistance = 5f;    //3D 世界距离 < 5米
    public float directionThreshold = 0.7f;//方向 dot > 0.7 视为方向一致

    private bool connected = false;

    void Start()
    {
        if (newRoad != null)
        {
            newRoad.SetActive(false);  //一开始隐藏道路
        }
    }

    void Update()
    {
        if (connected) return;     //防止重复触发
        if (!Camera.main) return;  //防止无相机报错

        //======================
        // ① 屏幕坐标
        //======================
        Vector3 A = Camera.main.WorldToScreenPoint(pointA.position);
        Vector3 B = Camera.main.WorldToScreenPoint(pointB.position);

        //点要在屏幕前方，否则距离没意义
        if (A.z < 0 || B.z < 0)
        {
            Debug.Log("点在相机后面，不做判定");
            return;
        }

        //屏幕距离
        float screenDist = Vector2.Distance(
            new Vector2(A.x, A.y),
            new Vector2(B.x, B.y)
        );
        Debug.Log("屏幕距离 = " + screenDist);


        //======================
        // ② 世界距离
        //======================
        float worldDist = Vector3.Distance(pointA.position, pointB.position);
        Debug.Log("世界距离 = " + worldDist);


        //======================
        // ③ 方向 dot
        //======================
        float dot = Vector3.Dot(pointA.forward, pointB.forward);
        Debug.Log("方向 dot = " + dot);


        //======================
        // 判定条件
        //======================

        if (screenDist > screenThreshold)
            return;

        if (worldDist > worldMaxDistance)
            return;

        if (dot < directionThreshold)
            return;


        //======================
        // 拼接成功
        //======================
        connected = true;
        newRoad.SetActive(true);
        Debug.Log(">>>>> 道路拼接成功！ <<<<<");
    }
}