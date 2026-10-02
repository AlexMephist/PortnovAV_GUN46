using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;

namespace GamePrototype.Utils
{
    public abstract class DungeonBuilder2
    {
        public LevelSelection levelSelection;
        public static DungeonRoom BuildDungeon()
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom = new DungeonRoom("Monster", UnitFactoryDemo.CreateGoblinEnemy());
            var emptyRoom = new DungeonRoom("Empty");
            var lootRoom = new DungeonRoom("Loot1", new Gold());
            var lootStoneRoom = new DungeonRoom("Loot1", new Grindstone("Stone"));
            var finalRoom = new DungeonRoom("Final", new Grindstone("Stone1"));

            Console.WriteLine("Choose the difficulty level for the dungeon: '0' - easy '1'- hard");

            if (Enum.TryParse<LevelSelection>(Console.ReadLine(), out var levelSelection))
            {
                switch (levelSelection)
                {
                    case LevelSelection.easy:

                        Console.WriteLine("You have chosen: - easy");
                        
                        enter.TrySetDirection(Direction.Right, monsterRoom);
                        enter.TrySetDirection(Direction.Left, emptyRoom);

                        monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
                        monsterRoom.TrySetDirection(Direction.Left, emptyRoom);

                        emptyRoom.TrySetDirection(Direction.Forward, lootStoneRoom);

                        lootRoom.TrySetDirection(Direction.Forward, finalRoom);
                        lootStoneRoom.TrySetDirection(Direction.Forward, finalRoom);

                        return enter;
                    
                    case LevelSelection.hard:

                        Console.WriteLine("You have chosen: - hard");
                        
                        enter.TrySetDirection(Direction.Right, monsterRoom);
                        enter.TrySetDirection(Direction.Left, emptyRoom);

                        monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
                        monsterRoom.TrySetDirection(Direction.Left, emptyRoom);

                        emptyRoom.TrySetDirection(Direction.Forward, lootStoneRoom);

                        lootStoneRoom.TrySetDirection(Direction.Right, monsterRoom);
                        lootStoneRoom.TrySetDirection(Direction.Left, emptyRoom);

                        monsterRoom.TrySetDirection(Direction.Left, emptyRoom);
                        emptyRoom.TrySetDirection(Direction.Forward, finalRoom);

                        lootRoom.TrySetDirection(Direction.Forward, finalRoom);
                        lootStoneRoom.TrySetDirection(Direction.Forward, finalRoom);

                        return enter;
                }
            }


            return enter;
        }
    }
}
