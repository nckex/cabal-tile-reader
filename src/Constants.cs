namespace PathfindingTest
{
    public static class Constants
    {
        public const int MASK_OVERATTCK0 = 0x10000000;

        public const int M_MOVABLE = 0x00000000;
        public const int M_UNMOVABLE = 0x01000010;
        public const int T_MOVABLE = 0x06000020;
        public const int T_UNMOVABLE = 0x07000030;

        public const int TILE_WIDTHEXP = 4;
        public const int TILE_HEIGHTEXP = 4;
        public const int TILE_WIDTHMASK = 0x0000000F;
        public const int TILE_HEIGHTMASK = 0x0000000F;

        public const int TILE_WIDTH = 16;
        public const int TILE_HEIGHT = 16;

        public const int TILELIST_MAXNUM = 9;
        public const int TILELIST_DATALEN = 11;
        public const int _TILELIST_DATALEN = (TILELIST_DATALEN + (5));

        public const int HOR_TILENUM = 16;          // ( TERR_WIDTH / TILE_WIDTH ) * MAX_HORTERRNUM
        public const int VER_TILENUM = 16;          // ( TERR_HEIGHT / TILE_HEIGHT ) * MAX_VERTERRNUM

        public const int HOR_TILENUMEXP = 4;        // 2^4 = HOR_TILENUM
        public const int VER_TILENUMEXP = 4;        // 2^4 = VER_TILENUM

        public const int MAX_TILENUM = 256;		    // ( TERR_WIDTH / TILE_WIDTH ) * ( TERR_HEIGHT / TILE_HEIGHT ) * MAX_TERRNUM
    }
}
