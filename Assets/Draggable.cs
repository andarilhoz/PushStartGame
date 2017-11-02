using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
	
	void OnMouseDrag()
	{
		Debug.Log("Dragging: " + this.name);	
	}
}
