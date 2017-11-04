using System;
using UnityEngine;

[Serializable]
public class Building  {
	public GameObject prefab;
	public BuildingType type;
	public float timeToBuild;
	public int moneyPerTime;
	public int price;
	public float timeToProfit;

}
