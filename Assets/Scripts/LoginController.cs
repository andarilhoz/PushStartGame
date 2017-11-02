using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginController : MonoBehaviour {

	public string APIUrl;
	public string loginPath;

	public int successStatus;

	public InputField username;
	public InputField password;

	public void Login(){
		Login login = new Login();
		login.username = username.text;
		login.password = Crypto.SHA256Hash(password.text);
		string loginJson = JsonUtility.ToJson(login);

		StartCoroutine(LoginPost(loginJson));
	}

	private IEnumerator LoginPost(string bodyJsonString){
		string fullURL = APIUrl+loginPath;
		UnityWebRequest request = RequestMaker("POST", fullURL, bodyJsonString);
		yield return request.SendWebRequest();
		ResponseHandler(request);
	}

	private UnityWebRequest RequestMaker( string type, string url, string body){
		UnityWebRequest request = new UnityWebRequest(url, type);
		byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(body);
		request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
		return request;
	}

	private void ResponseHandler(UnityWebRequest response){
		if(response.isNetworkError){
			Debug.LogError("Ocorreram erros ao realizer o request: " + response.error);
		}
		else{
			Debug.Log("Response: "+ response.downloadHandler.text);
			if(response.responseCode == successStatus){
				Debug.Log("Usuario logado com sucesso");
			}
			else{
				Debug.Log("Erro ao logar");
			}
		}
	}
}
