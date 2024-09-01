using Munchies.Models.Enums;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace Munchies.Models {
	public class Report {
		public static ConsumableMod VanillaConsumableMod = new(modTabName: "Terraria", modTabTexturePath: "Terraria/Images/Item_4765");
		public static List<ConsumablesEntry> ConsumablesList = [];

		private static readonly int[] VanillaItems = [
			// multi-use
			ItemID.LifeCrystal,
			ItemID.LifeFruit,
			ItemID.ManaCrystal,

			// normal
			ItemID.ArtisanLoaf,
			ItemID.TorchGodsFavor,
			ItemID.AegisCrystal,
			ItemID.AegisFruit,
			ItemID.ArcaneCrystal,
			ItemID.Ambrosia,
			ItemID.GummyWorm,
			ItemID.GalaxyPearl,

			// expert
			ItemID.DemonHeart,
			ItemID.MinecartPowerup,

			// world
			ItemID.CombatBook,
			ItemID.CombatBookVolumeTwo,
			ItemID.PeddlersSatchel,
		];

		public Report() {
			List<Consumable> vanillaConsumables = [
				// multi
				new(vanillaItemId: ItemID.LifeCrystal, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.ConsumedLifeCrystals, totalCount: () => 15, difficulty: Difficulty.classic, available: () => true),
				new(vanillaItemId: ItemID.LifeFruit, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.ConsumedLifeFruit, totalCount: () => 20, difficulty: Difficulty.classic, available: () => true), 
				new(vanillaItemId: ItemID.ManaCrystal, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.ConsumedManaCrystals, totalCount: () => 9, difficulty: Difficulty.classic, available: () => true), 

				// normal
				new(vanillaItemId: ItemID.ArtisanLoaf, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.ateArtisanBread.ToInt(), totalCount: () => 1, difficulty: Difficulty.classic, available: () => true), 
				new(vanillaItemId: ItemID.TorchGodsFavor, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.unlockedBiomeTorches.ToInt(), totalCount: () => 1, difficulty: Difficulty.classic, available: () => true), 
				new(vanillaItemId: ItemID.AegisCrystal, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.usedAegisCrystal.ToInt(), totalCount: () => 1, difficulty: Difficulty.classic, available: () => true), 
				new(vanillaItemId: ItemID.AegisFruit, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.usedAegisFruit.ToInt(), totalCount: () => 1, difficulty: Difficulty.classic, available: () => true), 
				new(vanillaItemId: ItemID.ArcaneCrystal, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.usedArcaneCrystal.ToInt(), totalCount: () => 1, difficulty: Difficulty.classic, available: () => true), 
				new(vanillaItemId: ItemID.Ambrosia, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.usedAmbrosia.ToInt(), totalCount: () => 1, difficulty: Difficulty.classic, available: () => true), 
				new(vanillaItemId: ItemID.GummyWorm, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.usedGummyWorm.ToInt(), totalCount: () => 1, difficulty: Difficulty.classic, available: () => true), 
				new(vanillaItemId: ItemID.GalaxyPearl, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.usedGalaxyPearl.ToInt(), totalCount: () => 1, difficulty: Difficulty.classic, available: () => true), 

				// expert
				new(vanillaItemId: ItemID.DemonHeart, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.extraAccessory.ToInt(), totalCount: () => 1, difficulty: Difficulty.expert, available: () => Main.expertMode), 
				new(vanillaItemId: ItemID.MinecartPowerup, type: ConsumableType.player, currentCount: () => Main.LocalPlayer.unlockedSuperCart.ToInt(), totalCount: () => 1, difficulty: Difficulty.expert, available: () => Main.expertMode),

				// world
				new(vanillaItemId: ItemID.CombatBook, type: ConsumableType.world, currentCount: () => NPC.combatBookWasUsed.ToInt(), totalCount: () => 1, difficulty: Difficulty.classic, available: () => true), 
				new(vanillaItemId: ItemID.CombatBookVolumeTwo, type: ConsumableType.world, currentCount: () => NPC.combatBookVolumeTwoWasUsed.ToInt(), totalCount: () => 1, difficulty: Difficulty.classic, available: () => true), 
				new(vanillaItemId: ItemID.PeddlersSatchel, type: ConsumableType.world, currentCount: () => NPC.peddlersSatchelWasUsed.ToInt(), totalCount: () => 1, difficulty: Difficulty.classic, available: () => true), 
			];
			ConsumablesList.Add(new(VanillaConsumableMod, vanillaConsumables));
		}

		internal static ConsumablesEntry GetModEntryOrAddIfNeeded(ConsumableMod mod) {
			ConsumablesEntry foundEntry = ConsumablesList.Find(e => e.Mod.ModTabName == mod.ModTabName);
			if (foundEntry != null) {
				return foundEntry;
			} else {
				ConsumablesEntry newEntry = new(mod, []);
				ConsumablesList.Add(newEntry);
				return newEntry;
			}
		}

		public static bool AddConsumableToList(ConsumableMod mod, Consumable consumable) {
			ConsumablesEntry entry = GetModEntryOrAddIfNeeded(mod);

			foreach (Consumable c in entry.Consumables) {
				if (c.ID == consumable.ID) {
					// consumable already exists, exit
					return false;
				}
			}

			// consumable does not exist, add it
			entry.Consumables.Add(consumable);
			return true;
		}
	}
}