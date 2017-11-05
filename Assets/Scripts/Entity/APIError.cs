using System.Collections.Generic;

public class APIError {

	public int code;
	public string message;
	public Fields fields;
}

public class Fields {
	public string[] password;
	public string[] username;
	
}