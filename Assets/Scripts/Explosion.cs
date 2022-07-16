using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	int _seconds = 1;
	
	void Update()
	{
		StartCoroutine(SetInactive());
	}

	IEnumerator SetInactive()
	{
		yield return new WaitForSeconds(_seconds);
		gameObject.SetActive(false);
	}
}
