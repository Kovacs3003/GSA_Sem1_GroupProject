using UnityEngine;

public class ChuanTouMoveWithAnim : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    public float realHeigth = 1.5f;
    public float bodyRadius = 0.5f;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    Vector3 AntiPenetrationPos(Vector3 aimPos)
    {
        if (transform.position == aimPos)
            return transform.position;

        bool couldHmove = true;
        bool couldVmove = true;

        Vector3 dirH = new Vector3(aimPos.x - transform.position.x, 0, 0).normalized;
        Vector3 dirV = new Vector3(0, 0, aimPos.z - transform.position.z).normalized;

        Vector3 bodyPos = transform.position;
        float tempPercent = 3f;
        bodyPos.y += realHeigth / tempPercent;

        if (dirH != Vector3.zero && Physics.Raycast(bodyPos, dirH, bodyRadius))
            couldHmove = false;
        else if (Physics.Raycast(bodyPos + dirH * bodyRadius, Vector3.up, realHeigth / tempPercent))
            couldHmove = false;

        if (dirV != Vector3.zero && Physics.Raycast(bodyPos, dirV, bodyRadius))
            couldVmove = false;
        else if (Physics.Raycast(bodyPos + dirV * bodyRadius, Vector3.up, realHeigth / tempPercent))
            couldVmove = false;

        Vector3 finalPos = aimPos;
        if (!couldHmove) finalPos.x = transform.position.x;
        if (!couldVmove) finalPos.z = transform.position.z;

        return finalPos;
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(x, 0, z);
        bool hasInput = input.sqrMagnitude > 0.01f;

        bool isRunning = hasInput && Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;

        if (hasInput)
        {
            transform.forward = input.normalized;
        }

        Vector3 aimPos = transform.position + input.normalized * speed * Time.deltaTime;
        transform.position = AntiPenetrationPos(aimPos);

        // ===== ¶¯»­ =====
        if (!hasInput)
        {
            anim.SetFloat("Speed", 0f);       // Idle
        }
        else if (isRunning)
        {
            anim.SetFloat("Speed", 1f);       // Run
        }
        else
        {
            anim.SetFloat("Speed", 0.5f);     // Walk
        }
    }
}