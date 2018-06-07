﻿using Models.ScriptableObjects;

namespace Models.Arena
{
    public class ArenaGameManager
    {
        /// <summary>
        /// Your CPU Behavior
        /// </summary>
        [Inject]
        public PlayerCpuBehavior PlayerCpu { get; set; }

        /// <summary>
        /// Arena
        /// </summary>
        [Inject]
        public Arena Arena { get; set; }

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Emulate Game
        /// </summary>
        public void EmulateGame()
        {
            var count = 0;
            while (true)
            {
                // init current player
                var player = BattleArena.ActiveState == BattleState.YourTurn
                    ? Arena.YourPlayer
                    : Arena.EnemyPlayer;

                if (count > 100)
                {
                    break;
                }

                count++;

                if (!BattleArena.IsGameOver(player))
                {
                    MakePlayerTurn(player);
                    continue;
                }

                var winPlayer = BattleArena.ActiveState == BattleState.YourTurn
                    ? Arena.EnemyPlayer
                    : Arena.YourPlayer;
                if (winPlayer.Status != PlayerStatus.Dead)
                {
                    BattleArena.TurnHistoty.AddBattleLog("\"" + winPlayer.Name + "\" WINS!");
                }

                break;
            }
        }

        /// <summary>
        /// Make player turn
        /// </summary>
        private void MakePlayerTurn(Player player)
        {
            // Init player Cpu Behavior
            PlayerCpu.Init(player);
            PlayerCpu.InitTurn();
            PlayerCpu.MakeBattleTurn();
        }
    }
}