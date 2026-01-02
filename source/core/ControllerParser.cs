namespace SeeloewenMapper.core
{
    internal static class ControllerParser
    {
        public static VirtualState FromDS4(byte[] values)
        {
            bool squarePressed = (values[5] & 0b00010000) != 0;
            bool circlePressed = (values[5] & 0b01000000) != 0;
            bool trianglePressed = (values[5] & 0b10000000) != 0;
            bool crossPressed = (values[5] & 0b00100000) != 0;

            int dir = values[5] & 0x0f;
            bool upPressed = dir == 0 || dir == 1 || dir == 7;
            bool downPressed = dir == 3 || dir == 4 || dir == 5;
            bool leftPressed = dir == 7 || dir == 6 || dir == 5;
            bool rightPressed = dir == 3 || dir == 2 || dir == 1;

            bool r1Pressed = (values[6] & 0b00000010) != 0;
            bool l1Pressed = (values[6] & 0b00000001) != 0;

            bool optionsPressed = (values[6] & 0b00100000) != 0;
            bool sharePressed = (values[6] & 0b00010000) != 0;

            double m = (short.MaxValue - short.MinValue) / 255;
            short leftStickX = (short)(m * values[1] + short.MinValue);
            short rightStickX = (short)(m * values[3] + short.MinValue);
            //short leftStickY = (short)(m * (255 - values[2]) - short.MinValue);
            short rightStickY = (short)(m * (255 - values[4]) + short.MinValue);
            short leftStickY = (short)(Math.Max(Math.Min((m * (255 - values[2]) + short.MinValue) * 1.2, short.MaxValue), short.MinValue));

            return new VirtualState(squarePressed,
                trianglePressed,
                crossPressed,
                circlePressed,
                leftStickX,
                leftStickY,
                rightStickX,
                rightStickY,
                l1Pressed,
                r1Pressed,
                values[8],
                values[9],
                upPressed,
                downPressed,
                leftPressed,
                rightPressed,
                optionsPressed,
                sharePressed);

        }
    }
}
