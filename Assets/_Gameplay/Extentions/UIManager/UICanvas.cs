using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] bool isDestroyOnClose = false;

    private void Awake()
    {
        //Xu ly tai tho
        RectTransform rect = GetComponent<RectTransform>();
        float ratio = (float)Screen.width / (float)Screen.height;
        if(ratio > 2.1f)
        {
            Vector2 leftButton = rect.offsetMin;
            Vector2 rightButton = rect.offsetMax;

            leftButton.y = 0f;
            rightButton.y = -100f;

            rect.offsetMin = leftButton;
            rect.offsetMax = rightButton;
        }    
    }

    //goi truoc khi canvas duoc active
    public virtual void Setup()
    {

    }   
    //goi sau khi duoc active
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }   
    
    //dong canvas sau timer s 
    public virtual void Close(float timer)
    {
        Invoke(nameof(CloseDirectly), timer);
    }

    //dong canvas ttruc tiep
    public virtual void CloseDirectly()
    {
        if(isDestroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }    
    }

}
