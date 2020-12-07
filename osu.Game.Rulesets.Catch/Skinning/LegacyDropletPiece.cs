// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics.Textures;

namespace osu.Game.Rulesets.Catch.Skinning
{
    public class LegacyDropletPiece : LegacyCatchHitObjectPiece
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();

            Texture texture = Skin.GetTexture("fruit-drop");
            Texture overlayTexture = Skin.GetTexture("fruit-drop-overlay");

            SetTexture(texture, overlayTexture);
        }
    }
}
