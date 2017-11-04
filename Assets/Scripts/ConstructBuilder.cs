using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructBuilder : MonoBehaviour {
	private GameObject buildingSpace;

	void Start()
	{
		buildingSpace = GameObject.Find("Buildings");
	}

	void OnMouseUp()
	{
		ConstructBuilding();
	}

	void ConstructBuilding(){
		GameObject building = new GameObject(transform.name + " constructed");
		building.AddComponent<SpriteRenderer>();
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
