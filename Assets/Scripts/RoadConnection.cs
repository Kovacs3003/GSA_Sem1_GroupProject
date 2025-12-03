using UnityEngine;

public class RoadConnection : MonoBehaviour
{
    [Header("基础引用")]
    public Camera mainCamera;       // 用来做屏幕坐标转换的相机
    public Transform pointA;        // 道路端点 A
    public Transform pointB;        // 道路端点 B

    [Header("检测参数")]
    public float screenThreshold = 50f;  // 两点在屏幕上的最大距离（像素），小于这个就算拼接成功

    [Header("拼接后的内容")]
    public GameObject newRoad;      // 拼接成功后要激活的那条路/桥

    private bool isConnected = false;   // 是否已经拼接成功过

    void Start()
    {
        // 如果你忘了拖相机，就自动用主相机
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // 确保新路一开始是隐藏的
        if (newRoad != null)
        {
            newRoad.SetActive(false);
        }
    }

    void Update()
    {
        // 已经连过一次了就不再检测（防止重复触发）
        if (isConnected) return;

        if (CheckConnection())
        {
            OnConnected();
        }
    }

    // 核心检测逻辑：两个端点在屏幕上是否足够接近
    bool CheckConnection()
    {
        if (pointA == null || pointB == null || mainCamera == null)
            return false;

        // 把 3D 世界坐标转换为屏幕坐标
        Vector3 screenA = mainCamera.WorldToScreenPoint(pointA.position);
        Vector3 screenB = mainCamera.WorldToScreenPoint(pointB.position);

        // 如果有一个点在相机后面（z < 0），说明没在视野中，直接失败
        if (screenA.z < 0 || screenB.z < 0)
            return false;

        // 计算屏幕上的距离（只看 x、y）
        float dist = Vector2.Distance(
            new Vector2(screenA.x, screenA.y),
            new Vector2(screenB.x, screenB.y)
        );

        // Debug 看看数值，用来调阈值
        // Debug.Log("Screen distance: " + dist);

        // 小于阈值就认为“在玩家视角下已经对齐”
        return dist <= screenThreshold;
    }

    // 拼接成功时执行的逻辑
    void OnConnected()
    {
        isConnected = true;

        if (newRoad != null)
        {
            newRoad.SetActive(true);   // 激活桥/新路
        }

        Debug.Log("Road Connected! 已经成功拼接道路！");
    }
}