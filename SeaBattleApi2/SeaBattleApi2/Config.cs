﻿namespace SeaBattleApi2
{
    static class Config
    {
        public static int Height { get; } = 10;
        public static int Width { get; } = 10;
        public static Dictionary<int, int> Ships { get; } = new Dictionary<int, int>()
    {
        { 2, 4 },
        { 3, 3 },
        { 4, 2 },
        { 6, 1 }
    };
    }
}
