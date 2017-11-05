using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float width;
    public float dragSpeed = 2;
	public  SpriteRenderer spriteBounds;
	public bool dragInvert = false;


    private Vector3 dragOrigin;
	private float rightBound;
	private float leftBound;
	private float topBound;
	private float bottomBound;
	private Vector3 pos;
	private Resolution resolution;

 	void Start()
	{
		resolution = Screen.currentResolution;
		CalculateBonds();
	}
    void Update()
    {
		if(Draggable.dragging) return;
		if(Input.touchCount == 2) return;

		if(!resolution.Equals(Screen.currentResolution)){
			CalculateBonds();
			resolution = Screen.currentResolution;
		}		

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
 
        if (!Input.GetMouseButton(0)) return;
 
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		
		if(dragInvert){
			pos.x = pos.x * -1;
			pos.y = pos.y * -1;
		}

       	Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);
        transform.Translate(move, Space.World);  
		transform.position = PosCorrectionLimit(transform.position);
    }

	private Vector3 PosCorrectionLimit(Vector3 pos){
		pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
		pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
		pos.z = -10;
		return pos;
	}
	public void CalculateBonds()
	{
		float vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;  
		float horzExtent = vertExtent * Screen.width / Screen.height;
		
		topBound = (float)(spriteBounds.sprite.bounds.size.y * spriteBounds.transform.localScale.y / 2.0f - vertExtent);
		leftBound = (float)(horzExtent - spriteBounds.sprite.bounds.size.x * spriteBounds.transform.localScale.x / 2.0f) ;
		rightBound = (float)(spriteBounds.sprite.bounds.size.x * spriteBounds.transform.localScale.x / 2.0f - horzExtent) ;
		bottomBound = (float)(vertExtent - spriteBounds.sprite.bounds.size.y * spriteBounds.transform.localScale.y / 2.0f) ;
		transform.position = PosCorrectionLimit(transform.position);
	}
 
}
