using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

	[SerializeField] float speed = 1.5f;
	[SerializeField] Vector3 direction = new Vector3(0, 0, 0);

	void Update()
	{
		if (gameObject.activeInHierarchy && speed > 0)
			transform.position += direction * speed * Time.deltaTime;
	}

	public void SetDirection(Vector3 direction)
	{
		this.direction = direction;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<BoundsEnemy>() || other.gameObject.GetComponent<Bounds>())
			gameObject.SetActive(false);

		if(other.gameObject.GetComponent<PlayerController>() && !other.gameObject.GetComponent<PlayerController>().invinsible)
			gameObject.SetActive(false);

	}
}
