package io.karte.unity;

import android.app.Activity;

import com.unity3d.player.UnityPlayer;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import io.karte.android.core.logger.Logger;
import io.karte.android.variables.FetchCompletion;
import io.karte.android.variables.Variable;
import io.karte.android.variables.Variables;

class UnityVariables {
    private static final String LOG_TAG = "Karte.UnityVariables";

    private static void fetch() {
        Variables.fetch();
    }

    private static void fetchWithCompletionBlock(final String callbackTarget, final String callbackId) {
        Variables.fetch(new FetchCompletion() {
            @Override
            public void onComplete(boolean success) {
                String result = success ? "true" : "false";
                JSONObject json = new JSONObject();
                try {
                    json.put("result", result);
                    json.put("callbackId", callbackId);
                } catch (JSONException e) {
                    Logger.e(LOG_TAG, "Failed to construct a JSON.", e);
                    return;
                }
                UnityPlayer.UnitySendMessage(callbackTarget, "VariableCallback", json.toString());
            }
        });
    }
//
//    private static String getVariable(String key) {
//        Activity activity = UnityPlayer.currentActivity;
//        io.karte.android.tracker.Variables variables = io.karte.android.tracker.Variables.getInstance(activity);
//        Variable variable = variables.getVariable(key);
//        JSONObject json = new JSONObject();
//        try {
//            json.put("campaignId", variable.getCampaignId());
//            json.put("shortenId", variable.getShortenId());
//            json.put("value", variable.getString(""));
//        } catch (JSONException e) {
//            KRLog.e(LOG_TAG, "Failed to construct a JSON.", e);
//            return "{}";
//        }
//        return json.toString();
//    }

    private static ArrayList<Variable> getVariablesFromSerializedKeys(String serializedKeys) {
        try {
            JSONArray rawkeys = new JSONArray(serializedKeys);

            ArrayList<Variable> variables = new ArrayList<>();
            for (int i=0;i<rawkeys.length();i++) {
                String key = rawkeys.getString(i);
                Variable variable = Variables.get(key);
                variables.add(variable);
            }
            return variables;
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse serialized variables.", e);
            return null;
        }
    }

    private static void trackOpen(String serializedKeys) {
        ArrayList<Variable> variables = getVariablesFromSerializedKeys(serializedKeys);
        if (variables != null) {
            Variables.trackOpen(variables);
        }
    }

    private static void trackOpen(String serializedKeys, String serializedValues) {
        ArrayList<Variable> variables = getVariablesFromSerializedKeys(serializedKeys);
        if (variables == null) {
            return;
        }
        try {
            JSONObject values = new JSONObject(serializedValues);
            Variables.trackOpen(variables, values);
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse values JSON.", e);
            return;
        }
    }

    private static void trackClick(String serializedKeys) {
        ArrayList<Variable> variables = getVariablesFromSerializedKeys(serializedKeys);
        if (variables != null) {
            Variables.trackClick(variables);
        }
    }

    private static void trackClick(String serializedKeys, String serializedValues) {
        ArrayList<Variable> variables = getVariablesFromSerializedKeys(serializedKeys);
        if (variables == null) {
            return;
        }
        try {
            JSONObject values = new JSONObject(serializedValues);
            Variables.trackClick(variables, values);
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse values JSON.", e);
            return;
        }
    }

    private static String getString(String key, String defaultValue) {
        Variable v = Variables.get(key);
        return v.getString(defaultValue);
    }

    private static long getLong(String key, long defaultValue) {
        Variable v = Variables.get(key);
        return v.getLong(defaultValue);
    }

    private static double getDouble(String key, long defaultValue) {
        Variable v = Variables.get(key);
        return v.getDouble(defaultValue);
    }

    private static boolean getBoolean(String key, boolean defaultValue) {
        Variable v = Variables.get(key);
        return v.getBoolean(defaultValue);
    }

    private static String getArray(String key) {
        Variable v = Variables.get(key);
        JSONArray array = v.getJSONArray(null);
        if(array == null) {
            return null;
        }
        return array.toString();
    }

    private static String getObject(String key) {
        Variable v = Variables.get(key);
        JSONObject object = v.getJSONObject(null);
        if(object == null) {
            return null;
        }
        return object.toString();
    }
}
