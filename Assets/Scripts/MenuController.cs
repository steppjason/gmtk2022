using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.K))
			SceneManager.LoadScene("Game");
	}
}
