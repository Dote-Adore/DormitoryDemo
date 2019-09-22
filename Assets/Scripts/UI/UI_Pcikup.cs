using UnityEngine.UI;
using UnityEngine;

public class UI_Pcikup : MonoBehaviour
{
    public UIMaskBlur blurMask;
    public Text UI_itemName;
    public Text UI_introduction;
    // 按键提示
    public RectTransform buttonPrompt;
    void Start()
    {
    }

    public void Blur(bool isBlur)
    {
        blurMask.BlurMaskUI(isBlur);
    }

    public void SetText(string itemName, string introduction)
    {
        UI_itemName.text = itemName;
        UI_introduction.text = introduction;
    }
    

    // 按键提示
    public void ButtonPrompt(bool isVisable)
    {
        buttonPrompt.gameObject.SetActive(isVisable);
    }
}
