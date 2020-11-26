package io.karte.unity;

import android.app.Activity;
import com.unity3d.player.UnityPlayer;

import io.karte.android.core.usersync.UserSync;

class UnityUserSync {
    private static String appendUserSyncQueryParameter(String url) {
        return UserSync.appendUserSyncQueryParameter(url);
    }
}