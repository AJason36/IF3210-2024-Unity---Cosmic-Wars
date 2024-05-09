using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 20f;
    public Transform follow;
 
    float XRotation = 0f;
    float YRotation = 0f;

    float BelowXRotationThreshold = -1.5f;
    float UpperXRotationThreshold = 7f;
 
    void Start()
    {
      //Locking the cursor to the middle of the screen and making it invisible
      Cursor.lockState = CursorLockMode.Locked;
    }
 
    void LateUpdate()
    {
       float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
       float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
 
       //control rotation around x axis (Look up and down)
       XRotation -= mouseY;
 
       //we clamp the rotation so we cant Over-rotate (like in real life)
       XRotation = Mathf.Clamp(XRotation, -90f, 90f);

      // Apply XRotation Threshold
       if (XRotation < BelowXRotationThreshold){
        XRotation = BelowXRotationThreshold;
       }
       if (XRotation > UpperXRotationThreshold){
        XRotation = UpperXRotationThreshold;
       }
 
       //control rotation around y axis (Look up and down)
       YRotation += mouseX;

      // Debug.Log(string.Format("XMouse {0}, YMouse {1}", mouseX, mouseY));
      // Debug.Log(string.Format("XRotation {0}, YRotation {1}", XRotation, YRotation));
 
      follow.localRotation = UnityEngine.Quaternion.Euler(XRotation * (mouseSensitivity/2) , 0f, 0f);
      transform.localRotation = UnityEngine.Quaternion.Euler(0f, YRotation * mouseSensitivity, 0f);
    }
}
