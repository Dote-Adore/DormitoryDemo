using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 交互类型
public enum InteractObjType
{
    Reapetdly = 0, Once = 1, Twice = 2, Pickup = 3
}

public class InteractObj : MonoBehaviour
{
    public InteractObjType type = InteractObjType.Twice;
    public bool canInteract;
    public bool shouldDestroy;
    public float interactTime = 0.5f;
    private bool isFirstInteraction = true;

    //交互时间是否被重写
    public bool isTimeOverrided = false;
    private PickupObj pickupObj;
    Animator animator;

    void Start()
    {
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("物体"+this.name+"没有挂载组件Animator，请挂载！");
        }
        if (GetComponent<PickupObj>())
        {
            type = InteractObjType.Pickup;
            pickupObj = GetComponent<PickupObj>();
        }
    }

    public void Interact()
    {
        // 如果当前不可交互
        if (!canInteract)
            return;
        canInteract = false;
        switch (type)
        {
            // 两次交互
            case InteractObjType.Twice:
                InteractTwice();
                break;
            case InteractObjType.Pickup:
                Pickup();
                break;
        }

        // 延时执行
        if(!isTimeOverrided)
            StartCoroutine(InteractDown());
    }
    public void ReadyToInteract()
    {
        if(animator)
        // 将描边动画启用
            animator.SetBool("outline", true);
    }
    public void CancelInteract()
    {
        if(animator)
        // 将描边动画禁止使用
        animator.SetBool("outline", false);
        Debug.Log("Cancel inteact:" + name);
    }

    IEnumerator InteractDown()
    {
        yield return new WaitForSeconds(interactTime);
        canInteract = true;
    }

    private void InteractTwice()
    {
        Debug.Log("Interact " + this.name);
        if (isFirstInteraction)
        {
            isFirstInteraction = false;
            animator.SetBool("Interaction", true);
        }
        else
        {
            isFirstInteraction = true;
            animator.SetBool("Interaction", false);
        }
    }

    // 捡起
    private void Pickup()
    {
        Debug.Log("Pickup");
        if (isFirstInteraction)
        {
            CancelInteract();
            isFirstInteraction = false;
            pickupObj.Pickup();
        }
        else
        {
            ReadyToInteract();
            pickupObj.PickDown();
            isFirstInteraction = true;
        }
    }

    public bool IsFirstInteraction()
    {
        return isFirstInteraction;
    }
}
