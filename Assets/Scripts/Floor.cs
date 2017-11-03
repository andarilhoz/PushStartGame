using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
	private Vector2 initialPos;
	void OnMouseDown()
	{
		initialPos = transform.position;
	}
	void Update(){
		
	}
	void OnMouseDrag()
	{
		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition - Input.mousePosition);
		Vector2 finalPos = initialPos + pos;
		transform.position = new Vector3(finalPos.x, finalPos.y, transform.position.z);
	}
}
