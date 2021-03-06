﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructBuilder : MonoBehaviour {

	[Tooltip("Cor da HUD quando usuario tem dinheiro para comprar")]
	public Color enableColor;
	[Tooltip("Cor da HUD quando usuario nao tem dinheiro para comprar")]
	public Color disableColor;

	
	public Text price;

	private GameObject buildingSpace;
	private Draggable draggable;
	private GameController gameController;
	private BuildingType type;
	private Building buildingEntity;
	private SpriteRenderer spriteRenderer;
	public bool canConstruct = true;

	void Start()
	{
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		buildingSpace = GameObject.Find("BuildingSpace");
		type = (BuildingType) System.Enum.Parse (typeof (BuildingType), transform.name);
		buildingEntity = gameController.availableBuildings.Find(build => build.type == type);
		draggable = transform.GetComponent<Draggable>();
		GameController.onLoggedUserChange += ShowMeTheMoney;
		spriteRenderer = transform.GetComponent<SpriteRenderer>();
		price.text = buildingEntity.price.ToString();
	}

	void OnMouseUp(){
		canConstruct = draggable.drag != null && draggable.drag.GetComponent<DraggableController>().canConstruct;
		if(canConstruct){
			ConstructBuilding();
		}
		Object.Destroy(draggable.drag);	
	}

	void ConstructBuilding(){	
		if(gameController.GetMoney() < buildingEntity.price) return;

		gameController.SpendMoney(buildingEntity.price);
		
		Vector3 buildingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		buildingPos.z = -1;	
		GameObject building = GameObject.Instantiate(buildingEntity.prefab, buildingPos, transform.rotation);
		
		building.transform.parent = buildingSpace.transform;
		building.transform.localPosition = buildingPos;
	}

	void ShowMeTheMoney(){
		if(gameController.GetMoney() > buildingEntity.price){
			draggable.canDrag = true;
			spriteRenderer.color = enableColor;
		}else{
			spriteRenderer.color = disableColor;
			draggable.canDrag = false;
		}
	}
}
