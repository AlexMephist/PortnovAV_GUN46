using GamePrototype.Units;

namespace GamePrototype.Combat
{
 
    public sealed class CombatManager
    {
        private readonly Random _random = new();
        
              
        public Unit StartCombat(Unit player, Unit enemy) => PlayCombatRoutine(player, enemy);

        private Unit PlayCombatRoutine(Unit player, Unit enemy)
        {

            if (player is Player p)
            {
                var randomMode = (AttackMode)_random.Next(0, 2); // 0 = Melee, 1 = Ranged
                p.SetAttackMode(randomMode);
                Console.WriteLine($"[TIP] {p.Name} is currently in {randomMode} mode! Press 'S' to switch attack style.");
            }

            Console.WriteLine(GetCombatString());
            
            while (player.Health > 0 && enemy.Health > 0)
            {
                string? input = Console.ReadLine()?.Trim();

                if (input == "S" || input == "s")
                {
                    if (player is Player pl)
                    {
                        pl.ToggleAttackMode();
                    }
                    else
                    {
                        Console.WriteLine("Only Player can switch attack mode.");
                    }
                    continue; 
                }
                
                if (Enum.TryParse<RockPaperScissors>(input, out var rockPaperScissors))
                {
                    HandleCombatInput(player, enemy, rockPaperScissors);
                }
                else
                {
                    // Console.WriteLine(GetCombatString());
                }
            }

            if (player.Health > 0 && enemy.Health == 0)
            {
                return player;
            }
            else if (player.Health == 0 && enemy.Health > 0)
            {
                return enemy;
            }

            return null;

        }

        private string GetCombatString() => $"Type {RockPaperScissors.Rock} = {(int)RockPaperScissors.Rock}" +
            $"or {RockPaperScissors.Paper} = {(int)RockPaperScissors.Paper}" +
            $"or {RockPaperScissors.Scissors} = {(int)RockPaperScissors.Scissors}";

        private void HandleCombatInput(Unit player, Unit enemy, RockPaperScissors rockPaperScissors)
        {
            var enemyInput = (RockPaperScissors) _random.Next(1, 4);
            Console.WriteLine($"Result player = {rockPaperScissors} and enemy = {enemyInput}");
            switch (rockPaperScissors) 
            {
                // player hit
                case RockPaperScissors.Rock when enemyInput == RockPaperScissors.Scissors:
                    ApplyDamage(player, enemy);
                    break;
                case RockPaperScissors.Scissors when enemyInput == RockPaperScissors.Paper:
                    ApplyDamage(player, enemy);
                    break;
                case RockPaperScissors.Paper when enemyInput == RockPaperScissors.Rock:
                    ApplyDamage(player, enemy);
                    break;
                // enemy hit
                case RockPaperScissors.Scissors when enemyInput == RockPaperScissors.Rock:
                    ApplyDamage(enemy, player);
                    break;
                case RockPaperScissors.Paper when enemyInput == RockPaperScissors.Scissors:
                    ApplyDamage(enemy, player);
                    break;
                case RockPaperScissors.Rock when enemyInput == RockPaperScissors.Paper:
                    ApplyDamage(enemy, player);
                    break;
                default:
                    Console.WriteLine("Combatants tried to hit, but missed :(");
                    break;
            }
        }

        private void ApplyDamage(Unit attacker, Unit defender)
        {
            
            defender.ApplyDamage(attacker.GetUnitDamage());
            
            Console.WriteLine($"{attacker.Name} hits {defender.Name}. {defender.Name} health {defender.Health}/{defender.MaxHealth}");
           
            if (defender.Health == 0) 
            {
                Console.WriteLine($"{defender.Name} is dead!");
            }
        }
    }
}
