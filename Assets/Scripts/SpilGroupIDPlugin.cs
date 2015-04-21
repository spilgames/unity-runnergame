using UnityEngine;
using System.Collections;
// We need this one for importing our IOS functions
using System.Runtime.InteropServices;

public class SpilGroupIDPlugin : MonoBehaviour
{
	// Use this #if so that if you run this code on a different platform, you won't get errors.
	#if UNITY_IPHONE
	// For the most part, your imports match the function defined in the iOS code, except char* is replaced with string here so you get a C# string.    
	[DllImport ("__Internal")]
	private static extern string _getGroupUserID();
	
	#endif
	

	
	public static string GetGroupUserID()
	{
		string userID = "";
		// We check for UNITY_IPHONE again so we don't try this if it isn't iOS platform.
		#if UNITY_IPHONE
		// Now we check that it's actually an iOS device/simulator, not the Unity Player. You only get plugins on the actual device or iOS Simulator.
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			userID = _getGroupUserID();
		}
		#endif

		return userID;
	}
	
}