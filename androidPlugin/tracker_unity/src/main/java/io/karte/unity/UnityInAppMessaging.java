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
}
