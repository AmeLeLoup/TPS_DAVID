using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MovementStateManager : MonoBehaviour
{ 
    
    
    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator anim;
    
    public float currentMoveSpeed;
    public float walkSpeed = 3f, walckbackSpeed = 2f;
    public float runSpeed = 7f, runbackSpeed = 5f;
    public float crouchSpeed = 2f, crouchbackSpeed = 1f;
    
    
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float horizontalInput, verticalInput;
    CharacterController _controller;
    

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 _spherePosition;

    [SerializeField] float gravity = -9.81f;
    private Vector3 _velocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        anim.SetFloat("horizontalInput", horizontalInput);
        anim.SetFloat("verticalInput", verticalInput);

        
        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        direction = transform.forward * verticalInput + transform.right * horizontalInput;
        _controller.Move(direction.normalized * currentMoveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        _spherePosition = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(_spherePosition, _controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) _velocity.y += gravity * Time.deltaTime;
        else if (_velocity.y < 0) _velocity.y = -2;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_spherePosition, _controller.radius - 0.05f);
    }
}
