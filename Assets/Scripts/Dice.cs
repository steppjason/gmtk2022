using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

	[SerializeField] AudioClip pickupSFX;
	public float speed = 0.25f;
	public float rotationSpeed = 50f;
	public Vector3 direction = new Vector3(0, -1, 0);
	public float rollTime = 0.5f;

	int roll = 1;

	SpriteRenderer _sprite;
	public Sprite[] dice;

	void Start()
	{
		roll = Random.Range(1, 7);
		_sprite = GetComponent<SpriteRenderer>();
		_sprite.sprite = dice[roll - 1];
		StartCoroutine(RollDice());
	}

	void Update()
	{
		if (gameObject.activeInHierarchy && speed > 0)
			transform.position += direction * speed * Time.deltaTime;

		transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
		_sprite.sprite = dice[roll - 1];

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<BoundsEnemy>())
			gameObject.SetActive(false);

		if (other.gameObject.GetComponent<PlayerController>())
		{
			GameController.Instance.DoPower(roll);
			GameController.Instance.AudioController.PlaySFX(pickupSFX);
			gameObject.SetActive(false);
		}
	}

	public void Spawn()
	{
		StartCoroutine(RollDice());
	}

	IEnumerator RollDice()
	{
		while (true)
		{
			roll++;
			if (roll > 6)
				roll = 1;
			yield return new WaitForSeconds(rollTime);
		}
	}

}
