using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour {

    [SerializeField] float maxLife = 100;
    [SerializeField] float damageSpeed = 1;
    [SerializeField] MapSystemController map;
    
    float systemLife;

    public void InitRepairable()
    {
        systemLife = maxLife;
        map.UpdateMapSystem(systemLife, maxLife);
    }

    public void Repair(float amount)
    {
        Debug.Log("Repairing " + gameObject.name + ": " + systemLife);
        systemLife = Mathf.Clamp(systemLife + amount, 0f, maxLife);
        map.UpdateMapSystem(systemLife, maxLife);
    }

    public void Damage()
    {
        systemLife = Mathf.Clamp(systemLife - (damageSpeed * Time.deltaTime), 0f, maxLife);
        Debug.Log(gameObject.name + ": " + systemLife);
        map.UpdateMapSystem(systemLife, maxLife);
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
    }

}
