package io.karte.unity;

import android.app.Application;

import io.karte.android.KarteApp;
import io.karte.android.core.logger.Logger;

public class UnityApplication extends Application {
    private static final String LOG_TAG = "Karte.UnityApplication";

    @Override
    public void onCreate() {
        super.onCreate();

        UnityConfig config = UnityConfig.buildFromConfigFile(this);
        if(config == null) {
            Logger.e(LOG_TAG, "Failed to setup KarteTracker. Make sure assets/karte_tracker_config.json is properly set.", null);
            return;
        }
        KarteApp.setup(this, config.getAppKey(), config.getKarteConfig());
    }
}
