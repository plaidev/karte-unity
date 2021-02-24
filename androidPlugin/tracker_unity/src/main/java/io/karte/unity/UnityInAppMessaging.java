package io.karte.unity;

import io.karte.android.inappmessaging.InAppMessaging;

class UnityInAppMessaging {
    private static void dismiss() {
        InAppMessaging.dismiss();
    }

    private static boolean isPresenting() {
        return InAppMessaging.isPresenting();
    }

    private static void suppress() {
        InAppMessaging.suppress();
    }

    private static void unsuppress() {
        InAppMessaging.unsuppress();
    }

    private static void enableOpenUrlHandler(String url) {
        UnityInAppMessagingDelegateHook.getInstance().enableOpenUrlHandler(url);
    }

    private static void disableOpenUrlHandler() {
        UnityInAppMessagingDelegateHook.getInstance().disableOpenUrlHandler();
    }
}
