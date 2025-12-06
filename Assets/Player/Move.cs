using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaMove : MonoBehaviour
{
    public float speed = 3f;

    Animator anim;
    Vector3 move;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = new Vector3(x, 0, z);

        if (move != Vector3.zero)
        {
            transform.LookAt(transform.position + move);
        }

        transform.position += move * speed * Time.deltaTime;

        UpdateAnim();
    }

    void UpdateAnim()
    {
        anim.SetFloat("Speed", move.magnitude);
    }
}
