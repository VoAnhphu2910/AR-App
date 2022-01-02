using System.Collections.Generic;
using UnityEngine;

public class DataStorage
{
    public static GameObject[] furnitures;
    public static Texture[] images;
    public static List<string> type;

    public static bool isStart = true;

    public struct Data
    {
        public string text;
        public string linkModel;
        public string name;
        public string type;
        public string image;
    }

    public static Data[] row;
}
