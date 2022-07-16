using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	ExplosionPool _explosionPool;

	void Start()
	{
		_explosionPool = FindObjectOfType<ExplosionPool>();
	}

	public void Kill()
	{
		gameObject.SetActive(false);
		_explosionPool.SetExplosionActice(gameObject.transform.position);
	}
}
