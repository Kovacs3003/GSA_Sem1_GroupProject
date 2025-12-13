using System;
using System.Collections;
using UnityEngine;

public class GamePlay2ndCube : MonoBehaviour
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
    [SerializeField] private GameObject UI;

    private bool canRotate = true;
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

        if (Scene2LimitCamPos()) return;

        for (int i = 0, len = littleSceneCams.Length; i < len; ++i)
        {
            //var cam = littleSceneCams[i];
            //cam.localPosition += new Vector3(0, m_deltaY * moveYSpeed, 0);
            littleSceneCams[i].LookAt(littleSceneCenters[i].position + lookFaceOffset);
            //cam.LookAt(littleSceneCenters[i].position + lookFaceOffset);
        }

        LevelCondition();
    }


    private bool Scene2LimitCamPos()
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
    private void LevelCondition()
    {
        if (isProcessingLevel) return;

        switch (level)
        {
            case 0f:
                Level1();
                break;

            case 1f:
                Level2();  
                break;

           
            case 2f:
                Level3();
                break;

            case 3f:
                Level4();
                break;


        }
    }


    /// <summary>
    /// Level 1 trigger
    /// </summary>
    private void Level1()
    {
        float boxY = box.eulerAngles.y;

        if (Vector3.Distance(camMain.position, new Vector3(0f, 3.005f, -3.59f)) <= 0.2f &&
        Mathf.Abs(camMain.localEulerAngles.x - 22.20f) <= 1.2f &&
        boxY >= 106f && boxY <= 110f && level == 0)
    {
        Debug.Log("level 1");
        StartCoroutine(Scene2Level1Process());
    }

        Debug.Log($"[DEBUG] Cam Pos: {camMain.position} | Cam Rot X: {camMain.localEulerAngles.x} | Level: {level} | BoxY = {boxY}");
    }



    /// <summary>
    /// Level 1 process
    /// </summary>
    private IEnumerator Scene2Level1Process()
    {
        isProcessingLevel = true;  

        canRotate = false;
        level = 1f;

        ani.SetInteger("level", 1);
        yield return new WaitForSeconds(2f);

        UI.transform.Find("Level 1").gameObject.SetActive(true);

        ani.SetInteger("level", 0);
        yield return new WaitForSeconds(2f);

        canRotate = true;
        isProcessingLevel = false; 
    }


    // level 2 detect
    private void Level2()
    {
        float boxY = box.eulerAngles.y;
       
        if (Vector3.Distance(camMain.position, new Vector3(0f, 1.615f, -6.37f)) <= 0.45f  &&
        Mathf.Abs(camMain.localEulerAngles.x - 4.66f) <= 1.0f &&
        boxY >= 132f && boxY <= 136f && level == 1)
        {
            Debug.Log("level 2");
            StartCoroutine(Scene2Level2Process());
        }

        Debug.Log($"[DEBUG] Cam Pos: {camMain.position} | Cam Rot X: {camMain.localEulerAngles.x} | Level: {level} | BoxY = {boxY}");
    }

    private IEnumerator Scene2Level2Process()
    {
        isProcessingLevel = true;

        canRotate = false;
        level = 2f;

        ani.SetInteger("level", 2);
        yield return new WaitForSeconds(2f);

        UI.transform.Find("Level 2").gameObject.SetActive(true);

        ani.SetInteger("level", 0);
        yield return new WaitForSeconds(2f);

        canRotate = true;
        isProcessingLevel = false;
    }

    // level 3 detect
    private void Level3()
    {
        float boxY = box.eulerAngles.y;
       
        if (Vector3.Distance(camMain.position, new Vector3(0f, 3.72f, -2.17f)) <= 0.35f  &&
        Mathf.Abs(camMain.localEulerAngles.x - 37.93f) <= 1.5f &&
        boxY >= 191f && boxY <= 194f && level == 2)
        {
            Debug.Log("level 3");
            StartCoroutine(Scene2Level3Process());
        }

        Debug.Log($"[DEBUG] Cam Pos: {camMain.position} | Cam Rot X: {camMain.localEulerAngles.x} | Level: {level} | BoxY = {boxY}");
    }

    private IEnumerator Scene2Level3Process()
    {
        isProcessingLevel = true;

        canRotate = false;
        level = 3f;

        ani.SetInteger("level", 3);
        yield return new WaitForSeconds(2f);

        UI.transform.Find("Level 3").gameObject.SetActive(true);

        ani.SetInteger("level", 0);
        yield return new WaitForSeconds(2f);

        canRotate = true;
        isProcessingLevel = false;
    }

    // level 4 detect
    private void Level4()
    {
        float boxY = box.eulerAngles.y;
       
        if (Vector3.Distance(camMain.position, new Vector3(0f, 2.395f, -4.81f)) <= 0.20f  &&
        Mathf.Abs(camMain.localEulerAngles.x - 12.80f) <= 0.8f &&
        boxY >= 299f && boxY <= 302f && level == 3)
        {
            Debug.Log("level 4");
            StartCoroutine(Scene2Level4Process());
        }

        Debug.Log($"[DEBUG] Cam Pos: {camMain.position} | Cam Rot X: {camMain.localEulerAngles.x} | Level: {level} | BoxY = {boxY}");
    }

    private IEnumerator Scene2Level4Process()
    {
        isProcessingLevel = true;

        canRotate = false;
        level = 4f;

        ani.SetInteger("level", 4);
        yield return new WaitForSeconds(2f);

        UI.transform.Find("Level 4").gameObject.SetActive(true);

        ani.SetInteger("level", 0);
        yield return new WaitForSeconds(2f);

        canRotate = true;
        isProcessingLevel = false;
    }



}




