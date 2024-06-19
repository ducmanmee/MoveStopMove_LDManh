using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : Singleton<TargetIndicator>
{
    [SerializeField] Text nameCharacter;
    [SerializeField] GameObject waypoint;
    [SerializeField] Character ownerTarget;
    [SerializeField] RectTransform rect;

    Vector3 viewPoint;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;
    Vector2 viewPointX = new Vector2(0.075f, 0.925f);
    Vector2 viewPointY = new Vector2(0.05f, 0.85f);

    Vector2 viewPointInCameraX = new Vector2(0.075f, 0.925f);
    Vector2 viewPointInCameraY = new Vector2(0.05f, 0.95f);

    bool IsInView()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(ownerTarget.transform.position);
        return viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1;
    }

    private void LateUpdate()
    {
        viewPoint = Camera.main.WorldToViewportPoint(ownerTarget.transform.position);
        nameCharacter.enabled = IsInView();
        waypoint.SetActive(!IsInView());

        viewPoint.x = Mathf.Clamp(viewPoint.x, viewPointX.x, viewPointX.y);
        viewPoint.y = Mathf.Clamp(viewPoint.y, viewPointY.x, viewPointY.y);

        Vector3 targetSPoint = Camera.main.ViewportToScreenPoint(viewPoint) - screenHalf;
        Vector3 playerSPoint = Camera.main.WorldToScreenPoint(Player.ins.transform.position) - screenHalf;
        rect.anchoredPosition = targetSPoint;
        waypoint.transform.up = (targetSPoint - playerSPoint).normalized;
    }

    public void SetName(string name)
    {
        nameCharacter.text = name;
        Debug.Log(name);
    } 
    
    public void SetOwner(Character C)
    {
        ownerTarget = C;
    }    
}
