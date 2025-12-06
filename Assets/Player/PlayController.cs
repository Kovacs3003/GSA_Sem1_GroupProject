using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Animator animator;

    
    private float speed = 0.5f;

    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        if (dir != Vector3.zero)
        {
           
            transform.rotation = Quaternion.LookRotation(dir);

            
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        
        if (
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D)
        )
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }
}