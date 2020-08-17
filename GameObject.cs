using System;
using System.Linq;
namespace ConsoleGameProj
{
    class GameObject
    {
        public int currentRotation = 1;
        public int type;
        public Block[] blocks;        //    0    1   2   3
        public GameObject(int pieceNo)//    #    L   S   Z   
        {
            type = pieceNo;
            switch (pieceNo)
            {
                case 0:
                    blocks = new Block[]{
                        new Block(1,0,'A',ConsoleColor.Red),
                        new Block(2,0,'A',ConsoleColor.Red),
                        new Block(1,1,'A',ConsoleColor.Red),
                        new Block(2,1,'A',ConsoleColor.Red)
                    };
                    break;
                case 1:
                    blocks = new Block[]{
                        new Block(1,0,'B',ConsoleColor.Blue),
                        new Block(1,1,'B',ConsoleColor.Blue),
                        new Block(1,2,'B',ConsoleColor.Blue),
                        new Block(2,2,'B',ConsoleColor.Blue)
                    };
                    break;
                case 2:
                    blocks = new Block[]{
                        new Block(2,0,'C',ConsoleColor.Green),
                        new Block(3,0,'C',ConsoleColor.Green),
                        new Block(1,1,'C',ConsoleColor.Green),
                        new Block(2,1,'C',ConsoleColor.Green)
                    };
                    break;
                case 3:
                    blocks = new Block[]{
                        new Block(1,0,'D',ConsoleColor.Yellow),
                        new Block(2,0,'D',ConsoleColor.Yellow),
                        new Block(2,1,'D',ConsoleColor.Yellow),
                        new Block(3,1,'D',ConsoleColor.Yellow)
                    };
                    break;
                case 4:
                    blocks = new Block[]{
                        new Block(1, 0, 'E',ConsoleColor.Cyan),
                        new Block(1, 1, 'E',ConsoleColor.Cyan),
                        new Block(1, 2, 'E',ConsoleColor.Cyan),
                        new Block(1, 3, 'E',ConsoleColor.Cyan)
                    };
                    break;
                case 5:
                    blocks = new Block[]{
                        new Block(2,0,'F',ConsoleColor.Magenta),
                        new Block(2,1,'F',ConsoleColor.Magenta),
                        new Block(2,2,'F',ConsoleColor.Magenta),
                        new Block(1,2,'F',ConsoleColor.Magenta)
                    };
                    break;
                case 6:
                    blocks = new Block[]{
                        new Block(2,0,'G'),
                        new Block(1,1,'G'),
                        new Block(2,1,'G'),
                        new Block(3,1,'G')
                    };
                    break;
            }
        }
        public void translate(Direction direction)
        {
            switch (direction)
            {
                case Direction.RIGHT:
                    {
                        var canMoveRight = true;
                        foreach (var block in blocks)
                        {
                            if (block.isCollide(Direction.RIGHT, blocks))
                            {
                                canMoveRight = false;
                                break;
                            }
                        }
                        if (canMoveRight)
                            foreach (var block in blocks)
                                block.x += 1;
                        break;
                    }
                case Direction.LEFT:
                    {
                        var canMoveLeft = true;
                        foreach (var block in blocks)
                        {
                            if (block.isCollide(Direction.LEFT, blocks))
                            {
                                canMoveLeft = false;
                                break;
                            }
                        }
                        if (canMoveLeft)
                            foreach (var block in blocks)
                                block.x -= 1;
                        break;
                    }
                case Direction.UP:
                    {
                        foreach (var block in blocks)
                            block.y -= 1;
                        break;
                    }
                case Direction.DOWN:
                    {
                        var canMoveDown = true;
                        foreach (var block in blocks)
                        {
                            if (block.isCollide(Direction.DOWN, blocks))
                            {
                                canMoveDown = false;
                                break;
                            }
                        }
                        if (canMoveDown)
                            foreach (var block in blocks)
                                block.y += 1;
                        else
                            Program.OnGameObjectCollidesFloor(this);
                        break;
                    }

            }
            Program.RenderBlocks();

        }
        public void Rotate()
        {
            int tempRotation = currentRotation + 1;
            if (tempRotation > 4) tempRotation = 1;
            tempRotation = Math.Clamp(tempRotation, 1, 4);
            switch (type)
            {
                case 1:
                    {
                        var baseX = blocks[1].x;
                        var baseY = blocks[1].y;
                        switch (tempRotation)
                        {
                            case 1:
                                {
                                    var checkVar =
                                       from checkblock in Program.allBlocks
                                       where checkblock.isEquals(
                                           new Block(baseX, baseY - 1, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                           new Block(baseX, baseY + 1, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                           new Block(baseX + 1, baseY + 1, 'x', ConsoleColor.White, false))
                                       select checkblock;
                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX, baseY - 1);
                                        blocks[2].changePos(baseX, baseY + 1);
                                        blocks[3].changePos(baseX + 1, baseY + 1);

                                    }
                                    break;
                                }
                            case 2:
                                {
                                    var checkVar =
                                      from checkblock in Program.allBlocks
                                      where checkblock.isEquals(
                                          new Block(baseX + 1, baseY, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX - 1, baseY, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX - 1, baseY + 1, 'x', ConsoleColor.White, false))
                                      select checkblock;

                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX + 1, baseY);
                                        blocks[2].changePos(baseX - 1, baseY);
                                        blocks[3].changePos(baseX - 1, baseY + 1);

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    var checkVar =
                                      from checkblock in Program.allBlocks
                                      where checkblock.isEquals(
                                          new Block(baseX, baseY + 1, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX, baseY - 1, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX - 1, baseY - 1, 'x', ConsoleColor.White, false))
                                      select checkblock;
                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX, baseY + 1);
                                        blocks[2].changePos(baseX, baseY - 1);
                                        blocks[3].changePos(baseX - 1, baseY - 1);
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    var checkVar =
                                      from checkblock in Program.allBlocks
                                      where checkblock.isEquals(
                                          new Block(baseX - 1, baseY, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX + 1, baseY, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX + 1, baseY - 1, 'x', ConsoleColor.White, false))
                                      select checkblock;
                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX - 1, baseY);
                                        blocks[2].changePos(baseX + 1, baseY);
                                        blocks[3].changePos(baseX + 1, baseY - 1);
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        if (tempRotation % 2 != 0)
                        {
                            var checkVar =
                                from checkblock in Program.allBlocks
                                where checkblock.isEquals(new Block(blocks[3].x + 1, blocks[3].y - 1, 'x', ConsoleColor.White, false)) || checkblock.isEquals(new Block(blocks[3].x - 1, blocks[3].y, 'x', ConsoleColor.White, false))
                                select checkblock;
                            if (!checkVar.Any())
                            {
                                currentRotation = tempRotation;
                                blocks[0].changePos(blocks[3].x, blocks[3].y + -1);
                                blocks[1].changePos(blocks[3].x + 1, blocks[3].y - 1);
                                blocks[2].changePos(blocks[3].x - 1, blocks[3].y);
                            }
                        }
                        else
                        {
                            var checkVar =
                                from checkblock in Program.allBlocks
                                where checkblock.isEquals(new Block(blocks[3].x + 1, blocks[3].y, 'x', ConsoleColor.White, false)) || checkblock.isEquals(new Block(blocks[3].x + 1, blocks[3].y + 1, 'x', ConsoleColor.White, false))
                                select checkblock;
                            if (!checkVar.Any())
                            {
                                currentRotation = tempRotation;
                                blocks[0].changePos(blocks[3].x + 1, blocks[3].y);
                                blocks[1].changePos(blocks[3].x + 1, blocks[3].y + 1);
                                blocks[2].changePos(blocks[3].x, blocks[3].y - 1);
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        if (tempRotation % 2 != 0)
                        {
                            var checkVar =
                                from checkblock in Program.allBlocks
                                where checkblock.isEquals(new Block(blocks[2].x - 1, blocks[2].y - 1, 'x', ConsoleColor.White, false)) || checkblock.isEquals(new Block(blocks[2].x, blocks[2].y - 1, 'x', ConsoleColor.White, false))
                                select checkblock;
                            if (!checkVar.Any())
                            {
                                currentRotation = tempRotation;
                                blocks[0].changePos(blocks[2].x - 1, blocks[2].y - 1);
                                blocks[1].changePos(blocks[2].x, blocks[2].y - 1);
                                blocks[3].changePos(blocks[2].x + 1, blocks[2].y);
                            }
                        }
                        else
                        {
                            var checkVar =
                                from checkblock in Program.allBlocks
                                where checkblock.isEquals(new Block(blocks[2].x + 1, blocks[2].y - 1, 'x', ConsoleColor.White, false)) || checkblock.isEquals(new Block(blocks[2].x, blocks[2].y + 1, 'x', ConsoleColor.White, false))
                                select checkblock;
                            if (!checkVar.Any())
                            {
                                currentRotation = tempRotation;
                                blocks[0].changePos(blocks[2].x + 1, blocks[2].y - 1);
                                blocks[1].changePos(blocks[2].x + 1, blocks[2].y);
                                blocks[3].changePos(blocks[2].x, blocks[2].y + 1);
                            }
                        }
                        break;
                    }
                case 4:
                    {

                        if (tempRotation % 2 == 0)
                        {
                            var checkVar =
                                from checkblock in Program.allBlocks
                                where checkblock.isEquals(new Block(blocks[2].x - 2, blocks[2].y, 'x', ConsoleColor.White, false)) || checkblock.isEquals(new Block(blocks[2].x - 1, blocks[2].y, 'x', ConsoleColor.White, false)) || checkblock.isEquals(new Block(blocks[2].x + 1, blocks[2].y, 'x', ConsoleColor.White, false))
                                select checkblock;
                            if (!checkVar.Any())
                            {
                                currentRotation = tempRotation;
                                blocks[0].changePos(blocks[2].x - 2, blocks[2].y);
                                blocks[1].changePos(blocks[2].x - 1, blocks[2].y);
                                blocks[3].changePos(blocks[2].x + 1, blocks[2].y);
                            }
                        }
                        else
                        {
                            var checkVar =
                                from checkblock in Program.allBlocks
                                where checkblock.isEquals(new Block(blocks[2].x, blocks[2].y - 2, 'x', ConsoleColor.White, false)) ||
                                        checkblock.isEquals(new Block(blocks[2].x, blocks[2].y - 1, 'x', ConsoleColor.White, false)) ||
                                        checkblock.isEquals(new Block(blocks[2].x, blocks[2].y + 1, 'x', ConsoleColor.White, false))
                                select checkblock;
                            if (!checkVar.Any())
                            {
                                currentRotation = tempRotation;
                                blocks[0].changePos(blocks[2].x, blocks[2].y - 2);
                                blocks[1].changePos(blocks[2].x, blocks[2].y - 1);
                                blocks[3].changePos(blocks[2].x, blocks[2].y + 1);
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        var baseX = blocks[1].x;
                        var baseY = blocks[1].y;
                        switch (tempRotation)
                        {
                            case 1:
                                {
                                    var checkVar =
                                       from checkblock in Program.allBlocks
                                       where checkblock.isEquals(
                                           new Block(baseX, baseY - 1, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                           new Block(baseX, baseY + 1, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                           new Block(baseX - 1, baseY + 1, 'x', ConsoleColor.White, false))
                                       select checkblock;
                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX, baseY - 1);
                                        blocks[2].changePos(baseX, baseY + 1);
                                        blocks[3].changePos(baseX - 1, baseY + 1);

                                    }
                                    break;
                                }
                            case 2:
                                {
                                    var checkVar =
                                      from checkblock in Program.allBlocks
                                      where checkblock.isEquals(
                                          new Block(baseX + 1, baseY, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX - 1, baseY, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX - 1, baseY - 1, 'x', ConsoleColor.White, false))
                                      select checkblock;

                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX + 1, baseY);
                                        blocks[2].changePos(baseX - 1, baseY);
                                        blocks[3].changePos(baseX - 1, baseY - 1);

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    var checkVar =
                                      from checkblock in Program.allBlocks
                                      where checkblock.isEquals(
                                          new Block(baseX, baseY + 1, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX, baseY - 1, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX + 1, baseY - 1, 'x', ConsoleColor.White, false))
                                      select checkblock;
                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX, baseY + 1);
                                        blocks[2].changePos(baseX, baseY - 1);
                                        blocks[3].changePos(baseX + 1, baseY - 1);
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    var checkVar =
                                      from checkblock in Program.allBlocks
                                      where checkblock.isEquals(
                                          new Block(baseX - 1, baseY, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX + 1, baseY, 'x', ConsoleColor.White, false)) || checkblock.isEquals(
                                          new Block(baseX + 1, baseY + 1, 'x', ConsoleColor.White, false))
                                      select checkblock;
                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX - 1, baseY);
                                        blocks[2].changePos(baseX + 1, baseY);
                                        blocks[3].changePos(baseX + 1, baseY + 1);
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case 6:
                    {
                        var baseX = blocks[2].x;
                        var baseY = blocks[2].y;
                        switch (tempRotation)
                        {
                            case 1:
                                {
                                    var checkVar =
                                       from checkblock in Program.allBlocks
                                       where checkblock.isEquals(
                                           new Block(baseX + 1, baseY, 'x', ConsoleColor.White, false))
                                       select checkblock;
                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX, baseY - 1);
                                        blocks[1].changePos(baseX - 1, baseY);
                                        blocks[3].changePos(baseX + 1, baseY);

                                    }
                                    break;
                                }
                            case 2:
                                {
                                    var checkVar =
                                      from checkblock in Program.allBlocks
                                      where checkblock.isEquals(
                                          new Block(baseX, baseY + 1, 'x', ConsoleColor.White, false))
                                      select checkblock;

                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX + 1, baseY);
                                        blocks[1].changePos(baseX, baseY - 1);
                                        blocks[3].changePos(baseX, baseY + 1);

                                    }
                                    break;
                                }
                            case 3:
                                {
                                    var checkVar =
                                      from checkblock in Program.allBlocks
                                      where checkblock.isEquals(
                                          new Block(baseX - 1, baseY, 'x', ConsoleColor.White, false))
                                      select checkblock;
                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX, baseY + 1);
                                        blocks[1].changePos(baseX + 1, baseY);
                                        blocks[3].changePos(baseX - 1, baseY);
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    var checkVar =
                                      from checkblock in Program.allBlocks
                                      where checkblock.isEquals(
                                          new Block(baseX, baseY - 1, 'x', ConsoleColor.White, false))
                                      select checkblock;
                                    if (!checkVar.Any())
                                    {
                                        currentRotation = tempRotation;
                                        blocks[0].changePos(baseX - 1, baseY);
                                        blocks[1].changePos(baseX, baseY + 1);
                                        blocks[3].changePos(baseX, baseY - 1);
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }
            Program.RenderBlocks();
        }
    }
}
