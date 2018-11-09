using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour {

    [SerializeField] float maxLife = 100;
    [SerializeField] float damageSpeed = 1;
    [SerializeField] MapSystemController map;
    [SerializeField] ParticleSystem sparks;
       
    float systemLife;
    bool linesEnabled;

    public void InitRepairable()
    {
        systemLife = maxLife;
        linesEnabled = false;
        map.UpdateMapSystem(systemLife, maxLife);
        UpdateSparks();
    }

    public void Repair(float amount)
    {
        Debug.Log("Repairing " + gameObject.name + ": " + systemLife);
        systemLife = Mathf.Clamp(systemLife + amount, 0f, maxLife);
        map.UpdateMapSystem(systemLife, maxLife);
        UpdateSparks();
    }

    public void Damage()
    {
        systemLife = Mathf.Clamp(systemLife - (damageSpeed * Time.deltaTime), 0f, maxLife);
        Debug.Log(gameObject.name + ": " + systemLife);
        map.UpdateMapSystem(systemLife, maxLife);
        UpdateSparks();
    }

    public bool IsOnline()
    {
        return (systemLife > 0f);
    }

    public float GetLifePercentage()
    {
        return Mathf.Clamp01(systemLife / maxLife);
    }

    public void DecreaseLife(int amount)
    {
        systemLife = Mathf.Clamp(systemLife - amount, 0f, maxLife);
        map.UpdateMapSystem(systemLife, maxLife);
        UpdateSparks();
    }

    public void SetLinesEnabled(bool e)
    {
        linesEnabled = e;
    }

    public void UpdateSparks()
    {
        if (!IsOnline())
        {
            sparks.Play();
        }
        else
        {
            sparks.Stop();
        }
    }

}
