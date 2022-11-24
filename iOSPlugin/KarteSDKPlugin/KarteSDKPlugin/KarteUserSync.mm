//
//  KarteTrackerJsUtil.m
//  Unity-iPhone
//
//  Created by kota.fujiwara on 2019/03/22.
//

#import <Foundation/Foundation.h>
#import "Util.h"
@import KarteCore;

extern "C" {
    const char *KRTUserSync_stringByAppendingUserSyncQueryParameter(const char *url) {
        NSString *urlStr = [NSString stringWithCString:url encoding:NSUTF8StringEncoding];
        NSString *str = [KRTUserSync appendingQueryParameterWithURLString:urlStr];
        RETURN_RETAINED_STR(str)
    }

    const char *KRTUserSync_getUserSyncScript() {
        NSString *str = [KRTUserSync getUserSyncScript];
        RETURN_RETAINED_STR(str)
    }
}
