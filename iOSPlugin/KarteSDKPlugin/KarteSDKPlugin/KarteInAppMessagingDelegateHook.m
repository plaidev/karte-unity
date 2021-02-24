//
//  KarteInAppMessagingDelegateHook.m
//  KarteSDKPlugin
//
//  Created by kota.fujiwara on 2021/02/18.
//  Copyright © 2021 PLAID, inc. All rights reserved.
//

#import "KarteInAppMessagingDelegateHook.h"

void UnitySendMessage(const char *obj, const char *method, const char *msg);

static KarteInAppMessagingDelegateHook *shared = nil;

@implementation KarteInAppMessagingDelegateHook

+ (KarteInAppMessagingDelegateHook *)shared
{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        shared = [[self alloc] init];
    });
    
    return shared;
}

- (BOOL)inAppMessaging:(KRTInAppMessaging *)inAppMessaging shouldOpenURL:(NSURL *)url
{
    if(self.openUrlHandlerEnabled) {
        [self invokeOpenUrlCallback: url];
        return NO;
    }
    return YES;
}

- (BOOL)inAppMessaging:(KRTInAppMessaging *)inAppMessaging shouldOpenURL:(NSURL *)url onScene:(UIScene *)scene API_AVAILABLE(ios(13))
{
    if(self.openUrlHandlerWithSceneEnabled) {
        [self invokeCallback:url onScene:scene];
        return NO;
    }
    
    if(self.openUrlHandlerEnabled) {
        [self invokeOpenUrlCallback:url];
        return NO;
    }
    
    return YES;
}

- (void)invokeOpenUrlCallback:(NSURL *)url
{
    NSString* urlString = [url absoluteString];
    const char *target = strdup([self.callbackTargetName UTF8String]);
    UnitySendMessage(target, "InAppMessagingOpenUrlCallback", strdup([urlString UTF8String]));
}

- (void)invokeCallback:(NSURL *)url onScene:(UIScene *)scene API_AVAILABLE(ios(13))
{
    NSString* urlString = [url absoluteString];
    const char *target = strdup([self.callbackTargetName UTF8String]);
        
    NSDictionary *dict = @{@"url": urlString, @"sceneIdentifier": scene.session.persistentIdentifier};
    if(![NSJSONSerialization isValidJSONObject:dict]) {
        return;
    }
    NSData *data = [NSJSONSerialization dataWithJSONObject:dict options:0 error:nil];
    if(data && ![data isKindOfClass:[NSNull class]]) {
        NSString *serializedArguments = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        UnitySendMessage(target, "InAppMessagingOpenUrlWithSceneCallback", strdup([serializedArguments UTF8String]));
    }
}

@end
