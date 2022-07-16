using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePool : MonoBehaviour
{
	[SerializeField] int numberOfDice = 20;
	[SerializeField] Dice die;

	Dice[] _dice;
	int _next = 0;

	void Awake()
	{
		InstantiateEnemys();
	}

	void InstantiateEnemys()
	{
		_dice = new Dice[numberOfDice];
		for (int i = 0; i < numberOfDice; i++)
		{
			_dice[i] = Instantiate(die, new Vector3(0, 0, 0), Quaternion.identity);
			_dice[i].transform.parent = gameObject.transform;
			_dice[i].gameObject.SetActive(false);
		}
	}

	public void SetEnemyActive(Vector3 position)
	{
		GetAvailable();
		Dice newDie = _dice[_next];
		newDie.transform.position = position;
		newDie.gameObject.SetActive(true);
	}

	void GetAvailable()
	{
		for (int i = 0; i < _dice.Length; i++)
		{
			if (!_dice[i].gameObject.activeInHierarchy)
			{
				_next = i;
				return;
			}
		}
	}
}
