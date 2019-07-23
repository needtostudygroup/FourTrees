using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         //플레이어 넘버
    public float m_Speed = 12f;            //이동속도
    public float m_TurnSpeed = 180f;       //회전속도

    
    private string m_MovementAxisName;     //앞뒤 축
    private string m_TurnAxisName;         //회전 축
    private Rigidbody m_Rigidbody;         //컴포넌트 담는거
    private float m_MovementInputValue;    //앞뒤
    private float m_TurnInputValue;        //회전


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>(); //컴포넌트 rigidbody 갖고오기, rigidbody는 게임 오브젝트가 물리엔진의 영향을 받게 만드는 기능
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false; //true일 경우 물리엔진에 의해서는 이동하지 않고 애니메이션이나 Transform컴포넌트를 통해서만 이동
        m_MovementInputValue = 0f; //초기화
        m_TurnInputValue = 0f; //초기화
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true; //물리엔진의 영향을 받지 않는다.
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
    }
    

    private void Update() //입력받는거
    {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName); //앞뒤
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName); //회전

    }

    private void FixedUpdate() //업데이트대로 실행
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