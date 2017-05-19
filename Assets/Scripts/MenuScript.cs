using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MenuScript : MonoBehaviour {

    void OnMouseDown()
    {
        if (gameObject.name.Equals("Play"))
        {
            SceneManager.LoadScene("Main");
        }

        if (gameObject.name.Equals("Rules"))
        {
            SceneManager.LoadScene("RuleScene");
        }

        if (gameObject.name.Equals("Exit"))
        {
            Application.Quit();
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
