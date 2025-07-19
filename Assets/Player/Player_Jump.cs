using System.Collections;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{

    public float jumpHeight, initialJumpDuration, fallDuration;
    public float jumpMultiplier = 2f;
    public float fallMultiplier = 3f;
    public float hangtime;

    Player_Gravity gravity;

    public float airControlMultiplier;

    float currentGravityMultiplier;

    [SerializeField] bool grounded;
    Rigidbody rb;

    [SerializeField] AnimationCurve jumpCurve, fallCurve;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity = GetComponent<Player_Gravity>();   
    }
    public void UpdateJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            StopAllCoroutines();
            grounded = false;   
            StartCoroutine(Jump());
        }

        if (!grounded)
        {
          //  rb.linearVelocity += new Vector3(Input.GetAxisRaw("Horizontal") * -airControlMultiplier, 0, Input.GetAxisRaw("Vertical") * -airControlMultiplier);
        }
    
    }

    IEnumerator Jump()
    {
        float timer = 0f;

        // Turn off normal gravity
        rb.useGravity = false;

        Vector3 startPos = transform.position;

        // --- ASCENT ---
        while (timer < initialJumpDuration)
        {
            timer += Time.deltaTime;
            float curveValue = jumpCurve.Evaluate(timer / initialJumpDuration); // normalized 0-1
            float targetY = startPos.y + (jumpHeight * curveValue);
            Vector3 pos = rb.position;
            pos.y = targetY;

            rb.MovePosition(pos);

            // If you hit ceiling, break
            // Optional: do raycast here
            yield return new WaitForFixedUpdate();
        }

 

        // --- DESCENT ---
        timer = 0f;
        Vector3 peakPos = transform.position;

        while (timer < fallDuration)
        {
            timer += Time.deltaTime;
            float curveValue = fallCurve.Evaluate(timer / fallDuration); // normalized 0-1
            float targetY = Mathf.Lerp(startPos.y, peakPos.y, curveValue);
            Vector3 pos = rb.position;
            pos.y = targetY;

            rb.MovePosition(pos);

            // If you land early, break
            yield return new WaitForFixedUpdate();
        }

        rb.useGravity = true;
    }


    public void OnGrounded()
    {
        grounded = true;
    }
}
