using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {

	public static bool dragging = false;
	private GameObject drag = null;
	void OnMouseDown()
	{
		if(drag == null){
			drag = CreateDragObject();
			MoveDragToCursor();
		}
	}
	void OnMouseDrag()
	{
		dragging = true;
		MoveDragToCursor();
	}

	void OnMouseUp()
	{
		dragging = false;
		Object.Destroy(drag);		
	}

	GameObject CreateDragObject(){
		GameObject dragable = new GameObject("Drag");
		dragable.AddComponent<SpriteRenderer>();
		SpriteRenderer render = dragable.GetComponent<SpriteRenderer>();
		SpriteRenderer myRender = transform.GetComponent<SpriteRenderer>();

		render.sprite = myRender.sprite;
		dragable.transform.localScale = transform.lossyScale;
		return dragable;
	}

	void MoveDragToCursor(){
		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		drag.transform.position = pos;
	}
}
