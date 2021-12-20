using UnityEngine;
using UnityEngine.UI;

public class DataStorage
{
    public static GameObject[] furnitures;
    public static Texture[] images;
    public static Button[] buttons;

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
