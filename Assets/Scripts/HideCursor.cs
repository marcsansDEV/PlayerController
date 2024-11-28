using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HideCursor : MonoBehaviour
{
    [SerializeField] private bool m_Hide = true;
    void Start()
    {
        if (m_Hide)
            Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
