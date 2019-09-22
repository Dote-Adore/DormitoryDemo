using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Interacttype
{
    UI = 0,
    InterctProp = 1
}

public class Interact : MonoBehaviour
{
    public bool canInteract = true;
    public Interacttype type = Interacttype.InterctProp;
    private RayCast rayCast;
    private GameObject interactObj;
    
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            rayCast = GetComponentInChildren<RayCast>();
        }
        catch
        {
            Debug.LogError("未在children中找到rayCast组件，请挂载！");
        }
        // 获取当前交互的物体
            interactObj = rayCast.GetInteractObj();
    }

    // Update is called once per frame
    void Update()
    {
        interactObj = rayCast.GetInteractObj();
        float interactValue = Input.GetAxis("Interact");
        if (interactValue == 1f)
        {
            // 如果当前物体可以交互
            if (interactObj != null && interactObj.GetComponent<InteractObj>().canInteract)
            {
                Debug.Log("canInteract");
                interactObj.GetComponent<InteractObj>().Interact();
            }
        }
    }
}
