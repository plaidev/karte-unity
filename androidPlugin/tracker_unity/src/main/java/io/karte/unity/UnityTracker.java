package io.karte.unity;

import android.app.Activity;

import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import io.karte.android.core.logger.Logger;
import io.karte.android.tracking.Tracker;

class UnityTracker {
    private static final String LOG_TAG = "Karte.UnityTracker";

    private static void track(String eventName) {
        Tracker.track(eventName);
    }

    private static void track(String eventName, String serializedValues) {
        try {
            JSONObject values = new JSONObject(serializedValues);
            Tracker.track(eventName, values);
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse values JSON.", e);
        }
    }

    private static void identify(String serializedValues) {
        try {
            JSONObject values = new JSONObject(serializedValues);
            Tracker.identify(values);
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse values JSON.", e);
        }
    }

    private static void identify(String userId, String serializedValues) {
        try {
            JSONObject values = new JSONObject(serializedValues);
            Tracker.identify(userId, values);
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse values JSON.", e);
        }
    }

    private static void view(String viewName) {
        Tracker.view(viewName);
    }

    private static void view(String viewName, String title) {
        Tracker.view(viewName, title);
    }

    private static void view(String viewName, String title, String serializedValues) {
        try {
            JSONObject values = new JSONObject(serializedValues);
            Tracker.view(viewName, title, values);
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse values JSON.", e);
        }
    }

    private static void viewWithValue(String viewName, String serializedValues) {
        try {
            JSONObject values = new JSONObject(serializedValues);
            Tracker.view(viewName, values);
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse values JSON.", e);
        }
    }

    private static void attribute(String serializedValues) {
        try {
            JSONObject values = new JSONObject(serializedValues);
            Tracker.attribute(values);
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse values JSON.", e);
        }
    }
}