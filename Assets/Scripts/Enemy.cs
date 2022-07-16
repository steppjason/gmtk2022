using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	Destructible _destructible;
	Animator _animator;
	EnemyBulletPool _enemyBulletPool;
	[SerializeField] PlayerController _player;

	[SerializeField] AudioClip hit;
	[SerializeField] AudioClip death;

	Vector3 direction = new Vector3(0, -1, 0);
	float speed = 1f;

	public int maxHealth = 10;
	public int health = 3;

	public float _maxCounter = 0.5f;
	float _counter;

	public SpriteRenderer sprite;
	public Shader guiText;
	public Shader defaultShader;


	float randomIdleStart;

	void Start()
	{
		_destructible = GetComponent<Destructible>();
		//_player = FindObjectOfType<PlayerController>();
		_enemyBulletPool = FindObjectOfType<EnemyBulletPool>();
		_animator = GetComponent<Animator>();
		_animator.Play("Enemy", 0, Random.Range(0, _animator.GetCurrentAnimatorStateInfo(0).length));

		sprite = GetComponent<SpriteRenderer>();
		guiText = Shader.Find("GUI/Text Shader");
		defaultShader = Shader.Find("Sprites/Default");
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
			if (health <= 0)
			{
				sprite.material.shader = defaultShader;
				_destructible.Kill();

				GameController.Instance.cameraShake.ShakeCamera(1.5f, 0.1f);
				GameController.Instance.AudioController.PlaySFX(death);
				GameController.Instance.AddScore(100 * GameController.Instance.multiplier);
				GameController.Instance.EnemyController.spawnRate -= 0.01f;

				if (GameController.Instance.EnemyController.spawnRate < 0.4f)
					GameController.Instance.EnemyController.spawnRate = 0.4f;

				int chance = Random.Range(0, 5);
				if (chance == 1)
					GameController.Instance.DicePool.SetDiceActive(transform.position);
			}
			else
			{
				StartCoroutine(FlashWhite());
				//GameController.Instance.AudioController.PlaySFX(hit);
				health--;
			}
		}

		if (other.gameObject.GetComponent<PlayerController>() && !other.gameObject.GetComponent<PlayerController>().invinsible)
		{
			_destructible.Kill();
		}

		if (other.gameObject.GetComponent<BoundsEnemy>())
			gameObject.SetActive(false);
	}


	IEnumerator FlashWhite()
	{
		sprite.material.shader = guiText;
		yield return new WaitForSeconds(0.1f);
		sprite.material.shader = defaultShader;
	}

}
