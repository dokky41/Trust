using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform objectToFollow; // ���� ������Ʈ
    public float followSpeed = 10f; // ���� ���ǵ�
    public float sensitivity = 500f; // ����
    public float clampAngle = 70f; // ī�޶� �������°� ���� ����

    private float rotX;
    private float rotY;

    public Transform realCamera; // ī�޶� ����
    public Vector3 dirNormalized;
    public Vector3 finalDir; // ��������
    public float minDistance; // �ּҰŸ�
    public float maxDistance; // �ִ�Ÿ�
    public float finalDinstance; // �����Ÿ�
    public float smoothness = 10f;


    private void Start()
    {
        // ���� �� ��ǲ �ʱ�ȭ
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDinstance = realCamera.localPosition.magnitude; //magniture��°� ũ��

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        objectToFollow = GameObject.Find("FollowCam").transform;
    }

    private void Update()
    {
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime; // ���������ӿ��� ������������ ���ݽð�
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
