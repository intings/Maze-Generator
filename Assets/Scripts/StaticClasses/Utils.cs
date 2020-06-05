namespace StaticClasses
{
    public static class Utils
    {
        public static bool Percentage(int rate)
        {
            return (UnityEngine.Random.Range(1, 100) > rate);
        }

        public static int RandomSelectionFromArray(int[] items)
        {
            if (items.Length == 0) return -1;
            int r = UnityEngine.Random.Range(1, 25 * items.Length);
            int direction = 0;
            if (r < 26) direction = items[0];
            else if (r > 25 && r < 51) direction = items[1];
            else if (r > 50 && r < 76) direction = items[2];
            else if (r > 75) direction = items[3];
            return direction;
        }
    }
}
