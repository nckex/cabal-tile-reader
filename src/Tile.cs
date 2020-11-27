using System.Linq;
using static PathfindingTest.Constants;

namespace PathfindingTest
{
    public class Tile
    {
        public World World { get; }
        public TileContext TileContext { get; }
        public TileListEx TileListEx { get; }

        public Tile(World world)
        {
            World = world;
            TileContext = new TileContext();
            TileListEx = new TileListEx();
        }
    }

    public class TileContext
    {
        public int ProcessIdx { get; set; }
        public int TileIdx { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int IsEdge { get; set; }
        public int Unk { get; set; }
        public TileAttr TileAttr { get; }

        public TileContext()
        {
            TileAttr = new TileAttr();
        }
    }

    public class TileAttr
    {
        public int[] Data { get; }

        public TileAttr()
        {
            Data = new int[TILE_WIDTH * TILE_HEIGHT];
        }
    }

    public class TileList
    {
        public int[] TileIdxCurProcessLayer { get; }
        public int[][] TileIdxOthBoundary { get; }
        public int TileIdxOthProcessLayerNum { get; set; }
        public int TileIdxNum { get; set; }

        public TileList()
        {
            TileIdxCurProcessLayer = new int[_TILELIST_DATALEN];
            TileIdxOthBoundary = Enumerable.Range(0, TILELIST_MAXNUM).Select(_ => new int[_TILELIST_DATALEN]).ToArray();
        }
    }

    public class TileListEx
    {
        public TileList[] TileList { get; }

        public TileListEx()
        {
            TileList = Enumerable.Range(0, TILELIST_MAXNUM).Select(_ => new TileList()).ToArray();
        }
    }
}
