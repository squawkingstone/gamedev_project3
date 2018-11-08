using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour {

    [SerializeField] float maxLife = 100;
    [SerializeField] float damageSpeed = 1;
    
    float systemLife;

    public void InitRepairable()
    {
        systemLife = maxLife;
    }

    public void Repair(float amount)
    {
        systemLife = Mathf.Clamp(systemLife + amount, 0f, maxLife);
    }

    public void Damage()
    {
        systemLife = Mathf.Clamp(systemLife - (damageSpeed * Time.deltaTime), 0f, maxLife);
        Debug.Log(gameObject.name + ": " + systemLife);
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
    }

}
