using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

#if UNITY_IOS

public static class XCodePostProcess
{
	[PostProcessBuild(5000)]
	public static void OnPostProcessBuild( BuildTarget target, string path )
	{
		// Create a new project object from build target
		UnityEditor.XCodeEditor.XCProject project = new UnityEditor.XCodeEditor.XCProject( path );
		
		// Find and run through all projmods files to patch the project
		string projModPath = System.IO.Path.Combine(Application.dataPath, "Editor");

		var files = System.IO.Directory.GetFiles( projModPath, "*.projmods", SearchOption.AllDirectories );
		foreach( var file in files ) {
			project.ApplyMod( file );
		}
		
		// Finally save the xcode project
		project.Save();
	}
}

#endif
