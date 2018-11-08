using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool m_Tutorial;

    void Start()
    {
        m_Tutorial = true;
    }

    [ContextMenu("End Tutorial")]
    void EndTutorial()
    {
        m_Tutorial = false;
    }

    public bool IsTutorial() { return m_Tutorial; }
}