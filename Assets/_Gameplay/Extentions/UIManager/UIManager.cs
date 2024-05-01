using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<System.Type, UICanvas> canvasActives = new Dictionary<System.Type, UICanvas>();
    Dictionary<System.Type, UICanvas> canvasPrefabs = new Dictionary<System.Type, UICanvas>();
    [SerializeField] List<UICanvas> canvasList;

    [SerializeField] Transform parent;

    private void Awake() {
        //Load prefab tu resource
        for(int i = 0; i < canvasList.Count; i++)
        {
            canvasPrefabs.Add(canvasList[i].GetType(), canvasList[i]);
        }
    }

    //Mo canvas
    public T OpenUI<T>() where T : UICanvas
    {
        T canvas = GetUI<T>();
        canvas.Setup();
        canvas.Open();

        return canvas;
    }

    //Dong canvas sau timer s
    public void CloseUI<T>(float timer) where T : UICanvas
    {
        if(IsOpened<T>())
        {
            canvasActives[typeof(T)].Close(timer);
        }    
    }

    //Dong canvas truc tiep
    public void CloseDirectly<T>() where T : UICanvas
    {
        if (IsOpened<T>())
        {
            canvasActives[typeof(T)].CloseDirectly();
        }
    }

    //Kiem tra canvas da duoc tai chua
    public bool IsLoaded<T>() where T : UICanvas
    {
        return canvasActives.ContainsKey(typeof(T)) && canvasActives[typeof(T)] != null; 
    }

    //Kiem tra canvas da duoc active chua
    public bool IsOpened<T>() where T : UICanvas
    {
        return IsLoaded<T>() && canvasActives[typeof(T)].gameObject.activeSelf;
    }

    //Lay active canvas
    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoaded<T>())
        {
            if (canvasPrefabs.ContainsKey(typeof(T)))
            {
                T prefab = canvasPrefabs[typeof(T)] as T;
                if (prefab != null)
                {
                    T canvas = Instantiate(prefab, parent);
                    canvasActives[typeof(T)] = canvas;
                    return canvas;
                }
                else
                {
                    Debug.LogError($"Prefab for {typeof(T)} is null.");
                    return null;
                }
            }
            else
            {
                Debug.LogError($"Prefab for {typeof(T)} is not loaded.");
                return null;
            }
        }

        return canvasActives[typeof(T)] as T;
    }


    //Get prefab
    private T GetUIPrefab<T>() where T : UICanvas
    {
        return canvasPrefabs[typeof(T)] as T;

    }

    //Dong tat ca canvas
    public void CloseAllUI()
    {
        foreach(var canvas in canvasActives)
        {
            if(canvas.Value != null && canvas.Value.gameObject.activeSelf)
            {
                canvas.Value.Close(0);
            }    
        }    
    }    
}
