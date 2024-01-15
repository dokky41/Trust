using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    Animator animator;


    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        // Move();
        
    }


    // 이동 방향 구하기
    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        bool isRun = Input.GetKey(KeyCode.LeftShift);

        animator.SetBool("isMove", isMove);
        animator.SetBool("isRun", isRun);

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;


            characterBody.transform.forward = moveDir.normalized;

            if (isRun)
            {
                transform.position += Vector3.ClampMagnitude(moveDir, 1f) * Time.deltaTime * 8f;
                
            }
            else
            {
                transform.position += Vector3.ClampMagnitude(moveDir, 1f) * Time.deltaTime * 4f;
                
            }

        }


    }

}
