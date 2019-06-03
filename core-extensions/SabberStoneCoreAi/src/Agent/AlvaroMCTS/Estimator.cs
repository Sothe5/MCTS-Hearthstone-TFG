using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Model.Entities;

namespace SabberStoneCoreAi.src.Agent.AlvaroMCTS
{
	class Estimator
	{
		private static float WEAPON_ATTACK_IMPORTANCE = 0;
		private static float WEAPON_DURABILITY_IMPORTANCE = 0;
		private static float HEALTH_IMPORTANCE = 0;
		private static float BOARD_STATS_IMPORTANCE = 0;
		private static float HAND_SIZE_IMPORTANCE = 0;
		private static float DECK_REMAINING_IMPORTANCE = 0;
		private static float MANA_IMPORTANCE = 0;
		private static float SECRET_IMPORTANCE = 0;

		private static float MINION_COST_IMPORTANCE = 0;
		private static float SECRET_COST_IMPORTANCE = 0;
		private static float CARD_COST_IMPORTANCE = 0;
		private static float WEAPON_COST_IMPORTANCE = 0;


		static public float estimateFromState(string estimationMode,POGame.POGame poGame)
		{
			float score = 0.5f;

			switch (estimationMode)
			{
				case "BaseEstimation":
					score = baseEstimation(poGame);
					break;
				case "ValueEstimation":
					score = valueEstimation(poGame);
					break;
				default:
					score = 0;
					break;
			}

			return score;
		}

		static public void setWeights(float weaponAttack, float weaponDurability, float health, float boardStats, float handSize, float deckRemaining,
			float mana, float secret, float minionCost, float secretCost, float cardCost, float weaponCost)
		{
			WEAPON_ATTACK_IMPORTANCE = weaponAttack;
			WEAPON_DURABILITY_IMPORTANCE = weaponDurability;
			HEALTH_IMPORTANCE = health;
			BOARD_STATS_IMPORTANCE = boardStats;
			HAND_SIZE_IMPORTANCE = handSize;
			DECK_REMAINING_IMPORTANCE = deckRemaining;
			MANA_IMPORTANCE = mana;
			SECRET_IMPORTANCE = secret;

			MINION_COST_IMPORTANCE = minionCost;
			SECRET_COST_IMPORTANCE = secretCost;
			CARD_COST_IMPORTANCE = cardCost;
			WEAPON_COST_IMPORTANCE = weaponCost;
		}	

		static private float baseEstimation(POGame.POGame poGame)
		{
			float finalScore = 0.5f;
			float score1 = calculateScorePlayer(poGame.FirstPlayer);

			Controller player2;
			if(poGame.CurrentPlayer == poGame.FirstPlayer)
				player2 = poGame.CurrentOpponent;
			else
				player2 = poGame.CurrentPlayer;

			float score2 = calculateScorePlayer(player2);

			finalScore = score1 - score2;
			finalScore = finalScore / Math.Max(score1, score2);
			finalScore /= 2;
			finalScore += 0.5f;

			return finalScore;
		}

		static private float calculateScorePlayer(Controller player)
		{
			float score = 0;
			float statsOnBoard = 0;
			foreach(Minion minion in player.BoardZone.GetAll())
			{
				statsOnBoard += minion.Health + minion.AttackDamage;
			}

			float weaponQuality = 0;
			if (player.Hero.Weapon != null)
				 weaponQuality = player.Hero.Weapon.AttackDamage * WEAPON_ATTACK_IMPORTANCE + player.Hero.Weapon.Durability * WEAPON_DURABILITY_IMPORTANCE;
			
			score = player.Hero.Health * HEALTH_IMPORTANCE + weaponQuality + statsOnBoard * BOARD_STATS_IMPORTANCE + player.HandZone.Count * HAND_SIZE_IMPORTANCE
				+ player.DeckZone.Count * DECK_REMAINING_IMPORTANCE + player.BaseMana * MANA_IMPORTANCE + player.SecretZone.Count * SECRET_IMPORTANCE;

			if (score == 0)
				return 0.0001f;

			return score;
		}

		static private float valueEstimation(POGame.POGame poGame)
		{
			float finalScore = 0.5f;

			float score1 = calculateValuePlayer(poGame.FirstPlayer);

			Controller player2;
			if (poGame.CurrentPlayer == poGame.FirstPlayer)
				player2 = poGame.CurrentOpponent;
			else
				player2 = poGame.CurrentPlayer;

			float score2 = calculateValuePlayer(player2);

			finalScore = score1 - score2;
			finalScore = finalScore / Math.Max(score1, score2);
			finalScore /= 2;
			finalScore += 0.5f;

			return finalScore;
		}

		private static float calculateValuePlayer(Controller player)
		{
			float score = 0;
			float BoardMana = 0;
			foreach (Minion minion in player.BoardZone.GetAll())
			{
				BoardMana += minion.Cost * MINION_COST_IMPORTANCE;
			}
			foreach (Spell spell in player.SecretZone.GetAll())
			{
				BoardMana += spell.Cost * SECRET_COST_IMPORTANCE;
			}
			foreach (IPlayable card in player.HandZone.GetAll())
			{
				BoardMana += card.Cost * CARD_COST_IMPORTANCE;
			}
			if (player.Hero.Weapon != null)
				BoardMana += player.Hero.Weapon.Cost * WEAPON_COST_IMPORTANCE;

			score = player.Hero.Health * HEALTH_IMPORTANCE + BoardMana + player.DeckZone.Count * DECK_REMAINING_IMPORTANCE
				+ player.BaseMana * MANA_IMPORTANCE;

			if (score == 0)
				score = 0.0001f;

			return score;
		}
	}
}


// relacion value-tempo
