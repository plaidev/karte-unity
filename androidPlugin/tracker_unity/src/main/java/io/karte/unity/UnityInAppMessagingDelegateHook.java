package io.karte.unity;

import android.net.Uri;

import com.unity3d.player.UnityPlayer;

import io.karte.android.inappmessaging.InAppMessagingDelegate;

public class UnityInAppMessagingDelegateHook extends InAppMessagingDelegate {

  private static final UnityInAppMessagingDelegateHook instance = new UnityInAppMessagingDelegateHook();
  private UnityInAppMessagingDelegateHook() {}
  public static UnityInAppMessagingDelegateHook getInstance() {
    return instance;
  }

  private String callbackTargetName;
  private boolean isOpenUrlHandlerEnabled;

  public void enableOpenUrlHandler(String callbackTargetName) {
    this.callbackTargetName = callbackTargetName;
    isOpenUrlHandlerEnabled = true;
  }

  public void disableOpenUrlHandler() {
    isOpenUrlHandlerEnabled = false;
  }

  @Override
  public boolean shouldOpenURL(Uri url) {
    if (isOpenUrlHandlerEnabled) {
      UnityPlayer.UnitySendMessage(callbackTargetName, "InAppMessagingOpenUrlCallback", url.toString());
      return false;
    }

    return true;
  }
}
