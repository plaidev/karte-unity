package io.karte.unity;

import org.json.JSONException;
import org.json.JSONObject;
import org.junit.Test;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URL;

import io.karte.android.core.config.Config;

import static org.junit.Assert.*;

/**
 * Example local unit test, which will execute on the development machine (host).
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
public class ConfigUnitTest {
    @Test
    public void buildFromJSON() throws IOException, JSONException {
        URL configURL = this.getClass().getClassLoader().getResource("karte_tracker_config.json");


        BufferedReader br = new BufferedReader(new InputStreamReader(configURL.openStream()));
        StringBuilder sb = new StringBuilder();
        String line;
        while ((line = br.readLine()) != null) {
            sb.append(line);
        }
        JSONObject rawConfig = new JSONObject(sb.toString());
        UnityConfig unityConfig = new UnityConfig(rawConfig);
        Config config = unityConfig.getKarteConfig();
        assertEquals("app_key", unityConfig.getAppKey());
        assertEquals("http://example.com/v0/native", config.getBaseUrl());
        assertEquals(true, config.getEnabledTrackingAaid());
        assertEquals(true, config.isOptOut());
        assertEquals(true, config.isDryRun());
    }
}


