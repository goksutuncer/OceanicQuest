using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalInput;
    public float VerticalInput;
    public bool MouseButtonDown;
    public bool MouseButtonUp;
    public bool SpaceKeyDown;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!MouseButtonDown && Time.timeScale != 0)
        {
            MouseButtonDown = Input.GetMouseButtonDown(0);
        }
        MouseButtonUp = Input.GetMouseButtonUp(0);
        bool w = Input.GetKey(KeyCode.W);
        bool a = Input.GetKey(KeyCode.A);
        bool s = Input.GetKey(KeyCode.S);
        bool d = Input.GetKey(KeyCode.D);
        HorizontalInput = 0;
        VerticalInput = 0;
        if (w)
        {
            VerticalInput = 1;
        }
        if (a)
        {
            HorizontalInput = -1;
        }
        if (s)
        {
            VerticalInput = -1;
        }
        if (d)
        {
            HorizontalInput = 1;
        }
        if (!SpaceKeyDown && Time.timeScale != 0)
        {
            SpaceKeyDown = Input.GetKeyDown(KeyCode.Space);
        }
    }

    public void ClearCache()
    {
        OnDisable();
    }
    private void OnDisable()
    {
        MouseButtonDown = false;
        HorizontalInput = 0;
        VerticalInput = 0;
        SpaceKeyDown = false;
    }
}
