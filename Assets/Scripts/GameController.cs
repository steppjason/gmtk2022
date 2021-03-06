using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameController : MonoBehaviour
{

	public static GameController Instance { get; private set; }

	public PlayerController PlayerController;
	public DicePool DicePool;
	public EnemyController EnemyController;
	public AudioController AudioController;
	public CameraShake cameraShake;
	public Animator textAnimator;

	public GameObject gameOverScreen;

	public AudioClip powerGone;

	public int score;
	public int multiplier = 1;
	public float multiplierTimer = 0;
	public int lives = 3;
	public bool dead = false;
	public TMP_Text scoreBoard;
	public TMP_Text multiplierText;
	public TMP_Text multiplierX;

	public Image[] extraLives;

	public Image multiFill;

	public Image backgroundLayer1;
	public Image backgroundLayer2;
	public Image backgroundLayer3;

	float offset1 = 0f;
	float offset2 = 0f;
	float offset3 = 0f;

	public float speed1 = 1f;
	public float speed2 = 1f;
	public float speed3 = 1f;

	public bool hasMulti = false;

	public float extraLife = 10000;

	public int powerLevel = 1;

	public bool gameOver = false;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		//DontDestroyOnLoad(gameObject);

		EnemyController = GetComponent<EnemyController>();
		AudioController = GetComponent<AudioController>();
	}

	void Start()
	{
		score = 0;
		StartCoroutine(StartEnemy());
	}

	void Update()
	{
		scoreBoard.text = score.ToString("n0", new NumberFormatInfo { NumberGroupSeparator = " " });
		multiplierText.text = multiplier.ToString();

		if (dead && lives > 0)
		{
			dead = false;
			StartCoroutine(Respawn());
		}
		else if (lives <= 0)
			gameOver = true;

		if (gameOver)
		{
			if(Input.GetKey(KeyCode.Escape))
				SceneManager.LoadScene("Start Screen");

			//EnemyController.enabled = false;
			EnemyController.StopSpawn();

			gameOverScreen.SetActive(true);
		}


		multiplierTimer -= Time.deltaTime;
		multiFill.fillAmount = multiplierTimer / 10;

		if (multiplierTimer <= 0 && hasMulti)
		{
			hasMulti = false;
			AudioController.PlaySFX(powerGone);
			multiplier = 1;
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

		if (score > extraLife)
		{
			extraLife *= 1.5f;
			lives++;
		}
	}

	public void DoPower(int roll)
	{
		// if (roll < 6)
		// 	powerLevel = roll;
		// else
		// 	ExplodeScreen();

		multiplier = roll;
		if (roll > 1)
		{
			hasMulti = true;
			textAnimator.Play("MultiplierScale", -1, 0f);
		}

		ResetTimer();
	}

	void ResetTimer()
	{
		multiplierTimer = 10;
		if (multiplier == 1)
			multiplierTimer = 0;
	}

	IEnumerator Respawn()
	{
		yield return new WaitForSeconds(2);
		PlayerController.invinsible = true;

		PlayerController.gameObject.transform.position = new Vector3(0, -1.2f, 0);
		PlayerController.gameObject.SetActive(true);
	}

	IEnumerator StartEnemy()
	{
		yield return new WaitForSeconds(2);
		EnemyController.enabled = true;
	}

	void ExplodeScreen()
	{

	}
}
