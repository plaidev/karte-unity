package io.karte.unity;

import io.karte.android.core.usersync.UserSync;

class UnityUserSync {
    private static String appendUserSyncQueryParameter(String url) {
        return UserSync.appendUserSyncQueryParameter(url);
    }

    private static String getUserSyncScript() {
        return UserSync.getUserSyncScript();
    }
}