using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsPlayerMove : MonoBehaviour
{
    // 移动速度
    public float walkSpeed = 1f;
    // 奔跑速度
    public float runSpeed = 1.5f;
    // 是否使用重力
    public bool useGravity;
    public float gravity = 9.8f;

    // 相机的transform
    private Transform cameraTransform;
    private float moveSpeed;
    private CharacterController controller;
    private float gravitySpeed = 0;
    void Start()
    {
        moveSpeed = walkSpeed;
        // 获取相机
        foreach(Transform transform in GetComponentsInChildren<Transform>())
        {
            if(transform.name =="Main Camera")
            {
                Debug.Log("Find Camera!");
                cameraTransform = transform;
            }
        }
        if (!cameraTransform)
        {
            Debug.LogError("Can not Find Camera in Children");
        }
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        // 获取y轴旋转角度
        float yRotation = cameraTransform.rotation.eulerAngles.y;
        // 角度转弧度
        yRotation = yRotation * Mathf.Deg2Rad;
        // cos值
        float cos = Mathf.Cos(yRotation);
        // sin 值
        float sin = Mathf.Sin(yRotation);
        // 向右移动
        moveRight(sin, cos);
        moveFoward(sin, cos);
        SpeedUp();
        // 如果使用重力
        if (useGravity)
        {
            SetGravity();
        }
    }

    // 向右移动
    void moveRight(float sin, float cos)
    {
        float horizontalValue = Input.GetAxis("Horizontal") * moveSpeed;
        Vector3 moveDirection = horizontalValue * (-Vector3.forward * sin+Vector3.right * cos);
        controller.Move(moveDirection * moveSpeed*Time.deltaTime);
    }

    // 向前移动
    void moveFoward(float sin, float cos)
    {
        float verticalValue = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 moveDirection = verticalValue * (Vector3.forward * cos + Vector3.right * sin);
        controller.Move(moveDirection* moveSpeed * Time.deltaTime);
    }

    //加速
    void SpeedUp()
    {
        float speed = Input.GetAxis("SpeedUp");
        // 如果启动加速键
        if (speed == 1f)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }
    //设置重力
    void SetGravity()
    {
        if (controller.isGrounded)
        {
            gravitySpeed = 0;
        }
        gravitySpeed -= (gravity * Time.deltaTime);
        Vector3 move = Vector3.zero;
        move.y += gravitySpeed;
        controller.Move(move * Time.deltaTime);
    }
}
