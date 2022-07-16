using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	Destructible _destructible;
	Animator _animator;
	EnemyBulletPool _enemyBulletPool;
	[SerializeField] PlayerController _player;

	Vector3 direction = new Vector3(0, -1, 0);
	float speed = 1f;

	float _maxCounter = 0.25f;
	float _counter;


	float randomIdleStart;

	void Start()
	{
		_destructible = GetComponent<Destructible>();
		//_player = FindObjectOfType<PlayerController>();
		_enemyBulletPool = FindObjectOfType<EnemyBulletPool>();
		_animator = GetComponent<Animator>();
		_animator.Play("Enemy", 0, Random.Range(0, _animator.GetCurrentAnimatorStateInfo(0).length));
	}

	void Update()
	{
		if (gameObject.activeInHierarchy && speed > 0)
			transform.position += direction * speed * Time.deltaTime;

		Fire();
	}

	void Fire()
	{
		_counter -= Time.deltaTime;
		if (_counter <= 0 && transform.position.y > 0)
		{
			if (_player.gameObject.activeInHierarchy)
			{
				_enemyBulletPool.SetBulletActive(transform.position, (_player.transform.position - transform.position).normalized);
				ResetShotCounter();
			}
		}
	}

	void ResetShotCounter()
	{
		_counter = _maxCounter;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<Bullet>())
			_destructible.Kill();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<Bullet>())
		{
			_destructible.Kill();
			GameController.Instance.AddScore(100);
		}

		if (other.gameObject.GetComponent<PlayerController>() && !other.gameObject.GetComponent<PlayerController>().invinsible)
		{
			_destructible.Kill();
		}

		if (other.gameObject.GetComponent<BoundsEnemy>())
			gameObject.SetActive(false);
	}
}
