using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class AimStateManager : MonoBehaviour
{
    //State
    AimingBaseState currentState;
    
    public RifleIdleState Idle = new RifleIdleState();
    public AimState Aim = new AimState();
    
    //Aim
    [SerializeField] float mouseSense = 5;
    [SerializeField] Transform camFollowPos;
     private float xAxis, yAxis;

     //Animation
     [HideInInspector] public Animator anim;

     //Cinemachine
     [HideInInspector] public CinemachineVirtualCamera vCam;
     public float adsFov = 30f;
     [HideInInspector] public float hipFov;
     [HideInInspector] public float currentFov;
     public float fovSmoothSpeed = 10f;

    //RayCast
     public Transform aimPos;
     [HideInInspector] public Vector3 actualAimPos;
     [SerializeField] float aimSmoothSpeed = 20f;
     [SerializeField] LayerMask aimMask;
     
     
    void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(Idle);
    }

    
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov , fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            actualAimPos = hit.point;
        }
        
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
