﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
	public static bool dragging = false;
	public GameObject drag = null;

	private GameController gameController;
	public bool canDrag = true;

	void Start(){
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

	void OnMouseDown()
	{
		if(drag == null && canDrag){
			drag = CreateDragObject();
			MoveDragToCursor();
		}
	}
	void OnMouseDrag()
	{
		dragging = true;
		if(!canDrag) return;
		MoveDragToCursor();
	}

	void OnMouseUp()
	{
		dragging = false;
	}

	GameObject CreateDragObject(){
		GameObject dragable = new GameObject("Drag");
		BuildingType type = (BuildingType) System.Enum.Parse (typeof (BuildingType), transform.name);
		Building buildType = gameController.availableBuildings.Find(build => build.type == type);
		
		SpriteRenderer render = dragable.AddComponent<SpriteRenderer>();
		BoxCollider2D collider  = dragable.AddComponent<BoxCollider2D>(); 
		SpriteRenderer myRender = transform.GetComponent<SpriteRenderer>();
		dragable.AddComponent<DraggableController>();

		collider.isTrigger = true;
		collider.size = new Vector2(2.5f, 2.5f);
		render.sortingLayerName ="Frontlayer";
		render.sprite = myRender.sprite;
		dragable.transform.localScale = buildType.prefab.transform.lossyScale;
		return dragable;
	}

	void MoveDragToCursor(){
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 1;
		drag.transform.position = pos;
	}
}
