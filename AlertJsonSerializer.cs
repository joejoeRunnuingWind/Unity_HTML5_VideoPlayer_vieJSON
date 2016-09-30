using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class AlertJsonSerializer: MonoBehaviour{

	public int tagNumber;
	public DateTime startTime;

	public void ConvertJson(){
		foreach (string name in Enum.GetNames(typeof(TypeOfAlert))) {
			for (int i = 0; i < tagNumber; i++) {
				if (name != "StatusRecord") {
					string s = @"c:\temp\logs\" + name + i.ToString ();
					ConvertFileToAlertMessageObject (s);
				}
			}
		}
	}
	void ConvertFileToAlertMessageObject(string s){
		if (File.Exists (s)) {
			if (File.Exists (s + ".json")) {
				File.Delete (s + ".json");
			}
			FileStream fs = new FileStream (s, FileMode.Open, FileAccess.Read, FileShare.Write);
			StreamReader sr = new StreamReader (fs);
			FileStream fsjson = new FileStream (s + ".json", FileMode.Append, FileAccess.Write, FileShare.Write);
			StreamWriter sw = new StreamWriter (fsjson);
			sw.WriteLine ("[");
			sw.Flush ();
			string tempString = sr.ReadLine ();
			while (tempString != null) {
				string[] temp1 = tempString.Split (new char[]{ ',' });
				Point p = new Point (Convert.ToInt16 (temp1 [4]), new Vector2 (float.Parse (temp1 [5]), float.Parse (temp1 [6])), new Vector3 (float.Parse (temp1 [7]), float.Parse (temp1 [8]), float.Parse (temp1 [9])));
				AlertMessage al = new AlertMessage (Convert.ToInt16(temp1[0]), Convert.ToInt16(temp1[1]), Convert.ToInt16(temp1[2]), CurrentTime(temp1[3]).ToString(), p);
				string json = JsonUtility.ToJson (al);
				sw.Write(json);
				sw.Flush ();
				tempString = sr.ReadLine();
				if (tempString != null) {
					sw.WriteLine (",");
					sw.Flush ();
				} else {
					sw.WriteLine ("");
					sw.Flush ();
				}
			}
			sw.WriteLine ("]");
			sw.Flush ();
			sw.Dispose ();
		}
	}

	public void InitializeStartTime(){
		startTime = new DateTime (2016, 8, 29, 10, 14, 0, DateTimeKind.Local);
	}

	double CurrentTime(string s){
		InitializeStartTime ();
		TimeSpan diff = Convert.ToDateTime (s) - startTime;
		//Debug.Log (Convert.ToDateTime (s));
		return Mathf.Floor ((float)diff.TotalSeconds);
	}

}
