using fluXis.Game.Graphics;
using fluXis.Game.Graphics.Containers;
using fluXis.Game.Graphics.Sprites;
using fluXis.Game.Graphics.UserInterface.Color;
using fluXis.Game.Map.Drawables.Online;
using fluXis.Game.Online.API.Models.Maps;
using fluXis.Game.Utils;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace fluXis.Game.Overlay.MapSet;

public partial class MapSetHeader : CompositeDrawable
{
    private APIMapSet set { get; }

    public MapSetHeader(APIMapSet set)
    {
        this.set = set;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.X;
        Height = 280;
        CornerRadius = 20;
        Masking = true;

        InternalChildren = new Drawable[]
        {
            new LoadWrapper<DrawableOnlineBackground>
            {
                RelativeSizeAxes = Axes.Both,
                LoadContent = () => new DrawableOnlineBackground(set)
                {
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                OnComplete = background => background.FadeInFromZero(400)
            },
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = FluXisColors.Background2.Opacity(.5f)
            },
            new GridContainer
            {
                Width = 1200,
                Height = 160,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.Absolute, 160),
                    new Dimension(GridSizeMode.Absolute, 10),
                    new Dimension()
                },
                Content = new[]
                {
                    new[]
                    {
                        new LoadWrapper<DrawableOnlineCover>
                        {
                            Size = new Vector2(160),
                            CornerRadius = 20,
                            Masking = true,
                            EdgeEffect = FluXisStyles.ShadowMedium,
                            LoadContent = () => new DrawableOnlineCover(set)
                            {
                                RelativeSizeAxes = Axes.Both,
                                FillMode = FillMode.Fill,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre
                            },
                            OnComplete = cover => cover.FadeInFromZero(400)
                        },
                        Empty(),
                        new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Direction = FillDirection.Vertical,
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Children = new Drawable[]
                            {
                                new Container
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Children = new[]
                                    {
                                        new StatusChip(set.Status),
                                        getUploadDate().With(d =>
                                        {
                                            d.Anchor = Anchor.CentreRight;
                                            d.Origin = Anchor.CentreRight;
                                        })
                                    }
                                },
                                new FluXisSpriteText
                                {
                                    Text = set.Title,
                                    WebFontSize = 40,
                                    Shadow = true,
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft
                                },
                                new FluXisSpriteText
                                {
                                    Text = set.Artist,
                                    WebFontSize = 24,
                                    Shadow = true,
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    private Drawable getUploadDate()
    {
        var date = TimeUtils.GetFromSeconds(set.Submitted);

        return new FillFlowContainer
        {
            AutoSizeAxes = Axes.Both,
            Direction = FillDirection.Horizontal,
            Spacing = new Vector2(5),
            Children = new Drawable[]
            {
                new FluXisSpriteText
                {
                    Text = "Uploaded at",
                    WebFontSize = 16,
                    Alpha = .8f,
                    Shadow = true
                },
                new FluXisSpriteText
                {
                    Text = date.ToString("d MMM yyyy"),
                    WebFontSize = 16,
                    Shadow = true
                }
            }
        };
    }

    private partial class StatusChip : CircularContainer
    {
        private int status { get; }

        public StatusChip(int status)
        {
            this.status = status;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AutoSizeAxes = Axes.X;
            Height = 30;
            Masking = true;
            EdgeEffect = FluXisStyles.ShadowSmall;
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = FluXisColors.GetStatusColor(status)
                },
                new FluXisSpriteText
                {
                    Text = status switch
                    {
                        -2 => "LOCAL",
                        -1 or 0 => "UNSUBMITTED",
                        1 => "PENDING",
                        2 => "IMPURE",
                        3 => "PURE",
                        _ => "UNKNOWN"
                    },
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = Colour4.Black,
                    Margin = new MarginPadding { Horizontal = 10 },
                    Alpha = .75f
                }
            };
        }
    }
}
