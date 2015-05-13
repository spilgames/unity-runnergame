using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PushNotifications : MonoBehaviour {
#if UNITY_ANDROID
	private AndroidJavaObject activityContext = null;
	private string projectNr = "428456783882";
	
	void Start(){

		if (activityContext == null) {
			using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				activityContext = activityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			}
			
			using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.spilgames.pushnotificationlibrary.RegistrationId")) {
				if (pluginClass != null) {
					pluginClass.CallStatic("registerDevice", new object[] { activityContext, projectNr});
				}
			}
//
//			Dictionary<string, string> values = new Dictionary<string, string>();
//			PixelData.LoadData();
//			values.Add("Coins", PixelData.coins.ToString());
//
//			using(AndroidJavaObject obj_HashMap = new AndroidJavaObject("java.util.HashMap")){
//
//				IntPtr method_Put = AndroidJNIHelper.GetMethodID(obj_HashMap.GetRawClass(), "put",
//				                                                 "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
//
//				object[] args = new object[2];
//				foreach(KeyValuePair<string, string> kvp in values){
//					using(AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key)){
//						using(AndroidJavaObject v = new AndroidJavaObject("java.lang.String", kvp.Value)){
//							args[0] = k;
//							args[1] = v;
//							AndroidJNI.CallObjectMethod(obj_HashMap.GetRawObject(),
//							                            method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
//						}
//					}
//				}
//
//				using(AndroidJavaClass pluginClass2 = new AndroidJavaClass("com.spilgames.pushnotificationlibrary.GameDataManager")) {
//
//					if(pluginClass2 != null){
//						pluginClass2.CallStatic("updateValues", new object[] { activityContext, obj_HashMap});
//					}
//
//				}
//
//			}

		}
	}
#endif
}
