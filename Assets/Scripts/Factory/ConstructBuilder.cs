﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructBuilder : MonoBehaviour {
	private GameObject buildingSpace;

	void Start()
	{
		buildingSpace = GameObject.Find("BuildingSpace");
	}

	void OnMouseUp()
	{
		ConstructBuilding();
	}

	void ConstructBuilding(){
		GameObject building = new GameObject(transform.name);
		building.AddComponent<SpriteRenderer>();
		building.AddComponent<BuildingController>();
		SpriteRenderer render = building.GetComponent<SpriteRenderer>();
		SpriteRenderer myRender = transform.GetComponent<SpriteRenderer>();

		

		Vector3 buildingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		buildingPos.z = -1;	

		
		render.sprite = myRender.sprite;
		building.transform.localScale = transform.lossyScale;
		building.transform.parent = buildingSpace.transform;
		building.transform.localPosition = buildingPos;
	}
}