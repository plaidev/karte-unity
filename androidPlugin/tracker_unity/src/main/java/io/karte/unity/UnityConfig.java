package io.karte.unity;

import android.app.Application;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

import io.karte.android.core.config.Config;
import io.karte.android.core.logger.Logger;

class UnityConfig {
    private static final String LOG_TAG = "Karte.UnityConfig";
    private static final String CONFIG_FILE_NAME = "karte_tracker_config.json";

    private Config karteConfig;
    private String appKey;

    Config getKarteConfig() { return karteConfig; }
    String getAppKey() { return appKey; }

    static UnityConfig buildFromConfigFile(Application app) {
        JSONObject rawConfig = readConfigFile(app);
        if(rawConfig == null) {
            return null;
        }
        return new UnityConfig(rawConfig);
    }

    UnityConfig(JSONObject rawConfig) {
        try {
            this.appKey = rawConfig.getString("appKey");
        } catch(JSONException e) {
            Logger.e(LOG_TAG, "Required property `appKey` is not defined in " + CONFIG_FILE_NAME, e);
        }
        if(rawConfig.has("karteConfig")) {
            try {
                this.karteConfig = buildKarteConfigFromJSON(rawConfig.getJSONObject("karteConfig"));
            } catch (JSONException e) {
                Logger.e(LOG_TAG, "Failed to parse " + CONFIG_FILE_NAME, e);
            }
        }
    }

    static Config buildKarteConfigFromJSON(JSONObject config) {
        Config.Builder builder = new Config.Builder();

        try {
            if(config.has("baseUrl")) {
                builder.baseUrl(config.getString("baseUrl"));
            }
            if(config.has("isDryRun")) {
                builder.isDryRun(config.getBoolean("isDryRun"));
            }
            if (config.has("isOptOut")) {
                builder.isOptOut(config.getBoolean("isOptOut"));
            }
            if (config.has("enabledTrackingAaid")) {
                builder.enabledTrackingAaid(config.getBoolean("enabledTrackingAaid"));
            }
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse " + CONFIG_FILE_NAME, e);
            return null;
        }

        return builder.build();
    }

    private static JSONObject readConfigFile(Application app) {
        try {
            InputStream inputStream = app.getAssets().open(CONFIG_FILE_NAME);
            BufferedReader br = new BufferedReader(new InputStreamReader(inputStream));
            StringBuilder sb = new StringBuilder();
            String line;
            while ((line = br.readLine()) != null) {
                sb.append(line);
            }
            JSONObject config = new JSONObject(sb.toString());
            return config;
        } catch (IOException e) {
            Logger.e(LOG_TAG, "Failed to read " + CONFIG_FILE_NAME, e);
            return null;
        } catch (JSONException e) {
            Logger.e(LOG_TAG, "Failed to parse " + CONFIG_FILE_NAME, e);
            return null;
        }
    }
}
