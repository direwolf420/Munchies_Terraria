﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Munchies.Models;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Munchies.UIElements {
	public class ReportListItem(IConsumable consumable) : UIElement() {
		public readonly IConsumable Consumable = consumable;

		// Maximum width of any of the images
		private readonly float imageAssetMaxWidth = 36;
		private readonly float spacing = 10;
		public static Asset<Texture2D> CheckMarkTexture;

		UIPanel panel;
		UIText text;

		public override void OnInitialize() {
			panel = new() {
				Width = StyleDimension.Fill,
				Height = StyleDimension.Fill
			};
			panel.SetPadding(0);
			Append(panel);

			Asset<Texture2D> consumableTexture = ModContent.Request<Texture2D>(Consumable.TexturePath);
			UIImage itemImage = new(consumableTexture);
			itemImage.Left.Set(spacing, 0);
			itemImage.Width.Set(Consumable.AssetDimensions.X, 0f);
			itemImage.Height.Set(Consumable.AssetDimensions.Y, 0f);
			itemImage.SetPadding(0);
			itemImage.VAlign = 0.5f;
			panel.Append(itemImage);

			UIImage checkMarkImage = new(CheckMarkTexture);
			checkMarkImage.Left.Set(-spacing - 19, 1f);
			checkMarkImage.Width.Set(19f, 0f);
			checkMarkImage.Height.Set(20f, 0f);
			checkMarkImage.SetPadding(0);
			checkMarkImage.VAlign = 0.5f;
			checkMarkImage.Color = Color.SpringGreen;
			if (Consumable.HasBeenConsumed()) {
				panel.Append(checkMarkImage);
			}

			text = new(text: Consumable.DisplayText) {
				TextColor = DisplayTextColor,
				ShadowColor = Color.Black,
				IsWrapped = false,
				WrappedTextBottomPadding = 0f,
				HAlign = 0f,
				VAlign = 0.5f,
				DynamicallyScaleDownToWidth = true,
			};
			text.Left.Set(imageAssetMaxWidth + (spacing * 2), 0f);
			text.SetPadding(0);
			panel.Append(text);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
			if (panel?.IsMouseHovering ?? false) {
				Main.hoverItemName = Consumable.HoverText;
			}
		}

		private Color DisplayTextColor => Consumable.Type switch {
			ConsumableType.player_normal => Color.White,
			ConsumableType.player_expert => Main.expertMode ? Color.Orange : Color.Gray,
			ConsumableType.world => new Color(r: 242, g: 111, b: 238),
			_ => throw new System.NotImplementedException(),
		};
	}
}
