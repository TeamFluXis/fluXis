using System.Collections.Generic;
using fluXis.Game.Graphics.Background;
using fluXis.Game.Map;
using fluXis.Game.Online.API;
using fluXis.Game.Online.API.Requests.Scores;
using fluXis.Game.Screens;
using fluXis.Game.Screens.Result;
using fluXis.Shared.API.Responses.Scores;
using fluXis.Shared.Components.Scores;
using fluXis.Shared.Components.Users;
using fluXis.Shared.Scoring;
using fluXis.Shared.Scoring.Enums;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace fluXis.Game.Tests.Results;

public partial class TestNewResults : FluXisTestScene
{
    [BackgroundDependencyLoader]
    private void load(MapStore maps)
    {
        CreateClock();

        var backgrounds = new GlobalBackground();
        TestDependencies.Cache(backgrounds);

        var score = new ScoreInfo
        {
            Accuracy = 98.661736f,
            Rank = ScoreRank.S,
            PerformanceRating = 8,
            Score = 1139289,
            MaxCombo = 1218,
            Flawless = 898,
            Perfect = 290,
            Great = 30,
            Alright = 0,
            Okay = 0,
            Miss = 0,
            Mods = new List<string> { "1.5x" }
        };

        var stack = new FluXisScreenStack();

        AddRange(new Drawable[]
        {
            backgrounds,
            new Container
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding { Top = 60 },
                Child = stack
            }
        });

        var set = maps.GetFromGuid("9896365c-5541-4612-9f39-5a44aa1012ed");
        var map = set?.Maps[0] ?? maps.MapSets[0].Maps[0];

        var screen = new SoloResults(map, score, APIUser.Dummy);
        screen.SubmitRequest = new SimulatedScoreRequest(score);
        stack.Push(screen);
    }

    private class SimulatedScoreRequest : ScoreSubmitRequest
    {
        public SimulatedScoreRequest(ScoreInfo score)
            : base(score)
        {
            Response = new APIResponse<ScoreSubmissionStats>(200, "", new ScoreSubmissionStats(new APIScore { PerformanceRating = 7 }, 10, 10, 1, 12, 10, 2));
        }
    }
}
