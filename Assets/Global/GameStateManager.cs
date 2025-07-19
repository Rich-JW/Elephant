using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameState
{

    GameStateManager gsm;

    
    public GameState(GameStateManager _gsm)
    {
        gsm = _gsm;
    }

    public abstract void Tick();

    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateExit()
    {

    }

    public abstract GameStateType GetStateType();
  
}

public class GameState_Play : GameState
{
    public GameState_Play(GameStateManager gsm) : base(gsm)
    {

    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override GameStateType GetStateType()
    {
        return GameStateType.Play;
    }

    public override void Tick()
    {
      
    }
}

public class GameState_Pause : GameState
{
    public GameState_Pause(GameStateManager gsm) : base(gsm)
    {

    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override GameStateType GetStateType()
    {
        return GameStateType.Pause;
    }

    public override void Tick()
    {

    }
}

public class GameState_No_Input: GameState
{
    public GameState_No_Input(GameStateManager gsm) : base(gsm)
    {

    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override GameStateType GetStateType()
    {
        return GameStateType.No_Input;
    }

    public override void Tick()
    {

    }
}

public class GameState_Init : GameState
{
    public GameState_Init(GameStateManager gsm) : base(gsm)
    {

    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override GameStateType GetStateType()
    {
        return GameStateType.Init;
    }

    public override void Tick()
    {

    }
}


public class GameStateManager : Singleton<GameStateManager>
{

    public Dictionary<GameStateType, GameState> states = new Dictionary<GameStateType, GameState>();    
    public Dictionary<int, IInteractable> interactables = new Dictionary<int, IInteractable>();

    [SerializeField] GameStateType currentGameStateType;
    private GameState currentGameState;

    [SerializeField] PlayerStateMachine player;
    [SerializeField] PlayerSpawnPoint currentSpawnPoint;

    [SerializeField] string currentSaveFile;

    public void UpdateSaveFile(string fileName)
    {
        currentSaveFile = fileName; 
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-initialize here if needed
        Debug.Log($"Scene loaded: {scene.name}");
      
        InitializePlayer();
        SetPlayerToSpawnPoint();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        InitializeStates();
 
        SetState(GameStateType.Play);

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    void InitializePlayer()
    {
        if (player == null) player = FindFirstObjectByType<PlayerStateMachine>();
    }

    void SetPlayerToSpawnPoint()
    {

        // In reality this would need to be loaded from ES3
        if (currentSpawnPoint == null) currentSpawnPoint = FindFirstObjectByType<PlayerSpawnPoint>();

        if (player == null || currentSpawnPoint == null) return; 


        player.transform.position = currentSpawnPoint.Position;
        player.transform.rotation = currentSpawnPoint.Rotation;

        player.OnPlayerSpawn(currentSpawnPoint.Rotation);
    }

    public void UpdatePlayerSpawnPoint(PlayerSpawnPoint spawnPoint)
    {
        currentSpawnPoint = spawnPoint; 
    }
  

    void InitializeStates()
    {
    
        if (!states.ContainsKey(GameStateType.Play))
        {
            GameState_Play play = new GameState_Play(this);
            states.Add(GameStateType.Play, play);
        }
        if (!states.ContainsKey(GameStateType.Pause))
        {
            GameState_Pause pause = new GameState_Pause(this);
            states.Add(GameStateType.Pause, pause);
        }
        if (!states.ContainsKey(GameStateType.No_Input))
        {
            GameState_No_Input noInput = new GameState_No_Input(this);
            states.Add(GameStateType.No_Input, noInput);
        }
        if (!states.ContainsKey(GameStateType.Init))
        {
            GameState_Init init = new GameState_Init(this);
            states.Add(GameStateType.Init, init);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentGameState?.Tick();

        if (currentGameStateType != currentGameState?.GetStateType())
        {
            SetState(currentGameStateType);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Save game in file:" + currentSaveFile);
        }
    }

    public void RegisterInteractable(int key, IInteractable value)
    {
        if (interactables != null)
        {
            if (!interactables.ContainsKey(key))
            {
                interactables.Add(key, value);
            }
        }
    }

    public IInteractable TryGetInteractable(int key)
    {
        if (interactables != null)
        {
            if (interactables.ContainsKey(key))
            {
       
                interactables.TryGetValue(key, out IInteractable interactable);
                return interactable;
            }

            
        }

        return null;
    }

    public void SetState(GameStateType stateType)
    {
        if (states == null) return;

        currentGameState?.OnStateExit();

        states.TryGetValue(stateType, out GameState newState);

        if (newState != null)
        {
            currentGameState = newState;
            currentGameStateType = stateType;   
        }

        gameObject.name = "Game State Manager - " + currentGameState.GetType().ToString();
        
        currentGameState?.OnStateEnter();

    }
}
