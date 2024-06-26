using fluXis.Game.Configuration.Experiments;
using fluXis.Game.Graphics.Sprites;
using fluXis.Game.Overlay.Settings.UI;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;

namespace fluXis.Game.Overlay.Settings.Sections;

public partial class ExperimentsSection : SettingsSection
{
    public override IconUsage Icon => FontAwesome6.Solid.Flask;
    public override LocalisableString Title => "Experiments";

    [BackgroundDependencyLoader(true)]
    private void load(ExperimentConfigManager experiments)
    {
        AddRange(new Drawable[]
        {
            new SettingsToggle
            {
                Label = "Enable Design Tab",
                Bindable = experiments.GetBindable<bool>(ExperimentConfig.DesignTab)
            },
            new SettingsToggle
            {
                Label = "Tint Long Notes on miss",
                Description = "Makes long notes fade dark instead of instantly disappearing.",
                Bindable = experiments.GetBindable<bool>(ExperimentConfig.TintLongNotesOnMiss)
            }
        });
    }
}
