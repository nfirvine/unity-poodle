using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography.X509Certificates;
using System.Security;
using System.Net.Security;
using System.Net;
using Mono.Security;

public class UnityPoodle : MonoBehaviour {
	
	public void Start () {
		Debug.Log ("Connecting without mitigation");
		bool unpwned = MyTest (false);
		if (unpwned) {
			Debug.Log ("Everything went fine.");
		} else {
			Debug.LogError ("You got pwned.");
		}
		Debug.Log ("Connection with mitigation");
		unpwned = MyTest (true);
		if (unpwned) {
			Debug.Log ("Everything went fine.");
		} else {
			Debug.LogError ("You got pwned.");
		}
	}
	
	public bool MyTest (bool disableSsl3) {
		String url = "https://www.ssllabs.com:10300/";
		bool unpwned = false;
		SecurityProtocolType oldprot = System.Net.ServicePointManager.SecurityProtocol;
		if (disableSsl3) {
			//This TLS is actually TLS1.0
			System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
		}
		HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
		HttpWebResponse resp;
		try {
			//This will throw an exception with SSL3 disabled, since we can't find an appropriate TLS version
			resp = req.GetResponse() as HttpWebResponse;
		} catch (WebException ex) {
			if (disableSsl3) {
				unpwned = true;
			}
		} 
		if (disableSsl3) {
			System.Net.ServicePointManager.SecurityProtocol = oldprot;
		}
		return unpwned;
	}
}
