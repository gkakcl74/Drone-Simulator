using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleDroneController : MonoBehaviour
{
    Rigidbody rigidbody; // 드론의 Rigidbody 컴포넌트
    float up_down_axis, forward_backward_axis, right_left_axis; // 드론의 움직임에 사용되는 축 값
    float forward_backward_angle = 0, right_left_angle = 0; // 드론의 기울기 각도
    [SerializeField]
    float speed = 3.3f, angle = 25; // 드론의 속도와 최대 기울기 각도
    Animator animator; // 드론의 애니메이터 컴포넌트
    bool isOnGround = false; // 드론이 땅에 닿아 있는지 여부
    public float rotationSpeed = 500f; // 드론의 회전 속도
    float yaw = 0; // Y축 회전 값

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 초기화
        animator = GetComponent<Animator>(); // Animator 컴포넌트 초기화
    }

    private void Update()
    {
        Controlls(); // 드론의 조작 함수 호출

        // 기울기와 회전을 계산하여 적용
        Vector3 tilt = new Vector3(forward_backward_angle, yaw, -right_left_angle);
        transform.localRotation = Quaternion.Euler(tilt);

        // Y축 회전 처리
        if (Input.GetKey(KeyCode.LeftBracket)) // [ 키를 누르면 왼쪽으로 회전
        {
            yaw -= rotationSpeed * Time.deltaTime; // Y축 회전 값 감소
        }
        if (Input.GetKey(KeyCode.RightBracket)) // ] 키를 누르면 오른쪽으로 회전
        {
            yaw += rotationSpeed * Time.deltaTime; // Y축 회전 값 증가
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(0);
        }
        // 드론의 움직임에 힘을 추가
        rigidbody.AddRelativeForce(right_left_axis, up_down_axis, forward_backward_axis);
    }

    void Controlls()
    {
        // 위로 이동
        if (Input.GetKey(KeyCode.Q))
        {
            up_down_axis = 15 * speed; // 위로 이동하는 힘
            animator.SetBool("Fly", true); // 비행 애니메이션 활성화
            isOnGround = false; // 땅에 닿지 않음
        }
        // 아래로 이동
        else if (Input.GetKey(KeyCode.E))
        {
            up_down_axis = 1; // 아래로 이동하는 힘
      
        }
        else
        {
            up_down_axis = 9.81f; // 중력의 힘을 적용
        
        }

        // 앞으로 이동
        if (Input.GetKey(KeyCode.W))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, angle, Time.deltaTime); // 기울기 각도 보간
            forward_backward_axis = speed; // 앞으로 이동하는 힘
          
        }
        // 뒤로 이동
        else if (Input.GetKey(KeyCode.S))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, -angle, Time.deltaTime); // 기울기 각도 보간
            forward_backward_axis = -speed; // 뒤로 이동하는 힘
           
        }
        else
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, 0, Time.deltaTime); // 기울기 각도 보간
            forward_backward_axis = 0; // 이동력 0
        }

        // 오른쪽으로 이동
        if (Input.GetKey(KeyCode.D))
        {
            right_left_angle = Mathf.Lerp(right_left_angle, angle, Time.deltaTime); // 기울기 각도 보간
            right_left_axis = speed; // 오른쪽으로 이동하는 힘
         
        }
        // 왼쪽으로 이동
        else if (Input.GetKey(KeyCode.A))
        {
            right_left_angle = Mathf.Lerp(right_left_angle, -angle, Time.deltaTime); // 기울기 각도 보간
            right_left_axis = -speed; // 왼쪽으로 이동하는 힘
            
        }
        else
        {
            right_left_angle = Mathf.Lerp(right_left_angle, 0, Time.deltaTime); // 기울기 각도 보간
            right_left_axis = 0; // 이동력 0
        }

        // 방향키 조합에 따른 회전 및 이동 처리
        // 오른쪽 위
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, angle, Time.deltaTime); // 기울기 각도 보간
            right_left_angle = Mathf.Lerp(right_left_angle, angle, Time.deltaTime); // 기울기 각도 보간
            forward_backward_axis = 0.5f * speed; // 앞으로 이동하는 힘
            right_left_axis = 0.5f * speed; // 오른쪽으로 이동하는 힘
        }
        // 왼쪽 위
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, angle, Time.deltaTime); // 기울기 각도 보간
            right_left_angle = Mathf.Lerp(right_left_angle, -angle, Time.deltaTime); // 기울기 각도 보간
            forward_backward_axis = 0.5f * speed; // 앞으로 이동하는 힘
            right_left_axis = -0.5f * speed; // 왼쪽으로 이동하는 힘
        }
        // 뒤로 오른쪽
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, -angle, Time.deltaTime); // 기울기 각도 보간
            right_left_angle = Mathf.Lerp(right_left_angle, angle, Time.deltaTime); // 기울기 각도 보간
            forward_backward_axis = -0.5f * speed; // 뒤로 이동하는 힘
            right_left_axis = 0.5f * speed; // 오른쪽으로 이동하는 힘
        }
        // 뒤로 왼쪽
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, -angle, Time.deltaTime); // 기울기 각도 보간
            right_left_angle = Mathf.Lerp(right_left_angle, -angle, Time.deltaTime); // 기울기 각도 보간
            forward_backward_axis = -0.5f * speed; // 뒤로 이동하는 힘
            right_left_axis = -0.5f * speed; // 왼쪽으로 이동하는 힘
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 드론이 땅에 닿으면 isOnGround를 true로 설정
        if (collision.gameObject.tag == "ground")
            isOnGround = true;
    }
}
