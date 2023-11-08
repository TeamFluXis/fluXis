using fluXis.Game.Overlay.Settings.UI;
using fluXis.Game.Screens.Import;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace fluXis.Game.Overlay.Settings.Sections;

public partial class DebugSection : SettingsSection
{
    public override IconUsage Icon => FontAwesome.Solid.Bug;
    public override string Title => "Debug";

    [BackgroundDependencyLoader]
    private void load(FrameworkConfigManager frameworkConfig, FluXisGameBase game)
    {
        AddRange(new Drawable[]
        {
            new SettingsToggle
            {
                Label = "Show Log Overlay",
                Bindable = frameworkConfig.GetBindable<bool>(FrameworkSetting.ShowLogOverlay)
            },
            new SettingsButton
            {
                Label = "Import File",
                ButtonText = "Import",
                Action = () =>
                {
                    game.Settings.Hide();
                    game.ScreenStack.Push(new FileImportScreen
                    {
                        OnFileSelected = file =>
                        {
                            game.HandleDragDrop(file.FullName);
                            game.Settings.Show();
                        }
                    });
                }
            },
            new SettingsButton
            {
                Label = "Install Update From File",
                Description = "Installs an update from a .zip file. Be careful from where you download the file from though!",
                ButtonText = "Find",
                Action = () =>
                {
                    game.Settings.Hide();
                    game.ScreenStack.Push(new FileImportScreen
                    {
                        AllowedExtensions = new[] { ".zip" },
                        OnFileSelected = file => game.CreateUpdateManager()?.UpdateFromFile(file)
                    });
                }
            }
        });
    }
}
