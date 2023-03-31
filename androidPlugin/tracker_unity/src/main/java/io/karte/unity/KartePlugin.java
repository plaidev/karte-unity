package io.karte.unity;
import io.karte.android.KarteApp;
import io.karte.android.core.library.Library;
import org.jetbrains.annotations.NotNull;

public class KartePlugin implements Library {
  // implements Library
  @Override
  public boolean isPublic() {
    return true;
  }

  @NotNull
  @Override
  public String getName() {
    return "unity";
  }

  @NotNull
  @Override
  public String getVersion() {
    return "1.0.3";
  }

  @Override
  public void configure(@NotNull KarteApp karteApp) {
  }

  @Override
  public void unconfigure(@NotNull KarteApp karteApp) {
  }
}
