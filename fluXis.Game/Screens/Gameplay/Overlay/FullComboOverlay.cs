using fluXis.Game.Graphics.Patterns;
using fluXis.Game.Graphics.Sprites;
using fluXis.Game.Graphics.UserInterface.Color;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace fluXis.Game.Screens.Gameplay.Overlay;

public partial class FullComboOverlay : Container
{
    private readonly StripePattern pattern;
    private readonly FluXisSpriteText text;

    public FullComboOverlay()
    {
        RelativeSizeAxes = Axes.Both;
        Alpha = 0;

        Children = new Drawable[]
        {
            new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = ColourInfo.GradientHorizontal(FluXisColors.Accent, FluXisColors.Accent4),
                Alpha = 0.2f
            },
            pattern = new StripePattern
            {
                Speed = new Vector2(-300)
            },
            text = new FluXisSpriteText
            {
                FontSize = 100,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            }
        };
    }

    public void Show(FullComboType type)
    {
        this.FadeIn(200);
        pattern.SpeedTo(new Vector2(-50), 500, Easing.OutQuint);
        text.ScaleTo(1.1f, 3000, Easing.OutQuint);

        text.Text = type switch
        {
            FullComboType.FullCombo => "FULL COMBO",
            FullComboType.AllFlawless => "ALL FLAWLESS",
            _ => text.Text
        };
    }

    public enum FullComboType
    {
        FullCombo,
        AllFlawless
    }
}
