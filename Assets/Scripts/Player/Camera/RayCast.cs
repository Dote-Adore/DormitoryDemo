using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 发射射线，进行交互
public class RayCast : MonoBehaviour
{

    private GameObject pickGameObj = null;
    private Ray ray;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CastObject();
    }

    void CastObject()
    {
        // 从屏幕中心发射一条射线
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out hit))
        {
            // 获取碰到的物体
            if (pickGameObj != hit.collider.gameObject && pickGameObj != null)
            {
                if (pickGameObj.GetComponent<Animator>())
                {
                    pickGameObj.GetComponent<Animator>().SetBool("outline", false);
                }
            }
            pickGameObj = hit.collider.gameObject;
            if (pickGameObj.GetComponent<Animator>())
            {
                pickGameObj.GetComponent<Animator>().SetBool("outline", true);
            }
        }
    }
}
