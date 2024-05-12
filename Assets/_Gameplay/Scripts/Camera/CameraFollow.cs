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
    public Vector3 startCamPos;

    private void MakeInstance()
    {
        if (ins == null)
        {
            ins = this;
        }    
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
            transform.position = camMenu;
            return;
        }   
        targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
    }

}
