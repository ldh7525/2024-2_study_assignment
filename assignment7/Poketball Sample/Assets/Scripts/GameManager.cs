using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    UIManager MyUIManager;

    public GameObject BallPrefab;   // prefab of Ball

    // Constants for SetupBalls
    public static Vector3 StartPosition = new Vector3(0, 0, -6.35f);
    public static Quaternion StartRotation = Quaternion.Euler(0, 90, 90);
    const float BallRadius = 0.286f;
    const float RowSpacing = 0.02f;

    GameObject PlayerBall;
    GameObject CamObj;
    public GameObject mainCamera;

    const float CamSpeed = 3f;

    const float MinPower = 15f;
    const float PowerCoef = 1f;
    Vector3 worldPosition; // Update와 CamMove에서 공유할 필드
    Vector3 ballPos;

    void Awake()
    {
        // PlayerBall, CamObj, MyUIManager를 얻어온다.
        PlayerBall = GameObject.Find("PlayerBall");
        CamObj = GameObject.Find("Main Camera");
        MyUIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Start()
    {
        SetupBalls();
    }

    // Update is called once per frame
    void Update()
    {
        // 좌클릭시 raycast하여 클릭 위치로 ShootBallTo 한다.
        // ---------- TODO ----------
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPosition = Input.mousePosition; // 클릭된 화면 좌표를 가져옴
            screenPosition.z = 15f; 
            worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);            // 월드 좌표로 변환
            ballPos = PlayerBall.transform.position;
            ShootBallTo(worldPosition);
        }
        // --------------------
    }


    void LateUpdate()
    {
        CamMove();
    }

    void SetupBalls()
    {
        // 15개의 공을 삼각형 형태로 배치한다.
        // 가장 앞쪽 공의 위치는 StartPosition이며, 공의 Rotation은 StartRotation이다.
        // 각 공은 RowSpacing만큼의 간격을 가진다.
        // 각 공의 이름은 {index}이며, 아래 함수로 index에 맞는 Material을 적용시킨다.
        // Obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ball_1");
        // ---------- TODO ---------- 
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col <= row; col++)
            {
                //x좌 z좌 설정
                float xOffset = col * (BallRadius * 2 + RowSpacing) - row * (BallRadius + RowSpacing);
                float zOffset = -row * ((BallRadius + RowSpacing/2)* Mathf.Sqrt(3));

                // 공의 위치 계산
                Vector3 position = StartPosition + new Vector3(xOffset, 0, zOffset); Quaternion rotation = StartRotation;
                GameObject ball = Instantiate(BallPrefab, position, rotation);
                ball.name = (row * (row + 1) / 2 + col + 1).ToString();
                ball.GetComponent<MeshRenderer>().material = Resources.Load<Material>($"Materials/ball_{row * (row + 1) / 2 + col + 1}");
            }
        }
    }
    void CamMove()
    {
        ballPos = PlayerBall.transform.position;
        Vector3 camTargetPos = ballPos;
        camTargetPos.y = 15f;
        CamObj.transform.position = Vector3.Lerp(CamObj.transform.position, camTargetPos, Time.deltaTime*CamSpeed);
    }


    float CalcPower(Vector3 displacement)
    {
        return MinPower + displacement.magnitude * PowerCoef;
    }

    void ShootBallTo(Vector3 targetPos)
    {
        // targetPos의 위치로 공을 발사한다.
        // 힘은 CalcPower 함수로 계산하고, y축 방향 힘은 0으로 한다.
        // ForceMode.Impulse를 사용한다.
        // ---------- TODO ---------- 
        Rigidbody rb = PlayerBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 force = (targetPos - PlayerBall.transform.position).normalized * CalcPower(targetPos - PlayerBall.transform.position);
            force.y = 0;
            rb.AddForce(force, ForceMode.Impulse);
        }
        // -------------------- 
    }

    // When ball falls
    public void Fall(string ballName)
    {
        // "{ballName} falls"을 1초간 띄운다.
        // ---------- TODO ---------- 
        MyUIManager.DisplayText($"{ballName} falls", 1f);
    }

}
