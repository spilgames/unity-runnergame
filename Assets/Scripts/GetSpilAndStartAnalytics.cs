using UnityEngine;
using System.Collections;

public class GetSpilAndStartAnalytics : MonoBehaviour {

	public GoogleAnalyticsV3 analytics;


	// Use this for initialization
	void Awake () {
	
#if UNITY_IOS
		analytics.SetUserIDOverride(SpilGroupIDPlugin.GetGroupUserID());
		analytics.StartAnalytics();
#else
		analytics.StartAnalytics();
#endif


	}
}
