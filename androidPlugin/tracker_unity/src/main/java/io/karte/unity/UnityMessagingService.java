package io.karte.unity;

import com.google.firebase.messaging.RemoteMessage;
import com.google.firebase.messaging.cpp.ListenerService;

import io.karte.android.core.logger.Logger;
import io.karte.android.notifications.Notifications;
import io.karte.android.notifications.MessageHandler;

public class UnityMessagingService extends ListenerService {

    private static final String LOG_TAG = "Karte.UnityMessagingService";

    @Override
    public void onNewToken(String token) {
        Logger.d(LOG_TAG, "Refreshed token: $token");

        super.onNewToken(token);
        Notifications.registerFCMToken(token);
    }

    @Override
    public void onMessageReceived(RemoteMessage remoteMessage) {
        Logger.d(LOG_TAG, "Remote Message From: ${remoteMessage.from}");

        boolean handled = MessageHandler.handleMessage(this, remoteMessage);

        super.onMessageReceived(remoteMessage);
    }
}
