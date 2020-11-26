//
//  KarteAppDelegateNotificationHandler.m
//  KarteTrackerUnityPlugin
//
//  Created by kota.fujiwara on 2019/08/01.
//  Copyright © 2019 PLAID, inc. All rights reserved.
//

#import "KarteAppDelegateNotificationHandler.h"
#import "KarteSetupUtil.h"

static KarteAppDelegateNotificationHandler *sharedInstance = nil;

@implementation KarteAppDelegateNotificationHandler

+(void)load
{
    sharedInstance = [[KarteAppDelegateNotificationHandler alloc] init];
    [[NSNotificationCenter defaultCenter] addObserver:sharedInstance
                                             selector:@selector(applicationDidFinishLaunching:)
                                                 name:UIApplicationDidFinishLaunchingNotification
                                               object:nil];
}

-(void)applicationDidFinishLaunching:(NSNotification*) notification
{
    [KarteSetupUtil setupWithConfigFile];
}

@end

