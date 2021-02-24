#import <Foundation/Foundation.h>
@import KarteCore;
@import KarteInAppMessaging;
#import "KarteInAppMessagingDelegateHook.h"

extern "C" {
    int KRTInAppMessaging_presenting() {
        return (int)[[KRTInAppMessaging shared] isPresenting];
    }

    void KRTInAppMessaging_dismiss() {
        [[KRTInAppMessaging shared] dismiss];
    }

    void KRTInAppMessaging_suppress() {
        [[KRTInAppMessaging shared] suppress];
    }

    void KRTInAppMessaging_unsuppress() {
        [[KRTInAppMessaging shared] unsuppress];
    }

    void KRTInAppMessaging_enableOpenUrlHandler(const char *callbackTargetName) {
        NSString *callbackTargetNameStr = [NSString stringWithCString:callbackTargetName encoding:NSUTF8StringEncoding];
        [KarteInAppMessagingDelegateHook shared].openUrlHandlerEnabled = YES;
        [KarteInAppMessagingDelegateHook shared].callbackTargetName = callbackTargetNameStr;
    }

    void KRTInAppMessaging_disableOpenUrlHandler() {
        [KarteInAppMessagingDelegateHook shared].openUrlHandlerEnabled = NO;
    }

    void KRTInAppMessaging_enableOpenUrlWithSceneHandler(const char *callbackTargetName) {
        NSString *callbackTargetNameStr = [NSString stringWithCString:callbackTargetName encoding:NSUTF8StringEncoding];
        [KarteInAppMessagingDelegateHook shared].openUrlHandlerWithSceneEnabled = YES;
        [KarteInAppMessagingDelegateHook shared].callbackTargetName = callbackTargetNameStr;
    }

    void KRTInAppMessaging_disableOpenUrlWithSceneHandler() {
        [KarteInAppMessagingDelegateHook shared].openUrlHandlerWithSceneEnabled = NO;
    }

    void KRTInAppMessaging_defaultOpenUrl(const char *urlChars) {
        NSString *urlStr = [NSString stringWithCString:urlChars encoding:NSUTF8StringEncoding];
        NSURL *url = [NSURL URLWithString:urlStr];
        if (@available(iOS 10.0, *)) {
            [[UIApplication sharedApplication] openURL:url options:@{} completionHandler:nil];
        } else {
            #pragma clang diagnostic push
            #pragma clang diagnostic ignored "-Wdeprecated-declarations"
            [[UIApplication sharedApplication] openURL:url];
            #pragma clang diagnostic pop
        }
    }
}
