using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalInput;
    public float VerticalInput;
    public bool MouseButtonDown;
    public bool SpaceKeyDown;


    // Update is called once per frame
    void Update()
    {
        if (!MouseButtonDown && Time.timeScale != 0)
        {
            MouseButtonDown = Input.GetMouseButtonDown(0);
        }
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
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
