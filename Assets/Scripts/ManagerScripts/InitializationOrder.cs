namespace Grail
{
    public static class InitializationOrder
    {
        //Map building
        public const int TILE_DATA_MANAGER = 1;

        //Managers
        public const int DIALOGUE_MANAGER = TILE_DATA_MANAGER + 1;
        public const int TURNS_MANAGER = DIALOGUE_MANAGER + 1;

        // player
        public const int PLAYER_CONTROLLER = TURNS_MANAGER + 1;
        public const int PLAYER_STATS = PLAYER_CONTROLLER + 1;
        public const int PLAYER_INVENTORY = PLAYER_STATS + 1;

        //UI
        public const int UI_MANAGER = PLAYER_INVENTORY + 1;

    }
}
