#import <Foundation/Foundation.h>
#import "Util.h"
@import KarteCore;
@import KarteRemoteNotification;

extern "C" {
    int KRTRemoteNotification_handle(const char *serializedUserInfo) {
        NSDictionary *dict = [Util dictionaryWithSerializedString:serializedUserInfo];
        KRTRemoteNotification *notification = [[KRTRemoteNotification alloc] initWithUserInfo:dict];
        return [notification handle];
    }
    
    const char *KRTRemoteNotification_url(const char *serializedUserInfo) {
        NSDictionary *dict = [Util dictionaryWithSerializedString:serializedUserInfo];
        KRTRemoteNotification *notification = [[KRTRemoteNotification alloc] initWithUserInfo:dict];
        NSURL *url = [notification url];
        if(url) {
            RETURN_RETAINED_STR([url absoluteString])
        }
        RETURN_RETAINED_STR(@"")
    }
}
