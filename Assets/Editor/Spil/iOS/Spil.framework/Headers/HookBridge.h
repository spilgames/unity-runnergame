//
//  HookBridge.h
//  SpilStaticLib
//
//  Created by Martijn van der Gun on 6/22/15.
//  Copyright (c) 2015 Martijn van der Gun. All rights reserved.
//

#ifdef __cplusplus

extern "C" {
    
    void initEventTracker();
    
    void trackEventNative(const char* eventName);
    
    void trackEventWithParamsNative(const char* eventName, const char* jsonStringParams);
    
    void handlePushNotification(const char* notificationStringParams);
    
    void SendUnityBridgeMessage(const char* objectName,const char* messageName,const char* parameterString);
    
    void UnitySendMessage(const char* obj, const char* method, const char* msg);

    void registerForPushNotifications(const char* obj);
    
    void applicationDidEnterBackground();
    
    void applicationDidBecomeActive();
    
    void setPushNotificationKey(const char* pushKey);
}

#endif