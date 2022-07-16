using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] float speed = 1f;
	[SerializeField] Vector3 direction = new Vector3(0, 0, 0);
	[SerializeField] int damage = 1;

	void Update()
	{
		if (gameObject.activeInHierarchy && speed > 0)
			transform.position += direction * speed * Time.deltaTime;
	}

	public void SetDirection(Vector3 direction)
	{
		this.direction = direction;
	}

	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.GetComponent<Enemy>() || other.gameObject.GetComponent<Bounds>())
			gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<Enemy>() || other.gameObject.GetComponent<Bounds>())
			gameObject.SetActive(false);
	}

}
