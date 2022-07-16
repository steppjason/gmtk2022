using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] BulletPool bulletPool;
	[SerializeField] float fireRate = 0.5f;

	Animator _animator;
	Destructible _destructible;
	SpriteRenderer _sprite;

	Vector3 direction;

	Coroutine fire;

	public bool invinsible = true;
	public float flashCount = 0;
	public bool flipped = true;
	public float flashSpeed = 1f;
	public bool flashing = false;

	float countDown = 5;

	float xMin = -1.12f;
	float xMax = 1.12f;
	float yMax = 1.58f;
	float yMin = -1.58f;

	void Start()
	{
		_animator = GetComponent<Animator>();
		_destructible = GetComponent<Destructible>();
		_sprite = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		GetInput();
		Move();
		CheckInvinsible();
	}

	void CheckInvinsible()
	{

		if (invinsible)
			countDown -= Time.deltaTime;

		if (countDown <= 0)
		{
			flashing = false;
			invinsible = false;
			flashCount = 0;
			countDown = 5;
		}
		else if (countDown <= 3 && countDown > 0)
			flashing = true;


		if (flashing && invinsible)
		{
			flashCount -= Time.deltaTime * flashSpeed;
			if (flashCount <= 0)
			{
				flipped = !flipped;
				flashCount = 1;
			}

			if (flipped)
				_sprite.color = new Color(1, 1, 1, 0.5f);
			else
				_sprite.color = new Color(1, 1, 1, 1f);
		}
		else if (invinsible)
			_sprite.color = new Color(1, 1, 1, 0.5f);
		else
			_sprite.color = new Color(1, 1, 1, 1);
	}

	void GetInput()
	{
		if (Input.GetKey(KeyCode.A))
			_animator.SetInteger("Direction", -1);
		else if (Input.GetKey(KeyCode.D))
			_animator.SetInteger("Direction", 1);
		else
			_animator.SetInteger("Direction", 0);

		if (Input.GetKeyDown(KeyCode.K))
			fire = StartCoroutine(Fire());

		if (Input.GetKeyUp(KeyCode.K))
			StopCoroutine(fire);

	}

	void Move()
	{
		direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
		direction.Normalize();
		transform.position += direction * moveSpeed * Time.deltaTime;
		transform.position = new Vector2(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax));
	}

	IEnumerator Fire()
	{
		while (true)
		{
			//bulletPool.SetBulletActive(transform.position, new Vector3(0, 1, 0));
			FireFour();
			yield return new WaitForSeconds(fireRate);
		}
	}

	void FireSingle()
	{
		bulletPool.SetBulletActive(transform.position, new Vector3(0, 1, 0));
	}

	void FireDouble()
	{
		bulletPool.SetBulletActive(new Vector3(transform.position.x - 0.06f, transform.position.y, transform.position.z), new Vector3(0, 1, 0));
		bulletPool.SetBulletActive(new Vector3(transform.position.x + 0.06f, transform.position.y, transform.position.z), new Vector3(0, 1, 0));
	}

	void FireTriple()
	{
		bulletPool.SetBulletActive(transform.position, new Vector3(0.5f, 1, 0));
		bulletPool.SetBulletActive(transform.position, new Vector3(-0.5f, 1, 0));
		bulletPool.SetBulletActive(transform.position, new Vector3(0, 1, 0));
	}

	void FireFour()
	{
		bulletPool.SetBulletActive(new Vector3(transform.position.x - 0.06f, transform.position.y, transform.position.z), new Vector3(0, 1, 0));
		bulletPool.SetBulletActive(new Vector3(transform.position.x + 0.06f, transform.position.y, transform.position.z), new Vector3(0, 1, 0));
		bulletPool.SetBulletActive(new Vector3(transform.position.x + 0.06f, transform.position.y, transform.position.z), new Vector3(0.1f, 1, 0));
		bulletPool.SetBulletActive(new Vector3(transform.position.x - 0.06f, transform.position.y, transform.position.z), new Vector3(-0.1f, 1, 0));
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!invinsible && (other.gameObject.GetComponent<Enemy>() || other.gameObject.GetComponent<EnemyBullet>()))
		{
			GameController.Instance.lives -= 1;
			GameController.Instance.dead = true;
			_destructible.Kill();

		}
	}


}
