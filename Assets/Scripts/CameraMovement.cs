using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform objectToFollow; // 따라갈 오브젝트
    public float followSpeed = 10f; // 따라갈 스피드
    public float sensitivity = 500f; // 감도
    public float clampAngle = 70f; // 카메라 뒤집히는거 방지 각도

    private float rotX;
    private float rotY;

    public Transform realCamera; // 카메라 정보
    public Vector3 dirNormalized;
    public Vector3 finalDir; // 최종방향
    public float minDistance; // 최소거리
    public float maxDistance; // 최대거리
    public float finalDinstance; // 최종거리
    public float smoothness = 10f;


    private void Start()
    {
        // 시작 시 인풋 초기화
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDinstance = realCamera.localPosition.magnitude; //magniture라는건 크기

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        objectToFollow = GameObject.Find("FollowCam").transform;
    }

    private void Update()
    {
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime; // 현재프레임에서 마지막프레임 간격시간
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    private void LateUpdate()
    {
        transform.position
            = Vector3.MoveTowards(transform.position, objectToFollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDinstance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDinstance = maxDistance;
        }
        realCamera.localPosition
            = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDinstance, Time.deltaTime * smoothness);

    }

}
