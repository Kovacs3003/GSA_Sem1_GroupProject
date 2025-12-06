using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 3f;
    public float tuenSpeed   = 10f;

    Animator anim;
    Rigidbody rigid;

    Vector3 move;

    float forwardAmount;
    float turnAmount;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = new Vector3(x, 0, z);

        Vector3 localMove = transform.InverseTransformVector(move);

        forwardAmount = localMove.z;
        turnAmount = Mathf.Atan2(localMove.x, localMove.z);

        UpdateAnim();
    }

    private void FixedUpdate()
    {
        rigid.angularVelocity = forwardAmount * transform.forward * speed;
        rigid.MoveRotation(rigid.rotation*Quaternion.Euler(0,turnAmount*tuenSpeed,0 ));
    }

    void UpdateAnim()
    {
        anim.SetFloat("Speed", move.magnitude);
    }
}
