﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TLoZ.Items.Weapons.MasterSword;
using TLoZ.Players;

namespace TLoZ
{
    public class TLoZDrawLayers
    {
        private static TLoZDrawLayers _instance;
        public static TLoZDrawLayers Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TLoZDrawLayers();
                return _instance;
            }
        }
        public readonly PlayerLayer twoHandedWeaponLayer = new PlayerLayer("TLoZ", "twoHandedWeaponLayer", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            TLoZPlayer tlozPlayer = TLoZPlayer.Get(drawPlayer);

            if (drawPlayer.dead || !tlozPlayer.HoldsTwoHander) // If the player can't use the item, don't draw it.
                return;
            int itemType = drawPlayer.HeldItem.type;
            Texture2D itemTexture = Main.itemTexture[itemType];
            Color color = Lighting.GetColor((int)drawPlayer.Center.X / 16, (int)drawPlayer.Center.Y / 16);
            float rotation = drawPlayer.direction == -1 ? (MathHelper.Pi / 3) : MathHelper.Pi + MathHelper.Pi * .66f;
            Vector2 posOffset = tlozPlayer.swingRotation > 0.0f ? new Vector2(8 * drawPlayer.direction, 2) * tlozPlayer.swingRotation : Vector2.Zero;
            DrawData weaponData = new DrawData
            (
                itemTexture,
                new Vector2((int)drawPlayer.MountedCenter.X, (int)drawPlayer.MountedCenter.Y) + new Vector2(22 * drawPlayer.direction, 6) - posOffset - Main.screenPosition,
                null,
                color,
                rotation - (tlozPlayer.swingRotation) * drawPlayer.direction,
                new Vector2(drawPlayer.direction == -1 ? 0 : itemTexture.Width , itemTexture.Height),
                1.4f,
                drawPlayer.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                1
                );

            Main.playerDrawData.Add(weaponData);
        });


        public readonly PlayerLayer paragliderLayer = new PlayerLayer("TLoZ", "paragliderLayer", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
                return;

            Player drawPlayer = drawInfo.drawPlayer;

            if (drawPlayer.dead) // If the player can't use the item, don't draw it.
                return;
            Color color = Lighting.GetColor((int)drawPlayer.Center.X / 16, (int)drawPlayer.Center.Y / 16);
            DrawData sheathData = new DrawData
            (
                ModContent.GetTexture("TLoZ/Items/Tools/ParaByLiz"),
                new Vector2((int)drawPlayer.MountedCenter.X, (int)drawPlayer.MountedCenter.Y - 22) - Main.screenPosition,
                null,
                color,
                0,
                new Vector2(19, 12),
                1f,
                drawPlayer.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                1
                );
            Main.playerDrawData.Add(sheathData);
        });



        public readonly PlayerLayer masterSwordSheath = new PlayerLayer("TLoZ", "SheathLayer", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
                return;

            Player drawPlayer = drawInfo.drawPlayer;

            if (drawPlayer.dead) // If the player can't use the item, don't draw it.
                return;

            int offsetY = 6;
            int offsetX = 6 * drawPlayer.direction;

            Color color = Lighting.GetColor((int)drawPlayer.Center.X / 16, (int)drawPlayer.Center.Y / 16);
            DrawData sheathData = new DrawData
            (
                ModContent.GetTexture("TLoZ/Items/Weapons/MasterSword/MasterSwordSheath"),
                new Vector2((int)drawPlayer.MountedCenter.X - offsetX, (int)drawPlayer.MountedCenter.Y + offsetY) - Main.screenPosition,
                null,
                color,
                0,
                new Vector2(17, 17),
                1f,
                drawPlayer.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                1
                );

            DrawData swordData = new DrawData
            (
                ModContent.GetTexture("TLoZ/Items/Weapons/MasterSword/MasterSheathedByLiz"),
                new Vector2((int)drawPlayer.MountedCenter.X + 2 * drawPlayer.direction, (int)drawPlayer.MountedCenter.Y - 2) - Main.screenPosition,
                null,
                color,
                0,
                new Vector2(24, 24),
                1f,
                drawPlayer.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                1
                );

            if (drawPlayer.HeldItem.type != TLoZ.Instance.ItemType<MasterSword>() || !TLoZPlayer.Get(drawPlayer).UsingMasterSword)
                Main.playerDrawData.Add(swordData);

            Main.playerDrawData.Add(sheathData);
        });

        public readonly PlayerLayer masterSwordSheathBelt = new PlayerLayer("TLoZ", "SheathBeltLayer", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
                return;

            Player drawPlayer = drawInfo.drawPlayer;

            if (drawPlayer.dead) // If the player can't use the item, don't draw it.
                return;
            Color color = Lighting.GetColor((int)drawPlayer.Center.X / 16, (int)drawPlayer.Center.Y / 16);
            DrawData sheathData = new DrawData
            (
                ModContent.GetTexture("TLoZ/Items/Weapons/MasterSword/Buckle_Body"),
                new Vector2((int)drawPlayer.MountedCenter.X, (int)drawPlayer.MountedCenter.Y - 3) - Main.screenPosition,
                drawPlayer.bodyFrame,
                color,
                0,
                new Vector2(20, 28),
                1f,
                drawPlayer.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                1
                );
            Main.playerDrawData.Add(sheathData);
        });
    }
}