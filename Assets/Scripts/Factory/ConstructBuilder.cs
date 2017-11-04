using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructBuilder : MonoBehaviour {
	public GameObject coinPrefab;
	private GameObject buildingSpace;

	private Draggable draggable;

	public bool canConstruct = true;
	private GameController gameController;

	void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		buildingSpace = GameObject.Find("BuildingSpace");
		draggable = transform.GetComponent<Draggable>();
	}

	void OnMouseUp(){
		canConstruct = draggable.drag != null && draggable.drag.GetComponent<DraggableController>().canConstruct;
		if(canConstruct){
			ConstructBuilding();
		}
		Object.Destroy(draggable.drag);	
	}

	void ConstructBuilding(){	

		Vector3 buildingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		buildingPos.z = -1;	
		
		BuildingType type = (BuildingType) System.Enum.Parse (typeof (BuildingType), transform.name);
		GameObject buildingPrefab = gameController.availableBuildings.Find(build => build.type == type).prefab;
		GameObject building = GameObject.Instantiate(buildingPrefab, buildingPos, transform.rotation);
		building.transform.localScale = transform.lossyScale;
		building.transform.parent = buildingSpace.transform;
		building.transform.localPosition = buildingPos;

	}
}
