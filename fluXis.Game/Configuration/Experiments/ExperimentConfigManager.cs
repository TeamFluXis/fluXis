using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace fluXis.Game.Configuration.Experiments;

public class ExperimentConfigManager : IniConfigManager<ExperimentConfig>
{
    protected override string Filename => "experiments.ini";

    public ExperimentConfigManager(Storage storage)
        : base(storage)
    {
    }

    protected override void InitialiseDefaults()
    {
        SetDefault(ExperimentConfig.DesignTab, false);
        SetDefault(ExperimentConfig.TintLongNotesOnMiss, false);
    }
}

public enum ExperimentConfig
{
    DesignTab,
    TintLongNotesOnMiss
}
