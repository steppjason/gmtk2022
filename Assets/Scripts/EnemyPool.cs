using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

	[SerializeField] int numberOfEnemies = 50;
	[SerializeField] Enemy enemy;

	Enemy[] _enemies;
	int _next = 0;

	void Awake()
	{
		InstantiateEnemys();
	}

	void InstantiateEnemys()
	{
		_enemies = new Enemy[numberOfEnemies];
		for (int i = 0; i < numberOfEnemies; i++)
		{
			_enemies[i] = Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
			_enemies[i].transform.parent = gameObject.transform;
			_enemies[i].gameObject.SetActive(false);
		}
	}

	public void SetEnemyActive(Vector3 position)
	{
		GetAvailable();
		Enemy newEnemy = _enemies[_next];
		newEnemy.transform.position = position;
		newEnemy.gameObject.SetActive(true);
	}

	void GetAvailable()
	{
		for (int i = 0; i < _enemies.Length; i++)
		{
			if (!_enemies[i].gameObject.activeInHierarchy)
			{
				_next = i;
				return;
			}
		}
	}

}
