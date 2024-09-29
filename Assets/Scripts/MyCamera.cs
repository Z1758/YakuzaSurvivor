using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class MyCamera : MonoBehaviour
{
    [SerializeField] private PlayController pc;
    [SerializeField] private Transform target;
    [SerializeField] private Transform mainCamera;


    Vector3 offset;
    Vector3 cameraOffset;
   
    [SerializeField] float smoothTime;
    Vector3 curVelocity = Vector3.zero;


    private Vector2 mouseDelta;

    public float rotateSpeed;



    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
   
        Cursor.visible = false;
        offset = transform.position - target.position;
        cameraOffset = mainCamera.position - transform.position;
    }


    public void LookAround(InputAction.CallbackContext value)
    {
        mouseDelta =  value.ReadValue<Vector2>();
        Vector3 angle = transform.rotation.eulerAngles;

        


        float maxAngle = angle.x - mouseDelta.y * rotateSpeed * Time.deltaTime;

        //위에서 아래 보는 각도
        if (maxAngle < 180f)
        {
            maxAngle = Mathf.Clamp(maxAngle, -1f, 40f);
        }
        //아래에서 위 보는 각도
        else
        {
            maxAngle = Mathf.Clamp(maxAngle, 340f, 361f);
        }


        transform.rotation = Quaternion.Euler(maxAngle, angle.y + mouseDelta.x * rotateSpeed * Time.deltaTime, angle.z);
   

        pc.InputDir();
    }


    private void LateUpdate()
    {

        transform.position = target.position + offset;


    }


}
