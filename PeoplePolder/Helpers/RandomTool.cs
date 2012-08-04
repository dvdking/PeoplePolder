using System;

namespace PeoplePolder.Helpers
{
    public static class RandomTool
    {
        public static Random random = new Random();

        public static int RandInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static int RandInt(int max)
        {
            try
            {
                return random.Next(max);
            }
            catch
            {
                return 0;
            }
        }

        public static int RandInt()
        {
            return random.Next();
        }

        public static double RandDouble()
        {
            return random.NextDouble();
        }
        public static bool RandBool(float ratio)
        {
            return random.NextDouble() <= ratio / 100;
        }

        public static bool RandBool(double ratio)
        {
            return random.NextDouble() <= ratio / 100;
        }

        public static bool RandBool()
        {
            return random.NextDouble() <= 0.5;
        }

        public static int RandSign()
        {
            return random.NextDouble() <= 0.5 ? 1 : -1;
        }
    }
}