using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	public float cameraDistanceMax = 13f;
	public float cameraDistanceMin = 5f;
	public float cameraDistance = 5f;
	public float scrollSpeed = 2f;

	private Camera mainCamera;

	private CameraController cameraController;
	void Start () {
		mainCamera = transform.GetComponent<Camera>();
		cameraController = transform.GetComponent<CameraController>();
	}
 
	void Update()
	{
		if(Input.touchCount != 2 && Input.GetAxis("Mouse ScrollWheel") == 0) return;
		
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff =  touchDeltaMag - prevTouchDeltaMag;
			cameraDistance +=  deltaMagnitudeDiff * (scrollSpeed -1.5f);

		}else{
			cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;			
		}
		
		cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);
		
		mainCamera.orthographicSize = cameraDistance;
		cameraController.CalculateBonds();
	}
}

