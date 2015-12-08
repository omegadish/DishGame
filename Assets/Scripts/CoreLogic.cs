using UnityEngine;
using System.Collections;

public class CoreLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
