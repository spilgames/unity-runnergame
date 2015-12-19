//
//  Spil.h
//  Spil
//
//  Created by Martijn van der Gun on 10/1/15.
//  Copyright © 2015 Spil Games. All rights reserved.
//

#import <UIKit/UIKit.h>
//#import <Spil/PublicHeader.h>


//! Project version number for Spil.
FOUNDATION_EXPORT double SpilVersionNumber;

//! Project version string for Spil.
FOUNDATION_EXPORT const unsigned char SpilVersionString[];

// In this header, you should import all the public headers of your framework using statements like #import <Spil/PublicHeader.h>



@interface Spil : NSObject {
    
}


/**
 *  Initiates the API
 */
+(void)start;

/**
 *  Show debug logs
 *
 *  @param debugEnabled Enables or disables the debug logs printed
 */
+(void)debug:(BOOL)debugEnabled;

/**
 *  Track a basic named event
 *
 *  @param name The name of the event. Replace spaces with an underscore
 */
+(void) trackEvent:(NSString*)name;

/**
 *  Track a named events with a key / value object
 *
 *  @param name The name of the event. Replace spaces with an underscore
 *  @param params A key value dictionary holding the params
 */
+(void) trackEvent:(NSString*)name withParameters:(NSDictionary *)params;

/**
 *  Track a basic named event with a response
 *
 *  @param name  The name of the event. Replace spaces with an underscore
 *  @param block A block with response param that will be executed when the server sends a reponse on the tracked event
 */
+(void) trackEvent:(NSString*)name onResponse:(void (^)(id response))block;

/**
 *  Track a named event params and a response
 *
 *  @param name   The name of the event. Replace spaces with an underscore
 *  @param params A key value dictionary holding the params
 *  @param block  A block with response param that will be executed when the server sends a reponse on the tracked event
 */
+(void) trackEvent:(NSString*)name withParameters:(NSDictionary *)params onResponse:(void (^)(id response))block;

/**
 *  Forwarding Delegate methods to let the Spil framework know when the app went to the background
 *
 *  @param application Delegate application to be passed
 */
+(void)applicationDidEnterBackground:(UIApplication *)application;

/**
 *  Forwarding Delegate methods to let the Spil framework know when the app became active again after running in background
 *
 *  @param application Delegate application to be passed
 */
+(void)applicationDidBecomeActive:(UIApplication *)application;

/**
 *  Unity message sender
 *
 *  @param messageName     Name of the message, should match the function in unity
 *  @param objectName      The name of the spil object where the script is attached to. In most cases "SpilSDK"
 *  @param parameterString A json string holding the data to send
 */
+(void)sendMessage:(NSString*)messageName toObject:(NSString*)objectName withParameter:(NSString*)parameterString;

/**
 *  Unity message listener
 *
 *  @param obj    The name of the spil object where the script is attached to. In most cases "SpilSDK"
 *  @param method Name of the message, should match the function in the iOS SDK
 *  @param msg    A json string holding the data being send
 */
void UnitySendMessage(const char* obj, const char* method, const char* msg);


/**
 *  Helper function to register for push notifications
 */
+(void)registerPushNotifications;

/**
 *  Helper function to forward the app delegate listener on the deviceToken
 */
+(void)didRegisterForRemoteNotificationsWithDeviceToken:(NSData*)deviceToken;


@end

