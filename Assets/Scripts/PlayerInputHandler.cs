using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Movement";

    private InputAction moveAction;

    public Vector2 MoveInput { get; private set; }

    [Header("Player Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        var actionMap = playerControls.FindActionMap(actionMapName);

        if (actionMap != null)
        {
            moveAction = actionMap.FindAction(move);
            if (moveAction != null)
            {
                moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
                moveAction.canceled += context => MoveInput = Vector2.zero;
            }
            else
            {
                Debug.LogError($"Action '{move}' not found in Action Map '{actionMapName}'");
            }
        }
        else
        {
            Debug.LogError($"Action Map '{actionMapName}' not found in Input Action Asset.");
        }
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void Update()
    {
        MovePlayer();
        UpdateAnimation();
        FlipSprite(); 
    }

    private void MovePlayer()
    {
        if (Mathf.Abs(MoveInput.x) > Mathf.Abs(MoveInput.y))
        {
            
            MoveInput = new Vector2(MoveInput.x, 0);
        }
        else
        {
            MoveInput = new Vector2(0, MoveInput.y);
        }
        Vector2 movement = MoveInput * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("MoveX", MoveInput.x);
        animator.SetFloat("MoveY", MoveInput.y);

        bool isMoving = MoveInput != Vector2.zero;
        animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    private void FlipSprite()
    {
        if (MoveInput.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (MoveInput.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
