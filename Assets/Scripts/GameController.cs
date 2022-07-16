using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameController : MonoBehaviour
{

	public static GameController Instance { get; private set; }

	public PlayerController PlayerController;
	EnemyController EnemyController;

	public int score;
	public int lives = 3;
	public bool dead = false;
	public TMP_Text scoreBoard;

	public Image[] extraLives;

	public Image backgroundLayer1;
	public Image backgroundLayer2;
	public Image backgroundLayer3;

	float offset1 = 0f;
	float offset2 = 0f;
	float offset3 = 0f;

	public float speed1 = 1f;
	public float speed2 = 1f;
	public float speed3 = 1f;


	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		EnemyController = GetComponent<EnemyController>();
	}

	void Start()
	{
		score = 0;
	}

	void Update()
	{
		scoreBoard.text = score.ToString("n0", new NumberFormatInfo { NumberGroupSeparator = " " });

		if (dead && lives > 0)
		{
			dead = false;
			StartCoroutine(Respawn());
		}

		if (lives > 6)
			lives = 6;

		for (int i = 0; i < extraLives.Length; i++)
		{
			if (!(i < lives))
				extraLives[i].gameObject.SetActive(false);
			else
				extraLives[i].gameObject.SetActive(true);

		}

		offset1 += Time.deltaTime * speed1;
		offset2 += Time.deltaTime * speed2;
		offset3 += Time.deltaTime * speed3;

		backgroundLayer1.material.SetTextureOffset("_MainTex", new Vector2(1.25f, offset1));
		backgroundLayer2.material.SetTextureOffset("_MainTex", new Vector2(0.5f, offset2));
		backgroundLayer3.material.SetTextureOffset("_MainTex", new Vector2(-2f, offset3));

	}

	public void AddScore(int value)
	{
		score += value;

		if (score % 10000 == 0)
			lives++;
	}

	public void DoPower(int roll)
	{
		
	}

	IEnumerator Respawn()
	{
		yield return new WaitForSeconds(2);
		PlayerController.invinsible = true;

		PlayerController.gameObject.transform.position = new Vector3(0, -1.2f, 0);
		PlayerController.gameObject.SetActive(true);
	}
}
