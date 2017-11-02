using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour {

	public InputField username;
	public InputField password;

	public void Login(){
		Debug.Log(username);
		Debug.Log(password);
	}
}
