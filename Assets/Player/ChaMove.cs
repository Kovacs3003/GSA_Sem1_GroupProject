using UnityEngine;

public class ChaMove : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

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

        bool hasInput = move.sqrMagnitude > 0.01f;
        bool isRunning = hasInput && Input.GetKey(KeyCode.LeftShift);

        if (hasInput)
        {
            transform.LookAt(transform.position + move);
            float speed = isRunning ? runSpeed : walkSpeed;
            transform.position += move * speed * Time.deltaTime;
        }

        if (!hasInput)
        {
            anim.SetFloat("Speed", 0f);
        }
        else if (isRunning)
        {
            anim.SetFloat("Speed", 1f);
        }
        else
        {
            anim.SetFloat("Speed", 0.5f);
        }
    }
}