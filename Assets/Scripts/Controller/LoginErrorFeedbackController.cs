using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginErrorFeedbackController : MonoBehaviour {

	[Tooltip("Tempo em que a mensagem de erro irá aparecer na tela")]
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
