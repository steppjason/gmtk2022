using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
	public Image backgroundLayer1;
	public Image backgroundLayer2;
	public Image backgroundLayer3;

	float offset1 = 0f;
	float offset2 = 0f;
	float offset3 = 0f;

	public float speed1 = 1f;
	public float speed2 = 1f;
	public float speed3 = 1f;

	void Update()
	{
		offset1 += Time.deltaTime * speed1;
		offset2 += Time.deltaTime * speed2;
		offset3 += Time.deltaTime * speed3;

		backgroundLayer1.material.SetTextureOffset("_MainTex", new Vector2(1.25f, offset1));
		backgroundLayer2.material.SetTextureOffset("_MainTex", new Vector2(0.5f, offset2));
		backgroundLayer3.material.SetTextureOffset("_MainTex", new Vector2(-2f, offset3));
	}
}
