//
//  KarteInAppMessagingDelegateHook.h
//  KarteSDKPlugin
//
//  Created by kota.fujiwara on 2021/02/18.
//  Copyright © 2021 PLAID, inc. All rights reserved.
//

#import <Foundation/Foundation.h>
@import KarteInAppMessaging;

NS_ASSUME_NONNULL_BEGIN

@interface KarteInAppMessagingDelegateHook : NSObject<KRTInAppMessagingDelegate>
+ (KarteInAppMessagingDelegateHook *)shared;
@property (atomic, copy) NSString* callbackTargetName;
@property (nonatomic) BOOL openUrlHandlerEnabled;
@property (nonatomic) BOOL openUrlHandlerWithSceneEnabled;
- (BOOL) inAppMessaging:(KRTInAppMessaging *)inAppMessaging shouldOpenURL:(NSURL *)url;
- (BOOL) inAppMessaging:(KRTInAppMessaging *)inAppMessaging shouldOpenURL:(NSURL *)url onScene:(UIScene *)scene API_AVAILABLE(ios(13));
@end

NS_ASSUME_NONNULL_END
