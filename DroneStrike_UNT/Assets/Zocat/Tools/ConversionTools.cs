namespace Zocat
{
    public static class ConversionTools
    {
        public static int ToInt(ref this float value)
        {
            return (int)value;
        }

        public static float ToFloat(ref this int value)
        {
            return value;
        }
    }
}