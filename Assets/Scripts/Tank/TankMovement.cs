using UnityEngine;

public class TankMovement : MonoBehaviour
{   
    //속도
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       

    //입력 축
    private string m_MovementAxisName;     
    private string m_TurnAxisName;
    
    //물리엔진 컴포넌트 값 true-끔 false-킴
    private Rigidbody m_Rigidbody;
    
    //전후방, 회전 입력값
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical";
        m_TurnAxisName = "Horizontal";
    }

    //앞,뒤, 회전 입력받는거
    private void Update() 
    {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

    }
    //업데이트대로 실행
    private void FixedUpdate()
    {
        Move();
        Turn();
    }


    private void Move()
    {
        //탱크 움직이는 벡터값 입력
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        //rigidbody를 옮겨준다
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        //탱크 회전시키는 값 입력
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        //회전시켜줘
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        //rigidbody에도 적용시커줘
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}