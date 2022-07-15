using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	public static GameController Instance { get; private set; }

	PlayerController PlayerController;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		PlayerController = FindObjectOfType<PlayerController>();
	}

	void Start()
	{

	}

	void Update()
	{

	}
}
