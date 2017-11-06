using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSlider : MonoBehaviour {
	[Header("Objetos do Slider")]
    public GameObject fill;
    public GameObject background;

	[Header("Cores do Slider")]
	public Color fillColor;
	public Color backgroundColor;

	private float totalFill;
	void Start()
	{
		totalFill = background.transform.localScale.x;
		fill.GetComponent<SpriteRenderer>().color = fillColor;
		background.GetComponent<SpriteRenderer>().color = backgroundColor;
		UpdateBar(0f);
	}

	public void UpdateBar(float percent){
		fill.transform.localScale = new Vector2(totalFill*percent,fill.transform.localScale.y);
	}

}
