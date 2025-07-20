using UnityEngine;
 


public class Player_Controller : MonoBehaviour
{
    [SerializeField] float walkSpeed;

    float inputX, inputZ;

    Rigidbody rb;
    Vector3 move;
    [SerializeField] float damping;

    [SerializeField] HeadBob bob;
    Vector3 targetVelocity, currentVelocity;

    bool updateRigidbody = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

   

    // Update is called once per frame
    public void UpdatePlayerController()
    {
        updateRigidbody = true;

        move = transform.TransformDirection(InputManager.Instance.Move());

        
            if (rb != null)
            {

                 targetVelocity = move * walkSpeed;
                 currentVelocity = Vector3.zero;
                
                currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, damping);

            
            }

        if (updateRigidbody == true) rb.MovePosition(rb.position + currentVelocity * Time.deltaTime);

        if (bob != null)
        {
            bob.UpdateBob();
        }


        updateRigidbody = false;


    }

 


}
