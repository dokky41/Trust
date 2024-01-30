using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; // 마우스 커서 보이지 않게
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 고정
    }

 
}
