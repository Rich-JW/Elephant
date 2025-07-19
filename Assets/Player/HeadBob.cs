using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{

    public float walkingBobbingSpeed = 14f;
    public float idleBobbingSpeed = 3f;
    public float bobbingAmount = 0.05f;
    public float idleBobAmountY, idleBobAmountX = 0.02f;
   


    float defaultPosY, defaultPosX = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
    
        defaultPosY = transform.localPosition.y;
        defaultPosX = transform.localPosition.x;
    }

    public void UpdateBob()
    {
      
            //Player is moving
            timer += Time.deltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
            
     
    }

    private void Update()
    {
        timer += Time.deltaTime * idleBobbingSpeed;
        transform.localPosition = new Vector3(defaultPosX + Mathf.Sin(timer) * idleBobAmountX, defaultPosY + Mathf.Sin(timer) * idleBobAmountY, transform.localPosition.z);
    }
}
