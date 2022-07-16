using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] BulletPool bulletPool;
	[SerializeField] float fireRate = 0.5f;

	Animator _animator;

	Vector3 direction;

	Coroutine fire;

	float xMin = -1.12f;
	float xMax = 1.12f;
	float yMax = 1.58f;
	float yMin = -1.58f;

	void Start()
	{
		_animator = GetComponent<Animator>();

	}

	void Update()
	{
		GetInput();
		Move();
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
		
		if(Input.GetKeyUp(KeyCode.K))
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
			bulletPool.SetBulletActive(transform.position, new Vector3(0, 1, 0));
			yield return new WaitForSeconds(fireRate);
		}
	}
}
