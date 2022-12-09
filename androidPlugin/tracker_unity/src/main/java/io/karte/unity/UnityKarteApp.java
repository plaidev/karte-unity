package io.karte.unity;

import android.app.Activity;
import android.net.Uri;

import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import io.karte.android.core.logger.LogLevel;
import io.karte.android.core.logger.Logger;
import io.karte.android.notifications.Notifications;
import io.karte.android.KarteApp;
import io.karte.android.core.config.Config;

public class UnityKarteApp {
  private static final String LOG_TAG = "Karte.UnityKarteApp";

  private static void setup(String appkey) {
    Activity activity = UnityPlayer.currentActivity;
    KarteApp.setup(activity, appkey);
  }

  private static void setup(String appkey, String serializedConfig) {
    JSONObject rawConfig;
    Config config;
    try {
      rawConfig = new JSONObject(serializedConfig);
      config = UnityConfig.buildKarteConfigFromJSON(rawConfig);
    } catch (JSONException e) {
      Logger.e(LOG_TAG, "Failed to parse config JSON.", e);
      return;
    }
    Activity activity = UnityPlayer.currentActivity;
    KarteApp.setup(activity, appkey, config);
  }

  private static String getVisitorId() {
    return KarteApp.getVisitorId();
  }

  private static void registerFcmToken(String token) {
    Notifications.registerFCMToken(token);
  }

  private static void setLogLevel(int minLevel) {
    LogLevel level;
    if (minLevel > LogLevel.values().length - 1) {
      Logger.w(LOG_TAG, "invalid minLevel", null);
    }
    level = LogLevel.values()[minLevel];
    KarteApp.setLogLevel(level);
  }

  private static void optIn() {
    KarteApp.optIn();
  }

  private static void optOut() {
    KarteApp.optOut();
  }

  private static void renewVisitorId() {
    KarteApp.renewVisitorId();
  }

  private static boolean openUrl(String uriStr) {
    Uri uri = Uri.parse(uriStr);
    Activity activity = UnityPlayer.currentActivity;
    return KarteApp.openUrl(uri, activity);
  }
}
