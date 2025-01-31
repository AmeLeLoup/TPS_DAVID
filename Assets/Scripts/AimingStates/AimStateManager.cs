using UnityEngine;
using System.Collections;

public class AimStateManager : MonoBehaviour
{
    AimingBaseState currentState;
    public RifleIdleState Idle = new RifleIdleState();
    public AimState Aim = new AimState();
    
    [SerializeField] float mouseSense = 5;
    [SerializeField] Transform camFollowPos;
     private float xAxis, yAxis;

     [HideInInspector] public Animator anim;
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);
        
        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimingBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
