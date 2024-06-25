using UnityEngine;

public class HandMove : MonoBehaviour
{
    public RectTransform hand;
    public float amplitudeX = 50f; 
    public float amplitudeY = 25f; 
    public float frequency = 1f; 

    private Vector2 startPos;

    void Start()
    {
        if (hand == null)
        {
            Debug.LogError("Hand RectTransform is not assigned.");
            return;
        }
        startPos = hand.anchoredPosition;
    }

    void Update()
    {
        if (hand != null)
        {
            float time = Time.time * frequency;
            float x = Mathf.Sin(time) * amplitudeX;
            float y = Mathf.Sin(2 * time) * amplitudeY;
            hand.anchoredPosition = startPos + new Vector2(x, y);
        }
    }
}
