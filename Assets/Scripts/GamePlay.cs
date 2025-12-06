using System;
using System.Collections;
using UnityEngine;

public class GamePlay : MonoBehaviour
{

    [SerializeField] private Transform[] littleSceneCams;
    [SerializeField] private Transform[] littleSceneCenters;
    [SerializeField] private Transform box;
    [SerializeField] private Transform camMain;

    private float m_deltaX;
    private float m_deltaY;
    [SerializeField] private float m_rotateSpeed;

    [SerializeField] private Vector3 lookOffset = new Vector3(0, 1, 0);
    [SerializeField] private Vector3 lookFaceOffset = new Vector3(0, -0.5f, 0);

    [SerializeField] private float moveYSpeed = 0.05f;
    [SerializeField] private float moveZSpeed = 0.1f;

    private float level = 0f;


    [SerializeField] private Animator ani;
    [SerializeField] private GameObject RealObject;

    private bool canRotate = true;

    // ⭐ 新增：防止连续触发
    private bool isProcessingLevel = false;


    void Update()
    {
        if (!canRotate || !Input.GetMouseButton(0)) return;

        m_deltaX = Input.GetAxis("Mouse X");
        m_deltaY = Input.GetAxis("Mouse Y");

        if (m_deltaX == 0 && m_deltaY == 0) return;

        box.Rotate(Vector3.up, -m_deltaX);

        for (int i = 0, len = littleSceneCenters.Length; i < len; ++i)
        {
            littleSceneCenters[i].Rotate(Vector3.up, -m_deltaX * m_rotateSpeed);
        }

        camMain.localPosition += new Vector3(0, m_deltaY * moveYSpeed, m_deltaY * moveZSpeed);

        if (LimitCamPos()) return;

        for (int i = 0, len = littleSceneCams.Length; i < len; ++i)
        {
            //var cam = littleSceneCams[i];
            //cam.localPosition += new Vector3(0, m_deltaY * moveYSpeed, 0);
            littleSceneCams[i].LookAt(littleSceneCenters[i].position + lookFaceOffset);
            //cam.LookAt(littleSceneCenters[i].position + lookFaceOffset);
        }

        CheckLevelCondition();
    }


    private bool LimitCamPos()
    {
        var curPos = camMain.position;
        var isOut = false;

        if (curPos.z < -7f) { curPos.z = -7f; isOut = true; }
        if (curPos.z > 1.2f) { curPos.z = 1.2f; isOut = true; }
        if (curPos.y > 4.8f) { curPos.y = 4.8f; isOut = true; }
        if (curPos.y < 1.3f) { curPos.y = 1.3f; isOut = true; }

        camMain.position = curPos;
        camMain.LookAt(box.position + lookOffset);

        return isOut;
    }

    /// <summary>
    /// detection
    /// detect different levels
    /// </summary>
    private void CheckLevelCondition()
    {
        if (isProcessingLevel) return;

        switch (level)
        {
            case 0f:
                CheckLevel1();
                break;

            case 1f:
                CheckLevel2();  
                break;

            // 未来你还可以继续添加：
            // case 2f:
            //     CheckLevel3();
            //     break;
        }
    }


    /// <summary>
    /// Level 1 trigger
    /// </summary>
    private void CheckLevel1()
    {
        float boxY = box.eulerAngles.y;

        if (Vector3.Distance(camMain.position, new Vector3(0f, 2.272501f, -5.07000f)) <= 0.1f &&
        Mathf.Abs(camMain.localEulerAngles.x - 11.22795f) <= 1f &&
        boxY >= 35f && boxY <= 50f)
    {
        Debug.Log("level 1");
        StartCoroutine(NextLevel());
    }

    if (Vector3.Distance(camMain.position, new Vector3(0f, 2.272501f, -5.07000f)) <= 2f)
    {
        Debug.Log($"[DEBUG] Cam Pos: {camMain.position} | Cam Rot X: {camMain.localEulerAngles.x} | Level: {level} | BoxY = {boxY}");
    }
    }



    /// <summary>
    /// Level 1 process
    /// </summary>
    private IEnumerator NextLevel()
    {
        isProcessingLevel = true;  

        canRotate = false;
        level = 1f;

        ani.SetInteger("level", 1);
        yield return new WaitForSeconds(2f);

        RealObject.SetActive(true);

        ani.SetInteger("level", 0);
        yield return new WaitForSeconds(2f);

        canRotate = true;
        isProcessingLevel = false; 
    }


    // level 2 detect
    private void CheckLevel2()
    {
       
        if (Vector3.Distance(camMain.position, new Vector3(0f, 3f, -4f)) <= 0.5f)
        {
            Debug.Log("level 2");
            StartCoroutine(Level2Process());
        }
    }

    private IEnumerator Level2Process()
    {
        isProcessingLevel = true;

        canRotate = false;
        level = 2f;

        ani.SetInteger("level", 2);
        yield return new WaitForSeconds(2f);

        // secondObject.SetActive(true);

        ani.SetInteger("level", 0);
        yield return new WaitForSeconds(2f);

        canRotate = true;
        isProcessingLevel = false;
    }

}
