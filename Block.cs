using System.Linq;
using System;
namespace ConsoleGameProj
{

    class Block
    {
        public int x, y;
        public char character;
        public ConsoleColor color;
        public Block(int posx, int posy, char character, ConsoleColor color = ConsoleColor.White, bool addToList = true)
        {
            x = posx;
            this.color = color;
            y = posy;
            this.character = character;
            if (addToList)
                Program.allBlocks.Add(this);
        }
        public void changePos(int newPosX, int newPosY)
        {
            x = newPosX;
            y = newPosY;
        }
        public bool isCollide(Direction direction, Block[] blocksToIgnore)
        {
            var foundBlock =
                from block in Program.allBlocks
                where block.y == (this.y + (direction.Equals(Direction.DOWN) ? 1 : direction.Equals(Direction.UP) ? -1 : 0)) && block.x == (this.x + (direction.Equals(Direction.RIGHT) ? 1 : direction.Equals(Direction.LEFT) ? -1 : 0)) && !blocksToIgnore.Contains(block)
                select block;

            return foundBlock.Any();
        }

        public bool isEquals(Block other) => this.x == other.x && this.y == other.y;
        public override string ToString() => x + " : " + y;
    }
}