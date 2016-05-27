using UnityEngine;
using System.Collections;
using UnityEditor; // Needed to make custom inpesctor/editor

[CustomEditor(typeof(camera_follow))] // additional buttons added beflow public parameters of script
public class editor_camera_follow : Editor {

	public override void OnInspectorGUI(){

		DrawDefaultInspector ();

		camera_follow cf = (camera_follow)target;

		if (GUILayout.Button("Set Min Cam Pos"))
			cf.SetMinCamPosition();

		
		if (GUILayout.Button("Set Max Cam Pos"))
			cf.SetMaxCamPosition();
	}

}
