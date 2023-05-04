using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [SerializeField] public Texture2D normal_cursor;
    [SerializeField] public Texture2D pointer_cursor;

    void Awake()
    {
        Instance = this;
        changeToNormal();
    }

    public void changeToPointer()
    {
        Cursor.SetCursor(pointer_cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void changeToNormal()
    {
        Cursor.SetCursor(normal_cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update()
    {
        
    }

}
