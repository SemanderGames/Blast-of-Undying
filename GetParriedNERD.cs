using System.Runtime.CompilerServices;
using System.Security.Permissions;
using BepInEx;
using MoreSlugcats;
using RWCustom;
using UnityEngine;

#pragma warning disable CS0618
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618

namespace GetParriedNERD
{
	[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
	public class GetParriedNERDPlugin : BaseUnityPlugin
	{
		public const string PLUGIN_GUID = "semander.getparriednerd";
		public const string PLUGIN_NAME = "Blast of Undying";
		public const string PLUGIN_VERSION = "1.0.0";

		private bool init;

		private void Awake()
		{
			On.Spear.HitSomething += Spear_HitSomething;
			On.RainWorld.OnModsInit += RainWorld_OnModsInit;
			Logger.LogInfo(PLUGIN_NAME + " enabled");
		}

		private void OnDestroy()
		{
			On.Spear.HitSomething -= Spear_HitSomething;
			On.RainWorld.OnModsInit -= RainWorld_OnModsInit;
			Logger.LogInfo(PLUGIN_NAME + " disabled");
		}

		private void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, global::RainWorld self)
		{
			orig(self);

			if (!init)
			{
				init = true;
				MachineConnector.SetRegisteredOI(PLUGIN_GUID, new GetParriedNERDOptions());
				Logger.LogInfo(PLUGIN_NAME + " initialized");
			}
		}

		private bool Spear_HitSomething(On.Spear.orig_HitSomething orig, Spear self, SharedPhysics.CollisionResult result, bool eu)
		{
			if (result.obj == null)
			{
				return orig(self, result, eu);
			}

			Player player = result.obj as Player;
			if (player != null && ShouldDeflect(self, player))
			{
				DoDeflectionEffects(self, player);
				return true;
			}

			return orig(self, result, eu);
		}

		private bool ShouldDeflect(Spear spear, Player player)
		{
			if (!IsArtificer(player))
				return false;
			if (spear == null || spear.room == null || spear.firstChunk == null)
				return false;
			if (spear.thrownBy == null || spear.thrownBy == player)
				return false;

			int stun_threshold = GetParriedNERDOptions.DeflectionStunThreshold.Value;

			if (player.pyroJumpCounter >= stun_threshold) // if Arti is stunned
				return false; // don't deflect and just die from spear

			return TryConsumeVanillaArtificerCharge(player);
		}

		private bool IsArtificer(Player player)
		{
			return player != null &&
				   player.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Artificer;
		}

		private bool TryConsumeVanillaArtificerCharge(Player player)
		{
			int cap = global::MoreSlugcats.MoreSlugcats.cfgArtificerExplosionCapacity.Value;
			int cost = GetParriedNERDOptions.DeflectionCost.Value;
			int stun_threshold = GetParriedNERDOptions.DeflectionStunThreshold.Value;

			int oldCounter = player.pyroJumpCounter;
			int newCounter = oldCounter + cost;

			bool can_explode = GetParriedNERDOptions.ExplodeWhenCantReflect.Value;
			if (can_explode && newCounter>=cap) // if overshoots and can explode
			{
				player.PyroDeath();
				return true;
			}
			newCounter = Mathf.Min(newCounter, cap - 1); //reduce overshooting otherwise

			// If this hit is before Arti is in the stun zone, stun instead of dying.
			if (oldCounter < stun_threshold && newCounter >= stun_threshold)
			{
				int stun_duration = GetParriedNERDOptions.DeflectionStunDuration.Value;
				if (stun_duration > 0)
				{
					int stun = stun_duration * (newCounter - (stun_threshold - 1));
					player.Stun(stun);
				}
			}

			player.pyroJumpCounter = newCounter;
			player.pyroParryCooldown = 40f;
			player.pyroJumpCooldown = 150f;

			return true;
		}

		private void DoDeflectionEffects(Spear spear, Player player)
		{
			Room room = spear.room;
			Vector2 pos = player.firstChunk.pos;

			room.PlaySound(
				SoundID.SS_AI_Give_The_Mark_Boom,
				pos,
				0.3f + UnityEngine.Random.value * 0.3f,
				0.5f + UnityEngine.Random.value * 2f
			);
			room.PlaySound(
				SoundID.Miros_Piston_Sharp_Impact,
				pos,
				2f + UnityEngine.Random.value * 0.3f,
				0.5f + UnityEngine.Random.value * 2f
			);
			room.PlaySound(
				SoundID.Miros_Heavy_Terrain_Impact,
				pos,
				2f + UnityEngine.Random.value * 0.3f,
				0.5f + UnityEngine.Random.value * 2f
			);

			room.AddObject(new ShockWave(pos, 500f, 0.2f, 4, false));

			spear.ChangeMode(Weapon.Mode.Free);
			spear.firstChunk.vel =
				Custom.DegToVec(Custom.AimFromOneVectorToAnother(player.firstChunk.pos, spear.firstChunk.pos)) * 20f;
			spear.SetRandomSpin();

			if (global::RainWorld.ShowLogs)
			{
				Debug.Log("PARRIED LOL");
			}
		}
	}
}