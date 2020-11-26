//
//  KarteApp.m
//  KarteTrackerUnityPlugin
//
//  Created by kota.fujiwara on 2020/07/16.
//  Copyright © 2020 PLAID, inc. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Util.h"
@import KarteCore;
@import KarteRemoteNotification;

extern "C" {
    const char *KRTApp_getVisitorId() {
        NSString *visitorId = [KRTApp visitorId];
        RETURN_RETAINED_STR(visitorId)
    }

    const char *KRTApp_getAppKey() {
        NSString *appKey = [KRTApp appKey];
        RETURN_RETAINED_STR(appKey)
    }
    
    void KRTApp_optOut() {
        [KRTApp optOut];
    }
    
    void KRTApp_optIn() {
        [KRTApp optIn];
    }
    
    void KRTApp_renewVisitorId() {
        [KRTApp renewVisitorId];
    }
    
    void KRTApp_registerFCMToken(const char *token) {
        NSString *tokenStr = [NSString stringWithCString:token encoding:NSUTF8StringEncoding];
        [KRTApp registerFCMToken:tokenStr];
    }
}
