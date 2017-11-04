using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DraggableController : MonoBehaviour {
	public bool canConstruct = true;
	
	void OnTriggerStay2D(Collider2D other)
	{
		if(!canConstruct || other.tag != "Building") return;
		Debug.Log("colidiu");
		canConstruct = false;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag != "Building") return;
		Debug.Log("n colide mais");
		canConstruct = true;
	}

}
