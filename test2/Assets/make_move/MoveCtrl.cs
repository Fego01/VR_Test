using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour {

	public enum MoveType
    {
        WAY_POINT,
        LOOK_AT,
        DAYDREAM
    }

    public MoveType moveType = MoveType.WAY_POINT; // 이동 방식
    public float speed = 1.0f;                     // 이동 속도
    public float damping = 3.0f;                   // 회전 속도를 조절할 계수
    public GameObject asdasd;

    private CharacterController Body;

    Transform asdtr;
    private Transform tr;
    private Transform camTr;
    private CharacterController cc;
    private Transform[] points;                    // 웨이포인트를 저장할 배열
    private int nextIdx = 1;                       // 다음에 이동해야 할 위치 변수

    public static bool isStopped = false;

    void Start()
    {
        Body = GetComponent<CharacterController>();

        asdtr = asdasd.GetComponent<Transform>();
        tr = GetComponent<Transform>();
        camTr = Camera.main.GetComponent<Transform>();
        cc = GetComponent<CharacterController>();
        points = GameObject.Find("WayPointGroup")
                    .GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (isStopped)
            return;

        switch(moveType)
        {
            case MoveType.WAY_POINT:
                MoveWayPoint();
                break;

            case MoveType.LOOK_AT:
                MoveLookAt();
                break;

            case MoveType.DAYDREAM:
                asd();
                break;
        }
        point();
    }

    void point()
    {
        Body.center = new Vector3(asdtr.localPosition.x, 1, asdtr.localPosition.z);

       // Debug.Log(asdtr.localPosition.x);
       
    }

    void MoveWayPoint()
    {
        // 현재 위치에서 다음 웨이포인트를 바로보는 벡터를 계산
        Vector3 direction = points[nextIdx].position - tr.position;
        // 산출된 벡터의 회전 각도를 쿼터니언 타입으로 산출
        Quaternion rot = Quaternion.LookRotation(direction);
        // 현재 각도에서 회전해야 할 각도까지 부드럽게 회전 처리
        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);

        // 전진 방향으로 이동 처리
        tr.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void MoveLookAt()
    {
        // 메인카메라가 바라보는 방향
        Vector3 dir = camTr.TransformDirection(Vector3.forward);
        // dir 벡터의 방향으로 초당 speed만큼씩 이동
        cc.SimpleMove(dir * speed);
    }

    void asd()
    {
        cc.SimpleMove(new Vector3(0,0,0));
    }

    void OnTriggerEnter(Collider coll)
    {
        // 웨이포인트(Point 게임오브젝트)에 충돌 여부 판단
        if(coll.CompareTag("WAY_POINT"))
        {
            // 맨 마지막 웨이포인트에 도달했을 때 처음 인덱스로 변경
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
        }
    }
}
