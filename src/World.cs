using System.IO;
using System.Linq;
using static PathfindingTest.Constants;

namespace PathfindingTest
{
    public class World
    {
        public int WorldIdx { get; }

        private readonly Tile[] tiles;

        public World(int worldIdx)
        {
            WorldIdx = worldIdx;

            tiles = Enumerable.Range(0, MAX_TILENUM).Select(_ => new Tile(this)).ToArray();

            OpenTile();
        }

        private void OpenTile()
        {
            using var reader = new BinaryReader(File.Open($"data/world{WorldIdx}-tmap.bin", FileMode.Open, FileAccess.Read, FileShare.Read));

            var processNum = reader.ReadInt32();
            var tileNum = reader.ReadInt32();

            for (var i = 0; i < tileNum; i++)
            {
                tiles[i].TileContext.ProcessIdx = reader.ReadInt32();
                tiles[i].TileContext.TileIdx = reader.ReadInt32();
                tiles[i].TileContext.PosX = reader.ReadInt32();
                tiles[i].TileContext.PosY = reader.ReadInt32();
                tiles[i].TileContext.IsEdge = reader.ReadInt32();
                tiles[i].TileContext.Unk = reader.ReadInt32();

                for (var j = 0; j < tiles[i].TileListEx.TileList.Length; j++)
                {
                    for (var k = 0; k < tiles[i].TileListEx.TileList[j].TileIdxCurProcessLayer.Length; k++)
                        tiles[i].TileListEx.TileList[j].TileIdxCurProcessLayer[k] = reader.ReadInt32();

                    for (var k = 0; k < tiles[i].TileListEx.TileList[j].TileIdxOthBoundary.Length; k++)
                        for (var n = 0; n < tiles[i].TileListEx.TileList[j].TileIdxOthBoundary[k].Length; n++)
                            tiles[i].TileListEx.TileList[j].TileIdxOthBoundary[k][n] = reader.ReadInt32();

                    tiles[i].TileListEx.TileList[j].TileIdxOthProcessLayerNum = reader.ReadInt32();
                    tiles[i].TileListEx.TileList[j].TileIdxNum = reader.ReadInt32();
                }

                for (var j = 0; j < tiles[i].TileContext.TileAttr.Data.Length; j++)
                    tiles[i].TileContext.TileAttr.Data[j] = reader.ReadInt32();
            }
        }

        public int GetCellAttrEx(in int posX, in int posY)
        {
            var tileIdx = ((posX) >> TILE_WIDTHEXP) | (((posY) >> TILE_HEIGHTEXP) << HOR_TILENUMEXP);
            var tileAttrDataIdx = ((posX) & TILE_WIDTHMASK) | (((posY) & TILE_HEIGHTMASK) << TILE_WIDTHEXP);

            if (tileIdx >= MAX_TILENUM || tileAttrDataIdx >= TILE_WIDTH * TILE_HEIGHT)
            {
                // TODO: Log Index error (posX, posY)
                return 0;
            }

            return tiles[tileIdx].TileContext.TileAttr.Data[tileAttrDataIdx];
        }

        public void SetCellAttrEx(in int posX, in int posY, in int attr)
        {
            var tileIdx = ((posX) >> TILE_WIDTHEXP) | (((posY) >> TILE_HEIGHTEXP) << HOR_TILENUMEXP);
            var tileAttrDataIdx = ((posX) & TILE_WIDTHMASK) | (((posY) & TILE_HEIGHTMASK) << TILE_WIDTHEXP);

            if (tileIdx >= MAX_TILENUM || tileAttrDataIdx >= TILE_WIDTH * TILE_HEIGHT)
            {
                // TODO: Log Index error (posX, posY)
                return;
            }

            tiles[tileIdx].TileContext.TileAttr.Data[tileAttrDataIdx] = attr;
        }

        public bool IsMovableCell(in int posX, in int posY, bool isMob = false)
        {
            var attr = GetCellAttrEx(posX, posY);

            if ((attr & MASK_OVERATTCK0) > 0)
                return false;

            return attr == M_MOVABLE || attr == T_MOVABLE && !isMob;
        }

        public void AddToCell(in int posX, in int posY)
        {
            var attr = GetCellAttrEx(posX, posY);
            if ((attr & 0x0F) < 0x0F)
            {
                attr += 0x00000001;
                SetCellAttrEx(posX, posY, attr);
            }
        }

        public void DelFromCell(in int posX, in int posY)
        {
            var attr = GetCellAttrEx(posX, posY);
            if ((attr & 0x0F) > 0)
            {
                attr -= 0x00000001;
                SetCellAttrEx(posX, posY, attr);
            }
        }
    }
}
