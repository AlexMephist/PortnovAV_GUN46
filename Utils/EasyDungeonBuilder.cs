using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using GamePrototype.Units;


namespace GamePrototype.Utils
{
    public class EasyDungeonBuilder : DungeonBuilder2
    
    {
        
        public override DungeonRoom BuildDungeon()
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom = new DungeonRoom("Monster", new Goblin(GameConstants.Goblin, 18, 18, 2));
            var emptyRoom = new DungeonRoom("Empty");
            var lootRoom = new DungeonRoom("Loot1", new Gold());
            var lootStoneRoom = new DungeonRoom("Loot1", new Grindstone("Stone"));
            var finalRoom = new DungeonRoom("Final", new Grindstone("Stone1"));


            enter.TrySetDirection(Direction.Right, monsterRoom);
            enter.TrySetDirection(Direction.Left, emptyRoom);

            monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
            monsterRoom.TrySetDirection(Direction.Left, emptyRoom);

            emptyRoom.TrySetDirection(Direction.Forward, lootStoneRoom);

            lootRoom.TrySetDirection(Direction.Forward, finalRoom);
            lootStoneRoom.TrySetDirection(Direction.Forward, finalRoom);

            return enter;
        }
    }
}
