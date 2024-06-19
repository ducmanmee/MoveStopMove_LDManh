using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow ins;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 targetPos;
    [SerializeField]private Vector3 offset;
    [SerializeField]private Vector3 camMenu;
    private bool isScale;
    private Vector3 startOffset;

    private void MakeInstance()
    {
        if (ins == null)
        {
            ins = this;
        }    
        startOffset = offset;
    }     

    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        transform.position = camMenu;
    }

    void LateUpdate()
    {
        if (GameManager.ins.GetGameState() is MenuState)
        {
            ResetCamera();
            transform.position = camMenu;
            return;
        }   
        if(isScale)
        {
            offset += new Vector3(0, .5f, .5f);
            isScale = false;

        }
        targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
    }

    public void ResetCamera()
    {
        offset = startOffset;
    }    

    public bool IsScale
    {
        get { return isScale; }
        set { isScale = value; }
    }

}
