package io.karte.unity;

import android.app.Activity;

import com.google.firebase.messaging.RemoteMessage;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Iterator;

import io.karte.android.core.logger.Logger;
import io.karte.android.notifications.MessageHandler;

class UnityNotifications {
    private static final String LOG_TAG = "Karte.UnityNotifications";

    private static boolean canHandleMessage(String serializedRemoteMessage) {
        RemoteMessage remoteMessage = UnityNotifications.deserializeRemoteMessage(serializedRemoteMessage);
        if (remoteMessage == null) {
            return false;
        }
        return MessageHandler.canHandleMessage(remoteMessage);
    }

    private static boolean handleMessage(String serializedRemoteMessage) {
        RemoteMessage remoteMessage = UnityNotifications.deserializeRemoteMessage(serializedRemoteMessage);
        if (remoteMessage == null) {
            return false;
        }
        Activity activity = UnityPlayer.currentActivity;
        return MessageHandler.handleMessage(activity, remoteMessage);
    }

    private static RemoteMessage deserializeRemoteMessage(String serializedRemoteMessage) {
        try {
            JSONObject rawRemoteMessage = new JSONObject(serializedRemoteMessage);

            //RemoteMessage.Builderの引数にはSENDER_IDを指定する必要があるが、
            //ここで生成されたRemoteMessageを渡す先の
            //- MessageHandler.canHandleMessage
            //- MessageHandler.handleMessage
            //ではその値を使用しないのでダミーの値で充分。
            RemoteMessage.Builder builder = new RemoteMessage.Builder("DUMMY_SENDER_ID@gcm.googleapis.com");

            Iterator<String> keys = rawRemoteMessage.keys();
            HashMap<String, String> map = new HashMap<String, String>();
            while(keys.hasNext()) {
                String key = keys.next();
                String value = rawRemoteMessage.getString(key);
                map.put(key, value);
            }
            builder.setData(map);

            return builder.build();
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse serialized remote message.", e);
            return null;
        }
    }
}
