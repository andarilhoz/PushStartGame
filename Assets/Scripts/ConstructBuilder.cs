using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructBuilder : MonoBehaviour {


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
		buildingPos.z = 0;

		building.transform.position = buildingPos;
		render.sprite = myRender.sprite;
		building.transform.localScale = transform.lossyScale;
	}
}
