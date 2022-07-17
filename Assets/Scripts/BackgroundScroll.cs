using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{

	public float scrollSpeed;

	Transform cameraTransform;
	Vector3 lastCameraPosition;
	float textureUnitSizeY;

	void Start()
	{
		cameraTransform = Camera.main.transform;
		lastCameraPosition = cameraTransform.position;
		Sprite sprite = GetComponent<SpriteRenderer>().sprite;
		Texture2D texture = sprite.texture;
		textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
	}

	void Update()
	{
		transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
		if(transform.position.y <= -textureUnitSizeY)
			transform.position = new Vector3(0, 0, 0);
	}
}
