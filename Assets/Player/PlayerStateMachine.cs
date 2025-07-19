using System.Collections.Generic;
using WeaponBobbing;
using UnityEngine;
using Sirenix.OdinInspector;


 
public class PlayerState_Dead : PlayerState
{

    public PlayerState_Dead(PlayerStateMachine psm) : base(psm)
    {

    }
    public override void Tick()
    {
        psm.RB.linearVelocity = Vector3.zero;
    }

    public override PlayerStateType GetStateType()
    {
        return PlayerStateType.Dead;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        psm.RB.linearVelocity = Vector3.zero;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}

public class PlayerState_Idle : PlayerState
{

    public PlayerState_Idle(PlayerStateMachine psm) : base(psm)
    {

    }
    public override void Tick()
    {
        psm.UpdatePlayerMove();
    }

    public override PlayerStateType GetStateType()
    {
        return PlayerStateType.Idle;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}

public class PlayerState_Crouch : PlayerState
{

    public PlayerState_Crouch(PlayerStateMachine psm) : base(psm)
    {

    }
    public override void Tick()
    {
        psm.UpdatePlayerMove();
    }

    public override PlayerStateType GetStateType()
    {
        return PlayerStateType.Crouch;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}

public class PlayerState_No_Input : PlayerState
{

    public PlayerState_No_Input(PlayerStateMachine psm) : base(psm)
    {

    }
    public override void Tick()
    {

    }

    public override PlayerStateType GetStateType()
    {
        return PlayerStateType.No_Input;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}

public class PlayerState_Walk : PlayerState
{

    public PlayerState_Walk(PlayerStateMachine psm) : base(psm)
    {

    }
    public override void Tick()
    {
        psm.UpdatePlayerMove();
    }

    public override PlayerStateType GetStateType()
    {
        return PlayerStateType.Walk;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}

public class PlayerState_Run: PlayerState
{

    public PlayerState_Run(PlayerStateMachine psm) : base(psm)
    {
        psm.UpdatePlayerMove();
    }
    public override void Tick()
    {

    }

    public override PlayerStateType GetStateType()
    {
        return PlayerStateType.Run;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}

public class PlayerState_Underwater : PlayerState
{

    public PlayerState_Underwater(PlayerStateMachine psm) : base(psm)
    {
        psm.UpdatePlayerMove();
    }
    public override void Tick()
    {

    }

    public override PlayerStateType GetStateType()
    {
        return PlayerStateType.Underwater;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}

 

public abstract class PlayerState
{

    protected PlayerStateMachine psm;

    public PlayerState(PlayerStateMachine _psm)
    {
        psm = _psm;
    }

    public abstract void Tick();

 

    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateExit() { }

    public abstract PlayerStateType GetStateType();

    

}

 



public class PlayerStateMachine : MonoBehaviour
{

    public Dictionary<PlayerStateType, PlayerState> stateDictionary = new Dictionary<PlayerStateType, PlayerState> ();

    [SerializeField] float currentMutationLevel, maxMutationLevel;

    [SerializeField] PlayerStateType currentStateType;

    PlayerState currentState;

    WeaponManager weaponManager;
    Player_Jump jump;
    Player_Controller controller;
    FPS_Controller fpsController;
    WeaponBob headBob;
    Rigidbody rb;
    public WeaponManager WeaponManager { get { return weaponManager; } set { weaponManager = value; } }
    public Player_Jump Jump { get { return jump; } set { jump = value; } }
    public Player_Controller Controller { get { return controller; } set { controller = value; } }
    public FPS_Controller FpsController { get { return fpsController; } set { fpsController = value; } }
    public Rigidbody RB { get { return rb; } set {  rb = value; } }    


    private void Awake()
    {
        weaponManager = GetComponentInChildren<WeaponManager>();
        jump = GetComponent<Player_Jump>();
        controller = GetComponent<Player_Controller>();
        fpsController = GetComponentInChildren<FPS_Controller>();
        headBob = GetComponentInChildren<WeaponBob>();  
        rb = GetComponentInChildren<Rigidbody>();   
    }
    private void Start()
    {
        InitializeStates();

     

        SetState(PlayerStateType.Idle);
    }

    public void OnPlayerSpawn(Quaternion rotation)
    {
        fpsController?.OnPlayerSpawn();
        transform.rotation = rotation;
    }

    void InitializeStates()
    {
        
       PlayerState_Idle playerIdle = new PlayerState_Idle(this);
       PlayerState_Dead playerDead = new PlayerState_Dead(this);
       PlayerState_Walk playerWalk = new PlayerState_Walk(this);
       PlayerState_Run playerRun = new PlayerState_Run(this);
       PlayerState_Crouch playerCrouch = new PlayerState_Crouch(this);
       PlayerState_Underwater playerUnderwater = new PlayerState_Underwater(this);
       PlayerState_No_Input playerNoInput = new PlayerState_No_Input(this);


       if (!stateDictionary.ContainsKey(PlayerStateType.Idle)) stateDictionary?.Add(PlayerStateType.Idle, playerIdle);

       if (!stateDictionary.ContainsKey(PlayerStateType.Dead)) stateDictionary?.Add(PlayerStateType.Dead, playerDead);

       if (!stateDictionary.ContainsKey(PlayerStateType.Walk)) stateDictionary?.Add(PlayerStateType.Walk, playerWalk);

       if (!stateDictionary.ContainsKey(PlayerStateType.Run)) stateDictionary?.Add(PlayerStateType.Run, playerRun);

       if (!stateDictionary.ContainsKey(PlayerStateType.Crouch)) stateDictionary?.Add(PlayerStateType.Crouch, playerCrouch);

       if (!stateDictionary.ContainsKey(PlayerStateType.Underwater)) stateDictionary?.Add(PlayerStateType.Underwater, playerUnderwater);

       if (!stateDictionary.ContainsKey(PlayerStateType.No_Input)) stateDictionary?.Add(PlayerStateType.No_Input, playerNoInput);

      
    }

    [Button]
    public void Kill()
   {
        if (currentStateType != PlayerStateType.Dead)
        {
            SetState(PlayerStateType.Dead);
            
            // New mutation
            // Reset player location
            // Reset player data
        }
   }

    private void Update()
    {

      
        currentState?.Tick();

       
        if (currentStateType != currentState?.GetStateType())
        {
            SetState(currentStateType);
        }

        if (Input.GetKeyDown(KeyCode.P)) TakeDamage(25f);

        
    }

    public void UpdatePlayerMove()
    {

        if (controller != null) controller.UpdatePlayerController();
        if (headBob != null) headBob.UpdateBob();
        if (weaponManager != null) weaponManager.UpdateWeapon();
        if (jump != null) jump.UpdateJump();
    }
    public void TakeDamage(float damage)
    {
        currentMutationLevel += damage;

        if (currentMutationLevel >= maxMutationLevel) Kill();
    }

    public void SetState(PlayerStateType stateType)
    {
        if (stateDictionary == null) return;

        currentState?.OnStateExit();

        stateDictionary.TryGetValue(stateType, out PlayerState newState);

        gameObject.name = "Player - " + newState?.ToString();
        currentState = newState;
        currentStateType = stateType;   

        newState?.OnStateEnter();



    }
}
