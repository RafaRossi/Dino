using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public static class AnalyticsManager
{
    public static void ReportLevelState(LevelState state)
    {
        switch (state)
        {
            case LevelState.Started:
                AnalyticsEvent.LevelStart(1);
                break;

            case LevelState.Won:
                AnalyticsEvent.LevelComplete(1);
                break;

            case LevelState.Loose:
                AnalyticsEvent.LevelFail(1);
                break;

            case LevelState.WatchAd:
                AnalyticsEvent.AdComplete(true);
                break;

        }
    }

    public static void ReportPlayerInfo(PlayerData data)
    {
        AnalyticsEvent.Custom("Level Data", new Dictionary<string, object>()
        {
            {"player_death_time", data.deathTimes },
            {"player_remaining_lifes", data.remainingLifes },
            {"cretes_destroyed", data.cratesBroken },
            {"max_crates", data.maxCrates },
            {"got_yellow_mushroom", data.yellowMushroomCollected },
            {"time_taken", data.time }
        });
    }
}
