using UnityEngine;
using System.Collections.Generic;
using System;

 
public class InputManager : Singleton<InputManager>
{

    public Dictionary<string, KeyCode> bindings = new Dictionary<string, KeyCode>();
    public Dictionary<string, KeyCode> defaultBindings = new Dictionary<string, KeyCode>();



    [SerializeField] InputType inputType = InputType.Keyboard;


    bool unfired = true;   

    private void Start()
    {
        DontDestroyOnLoad(this);
        InitializeDefaultBindings();
    
    }

    void InitializeDefaultBindings()
    {

     

        if (!defaultBindings.ContainsKey("Shoot_Key"))  defaultBindings.Add("Shoot_Key", KeyCode.Mouse0);
        if (!defaultBindings.ContainsKey("Jump_Key")) defaultBindings.Add("Jump_Key", KeyCode.Space);
        if (!defaultBindings.ContainsKey("Jump_Pad")) defaultBindings.Add("Jump_Pad", KeyCode.JoystickButton0);
    }

    private void LateUpdate()
    {
        if ((Input.GetAxis("Shoot_Pad") < 0.2f)) unfired = true;
    }

    public bool IsPressed(string keyString)
    {
 
        defaultBindings.TryGetValue(keyString, out KeyCode keyCode);

        bool isPressed = Input.GetKeyDown(keyCode) || (Input.GetAxis("Shoot_Pad") > 0.2f && unfired);

        unfired = false;

        return isPressed;
    }

    public Vector3 Move()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    public Vector2 GetMouseInput()
    {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
    }

    public void UpdateInputType(string inputTypeString)
    {
        foreach (InputType value in Enum.GetValues(typeof(InputType)))
        {
            if (value.ToString() == inputTypeString) inputType = value; 
        }
    }

 


    


}
