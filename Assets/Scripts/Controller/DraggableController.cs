using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DraggableController : MonoBehaviour {
	public bool canConstruct = true;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		canConstruct = false;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		canConstruct = true;
	}

}
