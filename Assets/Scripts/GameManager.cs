using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float gracePeriod;
    [SerializeField] GameObject signal;
    [SerializeField] float startInterval;
    [SerializeField] float endInterval;
    [SerializeField] AnimationCurve difficultyScale;
    [SerializeField] float roundTime;
    [SerializeField] Text countdown;

    [SerializeField] EventSystem eventSystem;

    Material signalMaterial;
    private bool m_Tutorial;
    float timer;

    void Start()
    {
        Renderer r = signal.GetComponent<Renderer>();
        signalMaterial = r.sharedMaterial = new Material(r.sharedMaterial);
        signalMaterial.SetColor("_Color", Color.red);
        m_Tutorial = true;
        timer = 0f;
    }

    [ContextMenu("End Tutorial")]
    void EndTutorial()
    {
        m_Tutorial = false;
        signalMaterial.SetColor("_Color", Color.green);
    }

    public bool IsTutorial() { return m_Tutorial; }

    void Update()
    {
        if (IsTutorial())
        {
            if (timer >= gracePeriod)
            {
                EndTutorial();
                timer = 0f;
                return;
            }
            timer += Time.deltaTime;
            SetCountdown(gracePeriod - timer);
        }
        if (!IsTutorial())
        {
            if (timer >= roundTime)
            {
                LoadScene.LoadWin();
            }
            eventSystem.SetInterval(Mathf.Lerp(startInterval, endInterval, timer/roundTime));
            timer += Time.deltaTime;
            SetCountdown(roundTime - timer);   
        }
    }

    void SetCountdown(float count)
    {
        int seconds = Mathf.FloorToInt(count);
        countdown.text = (seconds/60).ToString() + ":" + (seconds % 60).ToString("D2");
    }
}