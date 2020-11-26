#import <Foundation/Foundation.h>
@import KarteCore;
@import KarteInAppMessaging;

extern "C" {
    int KRTInAppMessaging_presenting() {
        return (int)[[KRTInAppMessaging shared] isPresenting];
    }

    void KRTInAppMessaging_dismiss() {
        [[KRTInAppMessaging shared] dismiss];
    }

    void KRTInAppMessaging_suppress() {
        [[KRTInAppMessaging shared] suppress];;
    }

    void KRTInAppMessaging_unsuppress() {
        [[KRTInAppMessaging shared] unsuppress];;
    }
}
