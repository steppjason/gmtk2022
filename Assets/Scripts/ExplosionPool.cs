using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{

	[SerializeField] int numberOfExplosions = 100;
	[SerializeField] Explosion explosion;

	Explosion[] _explosions;
	int _next = 0;

	void Start()
	{
		InstantiateBullets();
	}

	void InstantiateBullets()
	{
		_explosions = new Explosion[numberOfExplosions];
		for (int i = 0; i < numberOfExplosions; i++)
		{
			_explosions[i] = Instantiate(explosion, new Vector3(0, 0, 0), Quaternion.identity);
			_explosions[i].transform.parent = gameObject.transform;
			_explosions[i].gameObject.SetActive(false);
		}
	}

	public void SetExplosionActice(Vector3 position)
	{
		GetAvailable();
		Explosion newExplosion = _explosions[_next];
		newExplosion.transform.position = position;
		newExplosion.gameObject.SetActive(true);
	}

	void GetAvailable()
	{
		for (int i = 0; i < _explosions.Length; i++)
		{
			if (!_explosions[i].gameObject.activeInHierarchy)
			{
				_next = i;
				return;
			}
		}
	}

}
