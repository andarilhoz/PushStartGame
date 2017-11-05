using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginErrorFeedbackController : MonoBehaviour {
	public int timeDisplayError = 3;
	private Text errorText;
	void Start(){
		errorText =transform.GetComponent<Text>();
	}
	public void SetText(string errorMessage){
		errorText.text = errorMessage;
		errorText.enabled = true;
		StartCoroutine(Hide());
	}

	IEnumerator Hide(){
		yield return new WaitForSeconds(timeDisplayError);
		errorText.enabled = false;
	}
}
