//
//  KarteTrackerSetupUtil.m
//  KarteTrackerUnityPlugin
//
//  Created by kota.fujiwara on 2019/08/01.
//  Copyright © 2019 PLAID, inc. All rights reserved.
//

#import "KarteSetupUtil.h"
#import "KarteInAppMessagingDelegateHook.h"
@import KarteCore;
@import KarteInAppMessaging;

@implementation KarteSetupUtil
+ (void)setupWithConfigFile
{
    NSDictionary *configDict = [self readConfigFromFile];
    if(!configDict) {
        return;
    }
    NSString *appKey = (NSString *)[configDict objectForKey: @"appKey"];
    KRTConfiguration *config = [self configWithDictionary:configDict];
    [KRTApp setupWithAppKey:appKey configuration:config];
    
    [KRTInAppMessaging shared].delegate = [KarteInAppMessagingDelegateHook shared];
}

+ (KRTConfiguration *) configWithDictionary: (NSDictionary *)dict {
    KRTConfiguration *config = [KRTConfiguration defaultConfiguration];
    if ([dict objectForKey:@"isDryRun"]) {
        config.isDryRun = [[dict objectForKey:@"isDryRun"] boolValue];
    }
    if ([dict objectForKey:@"isOptOut"]) {
        config.isOptOut = [[dict objectForKey:@"isOptOut"] boolValue];
    }
    return config;
}

+ (NSDictionary *)readConfigFromFile
{
    NSString *bundlePath = [[NSBundle mainBundle] bundlePath];
    NSString *streamingAssetsPath = [NSString stringWithFormat:@"%@/Data/Raw", bundlePath];
    NSString *configFilePath = [streamingAssetsPath stringByAppendingPathComponent:@"karte_tracker_config.json"];

    NSError *error;
    NSString *contents = [NSString stringWithContentsOfFile:configFilePath encoding:NSUTF8StringEncoding error:&error];
    if (error || !contents) {
        NSLog(@"failed to read from %@", configFilePath);
        return nil;
    }

    NSData *data = [contents dataUsingEncoding:NSUTF8StringEncoding];
    NSDictionary *json = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:nil];

    return json;
}
@end
