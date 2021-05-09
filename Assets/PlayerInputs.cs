using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;



public class PlayerInputs : MonoBehaviour
{
    private PlayableDirector director;

    private GameController gameController;

    private static PlayerControls controls;

    static private InputActionAsset inputActions;

    private static ControlsOverlay controlsOverlay;

    private static float moveInput;

    private TrackAsset moveLeftTrack;

    private TrackAsset moveRightTrack;

    private TrackAsset jumpTrack;
    
    private TrackAsset slideTrack;

    private TimelineClip currentMoveLeftClip;

    private TimelineClip currentMoveRightClip;

    private static float movePressed;

    public float timer;

    public bool isPaused;

    public bool blueScreen;

    [SerializeField] TMPro.TMP_Text timerText;

    [SerializeField] GameObject pauseMenu;

    private void Awake()
    {
        director = GameObject.Find("Player").GetComponent<PlayableDirector>();

        gameController = FindObjectOfType<GameController>();

        controls = new PlayerControls();

        inputActions = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;

        TimelineAsset replay = (TimelineAsset)GameObject.Find("Player").GetComponent<PlayableDirector>().playableAsset;
        
        moveLeftTrack = replay.GetOutputTrack(1);

        moveRightTrack = replay.GetOutputTrack(2);

        jumpTrack = replay.GetOutputTrack(3);

        slideTrack = replay.GetOutputTrack(4);
        
        controlsOverlay = FindObjectOfType<ControlsOverlay>();
    }

    private void Start()
    {
        controls.Enable();

        isPaused = false;

        blueScreen = false;
    }

    private void Update()
    {   
        if(FindObjectOfType<PlayerInputs>().isPaused)
        {
            return;
        }

        if(timer < gameController.currentSequenceDuration && !gameController.inSegmentChange && !gameController.pressAnyKeyToContinue)
        {
            timer += Time.deltaTime;

            timerText.text = (gameController.currentSequenceDuration - timer).ToString("#0.00");

            if(gameController.inInputPhase)
            {
                timerText.color = Color.green;   
            }
            else
            {
                timerText.color = Color.red;
            }
        }
        else if(gameController.inSegmentChange)
        {
            timerText.text = "Pending...";
        }
        else if(gameController.pressAnyKeyToContinue)
        {
            timerText.color = Color.green;
            
            timerText.text = "Press Any Key";
        }
    }

    public void FinaliseClips()
    {
        if(currentMoveLeftClip != null)
        {
            currentMoveLeftClip.duration = gameController.currentSequenceDuration - ((gameController.currentSequenceDuration - (gameController.currentSequenceDuration - currentMoveLeftClip.start)));
            
            currentMoveLeftClip = null;
        }
        
        if(currentMoveRightClip != null)
        {
            currentMoveRightClip.duration = gameController.currentSequenceDuration - ((gameController.currentSequenceDuration - (gameController.currentSequenceDuration - currentMoveRightClip.start)));
            
            currentMoveRightClip = null;
        }
    }

    public void ContinueGamePerformed(InputAction.CallbackContext context)
    {
        if(FindObjectOfType<PlayerInputs>().isPaused)
        {
            if(blueScreen)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }


            return;
        }

        if(gameController.pressAnyKeyToContinue)
        {
            gameController.ContinueGame();
        }
    }

    public void JumpPerformed(InputAction.CallbackContext context)
    {
        if(!gameController.inInputPhase)
        {
            return;
        }

        
        if(FindObjectOfType<PlayerInputs>().isPaused)
        {
            return;
        }

        if(context.action.phase == InputActionPhase.Started)
        {
            TimelineClip clip = jumpTrack.CreateDefaultClip();

            clip.duration = 0.1f;

            clip.start = timer;

            controlsOverlay.JumpActionCreated(); 
        }
    }

    public void SlidePerformed(InputAction.CallbackContext context)
    {
        if(!gameController.inInputPhase)
        {
            return;
        }

        
        if(FindObjectOfType<PlayerInputs>().isPaused)
        {
            return;
        }

        if(context.action.phase == InputActionPhase.Started)
        {
            TimelineClip clip = slideTrack.CreateDefaultClip();

            clip.duration = 0.1f;

            clip.start = timer;

            controlsOverlay.SlideActionCreated(); 
        }
    }
    
    public void LeftPerformed(InputAction.CallbackContext context)
    {
        if(!gameController.inInputPhase)
        {
            return;
        }

        
        if(FindObjectOfType<PlayerInputs>().isPaused)
        {
            return;
        }

        if(context.action.phase == InputActionPhase.Started)
        {
            currentMoveLeftClip = moveLeftTrack.CreateDefaultClip();

            currentMoveLeftClip.start = timer;

            movePressed = 1f;
        }
        else if(context.action.phase == InputActionPhase.Canceled)
        {
            if(currentMoveLeftClip == null && gameController.inInputPhase)
            {
                currentMoveLeftClip = moveLeftTrack.CreateDefaultClip();

                currentMoveLeftClip.start = 0;
            }

            movePressed = 0f;

            currentMoveLeftClip.duration = gameController.currentSequenceDuration - ((gameController.currentSequenceDuration - (gameController.currentSequenceDuration - currentMoveLeftClip.start)) + (gameController.currentSequenceDuration - timer));
        
            currentMoveLeftClip = null;
        }
    }
    
    public void RightPerformed(InputAction.CallbackContext context)
    {
        if(!gameController.inInputPhase)
        {
            return;   
        }

        
        if(FindObjectOfType<PlayerInputs>().isPaused)
        {
            return;
        }

        if(context.action.phase == InputActionPhase.Started)
        {
            currentMoveRightClip = moveRightTrack.CreateDefaultClip();
            
            currentMoveRightClip.start = timer;

            movePressed = -1f;
        }
        else if(context.action.phase == InputActionPhase.Canceled)
        {
            if(currentMoveRightClip == null && gameController.inInputPhase)
            {
                currentMoveRightClip = moveRightTrack.CreateDefaultClip();

                currentMoveRightClip.start = 0;
            }

            movePressed = 0f;

            currentMoveRightClip.duration = gameController.currentSequenceDuration - ((gameController.currentSequenceDuration - (gameController.currentSequenceDuration - currentMoveRightClip.start)) + (gameController.currentSequenceDuration - timer));

            currentMoveRightClip = null;    
        }
    }

    public void PausePerformed(InputAction.CallbackContext context)
    {
        if(FindObjectOfType<PlayerInputs>().isPaused)
        {
            return;
        }

        isPaused = true;
        pauseMenu.SetActive(true);
    }

    public static float GetMoveInput()
    {
        if(Mathf.Abs(moveInput) > 0.01f)
        {
            return moveInput;           
        }
        else
        {
            return 0f;
        }
    }

    public static float GetMovePressed()
    {
        return movePressed;
    }

    public static InputActionAsset GetInputActions()
    {
        return inputActions;
    }

    public static void SetMoveInput(float moveInput)
    {
        PlayerInputs.moveInput = moveInput;
    }
}
