//
//  NativeMethods.mm
//  Unity-iPhone
//
//  Created by kota.fujiwara on 2019/03/18.
//
#import <Foundation/Foundation.h>
#import "Util.h"
@import KarteCore;
@import KarteRemoteNotification;

extern "C" {
    void KRTTracker_view(const char *viewName) {
        NSString *viewNameStr = [NSString stringWithCString:viewName encoding:NSUTF8StringEncoding];
        [KRTTracker view:viewNameStr];
    }

    void KRTTracker_viewWithTitle(const char *viewName, const char *title) {
        NSString *viewNameStr = [NSString stringWithCString:viewName encoding:NSUTF8StringEncoding];
        NSString *titleStr = [NSString stringWithCString:title encoding:NSUTF8StringEncoding];
        [KRTTracker view:viewNameStr title:titleStr];
    }

    void KRTTracker_viewWithTitleAndValues(const char *viewName, const char *title, const char *serializedValues) {
        NSString *viewNameStr = [NSString stringWithCString:viewName encoding:NSUTF8StringEncoding];
        NSString *titleStr = [NSString stringWithCString:title encoding:NSUTF8StringEncoding];
        NSDictionary *dict = [Util dictionaryWithSerializedString:serializedValues];
        [KRTTracker view:viewNameStr title:titleStr values:dict];
    }

    void KRTTracker_track(const char *eventName) {
        NSString *eventNameStr = [NSString stringWithCString:eventName encoding:NSUTF8StringEncoding];
        [KRTTracker track:eventNameStr];
    }

    void KRTTracker_trackWithValues(const char *eventName, const char *serializedValues) {
        NSString *eventNameStr = [NSString stringWithCString:eventName encoding:NSUTF8StringEncoding];
        NSDictionary *dict = [Util dictionaryWithSerializedString: serializedValues];
        [KRTTracker track:eventNameStr values:dict];
    }

    void KRTTracker_identify(const char *serializedValues) {
        NSDictionary *dict = [Util dictionaryWithSerializedString: serializedValues];
        [KRTTracker identify:dict];
    }

    void KRTTracker_trackClick(const char *serializedValues) {
        NSDictionary *userInfo = [Util dictionaryWithSerializedString: serializedValues];
        [KRTTracker trackClickWithUserInfo:userInfo];
    }
}
