using UnityEngine;
using UnityEngine.InputSystem;


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

    public void UpdatePlayerInput(InputAction.CallbackContext context)
    {

        Debug.Log("Update playing input ");
        Vector2 moveInput = context.ReadValue<Vector2>();

          inputX = moveInput.x;
          inputZ = moveInput.y;
    }

    // Update is called once per frame
    public void UpdatePlayerController()
    {
        updateRigidbody = true;


   

        move = transform.TransformDirection(new Vector3(inputX, 0, inputZ).normalized);

        
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
