using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{

	[SerializeField] int numberOfBullets = 100;
	[SerializeField] Bullet bullet;

	Bullet[] _bullets;
	int _nextBullet = 0;

	void Awake()
	{
		InstantiateBullets();
	}

	void Update()
	{

	}

	void InstantiateBullets()
	{
		_bullets = new Bullet[numberOfBullets];
		for (int i = 0; i < numberOfBullets; i++)
		{
			_bullets[i] = Instantiate(bullet, new Vector3(0, 0, 0), Quaternion.identity);
			_bullets[i].transform.parent = gameObject.transform;
			_bullets[i].gameObject.SetActive(false);
		}
	}

	public void SetBulletActive(Vector3 position, Vector3 direction)
	{
		GetAvailable();
		Bullet newBullet = _bullets[_nextBullet];
		newBullet.transform.position = position;
		newBullet.SetDirection(direction);
		newBullet.gameObject.SetActive(true);
	}

	void GetAvailable()
	{
		for (int i = 0; i < _bullets.Length; i++)
		{
			if (!_bullets[i].gameObject.activeInHierarchy)
			{
				_nextBullet = i;
				return;
			}
		}
	}
	
}
