using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] EnemyPool enemyPool;
	public float _spawnRate = 1f;

	float xMin = -1.12f;
	float xMax = 1.12f;
	float yMax = 1.58f;
	float yMin = -1.58f;

	void Start()
	{
		StartCoroutine(SpawnEnemy());
	}

	IEnumerator SpawnEnemy()
	{
		while (true)
		{
			enemyPool.SetEnemyActive(new Vector3(Random.Range(xMin, xMax), Random.Range(0.5f, 2) + yMax, 0));
			yield return new WaitForSeconds(_spawnRate);
		}
	}
}
