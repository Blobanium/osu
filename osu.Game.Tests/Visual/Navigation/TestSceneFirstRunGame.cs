// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;
using osu.Game.Configuration;
using osu.Game.Online.API;
using osu.Game.Overlays.Notifications;

namespace osu.Game.Tests.Visual.Navigation
{
    [System.ComponentModel.Description("game with first-run setup overlay")]
    public class TestSceneFirstRunGame : OsuGameTestScene
    {
        public override void SetUpSteps()
        {
            base.SetUpSteps();

            AddUntilStep("Wait for first-run setup", () => Game.FirstRunOverlay.State.Value == Visibility.Visible);
        }

        [Test]
        public void TestImportantNotificationDoesntInterruptSetup()
        {
            AddStep("post important notification", () => Game.Notifications.Post(new SimpleNotification { Text = "Important notification" }));
            AddAssert("no notification posted", () => Game.Notifications.UnreadCount.Value == 0);
            AddAssert("first-run setup still visible", () => Game.FirstRunOverlay.State.Value == Visibility.Visible);

            AddUntilStep("finish first-run setup", () =>
            {
                Game.FirstRunOverlay.NextButton.TriggerClick();
                return Game.FirstRunOverlay.State.Value == Visibility.Hidden;
            });
            AddWaitStep("wait for post delay", 5);
            AddAssert("notifications shown", () => Game.Notifications.State.Value == Visibility.Visible);
            AddAssert("notification posted", () => Game.Notifications.UnreadCount.Value == 1);
        }

        protected override TestOsuGame CreateTestGame() => new FirstRunGame(LocalStorage, API);

        private class FirstRunGame : TestOsuGame
        {
            public FirstRunGame(Storage storage, IAPIProvider api, string[] args = null)
                : base(storage, api, args)
            {
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                LocalConfig.SetValue(OsuSetting.ShowFirstRunSetup, true);
            }
        }
    }
}
