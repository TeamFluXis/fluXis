using fluXis.Game.Graphics;
using fluXis.Game.Screens.Edit.Tabs.Metadata;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace fluXis.Game.Screens.Edit.Tabs
{
    public partial class SetupTab : EditorTab
    {
        public SetupTab(Editor screen)
            : base(screen)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    CornerRadius = 10,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = FluXisColors.Background2,
                        },
                    }
                },
                new Container
                {
                    Padding = new MarginPadding(20),
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Child = new BasicScrollContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Direction = FillDirection.Vertical,
                            Children = new Drawable[]
                            {
                                new AssetsSetupSection(),
                                new MetadataSetupSection(Screen.MapInfo.Metadata),
                                new KeyModeSetupSection(Screen.Map)
                            }
                        }
                    }
                }
            };
        }
    }
}