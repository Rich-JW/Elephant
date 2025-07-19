using UnityEngine;

public class Player_Gravity : MonoBehaviour
{

    Vector3 currentGravityDirection;
    Rigidbody rb;

    public float GravityScale { get { return gravityScale; } }

    [SerializeField] float gravityScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGravityDirection = -Vector3.up;
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb != null) rb.AddForce(currentGravityDirection.normalized * gravityScale, ForceMode.Acceleration);   
    }
}
