using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadGame()
    {
        SceneManager.LoadScene("heart_of_the_iron_giant");
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public static void LoadWin()
    {
        SceneManager.LoadScene("Win");
    }

    public static void LoadLose()
    {
        SceneManager.LoadScene("Lose");
    }
}
