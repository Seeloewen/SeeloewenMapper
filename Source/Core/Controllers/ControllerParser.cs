namespace SeeloewenMapper.Core.Controllers
{
    internal static class ControllerParser
    {
        public static VirtualState FromDS4(byte[] values, ConnectionMode mode)
        {
            int o = mode == ConnectionMode.FULL ? 2 : 0 ; //This offset is needed in BT full mode

            bool squarePressed = (values[5 + o] & 0b00010000) != 0;
            bool circlePressed = (values[5 + o] & 0b01000000) != 0;
            bool trianglePressed = (values[5 + o] & 0b10000000) != 0;
            bool crossPressed = (values[5 + o] & 0b00100000) != 0;

            int dir = values[5 + o] & 0x0f;
            bool upPressed = dir == 0 || dir == 1 || dir == 7;
            bool downPressed = dir == 3 || dir == 4 || dir == 5;
            bool leftPressed = dir == 7 || dir == 6 || dir == 5;
            bool rightPressed = dir == 3 || dir == 2 || dir == 1;

            bool r1Pressed = (values[6 + o] & 0b00000010) != 0;
            bool l1Pressed = (values[6 + o] & 0b00000001) != 0;

            bool optionsPressed = (values[6 + o] & 0b00100000) != 0;
            bool sharePressed = (values[6 + o] & 0b00010000) != 0;

            bool l3Pressed = (values[6 + o] & 0b01000000) != 0;
            bool r3Pressed = (values[6 + o] & 0b10000000) != 0;

            double m = (short.MaxValue - short.MinValue) / 255;
            short leftStickX = (short)(m * values[1 + o] + short.MinValue);
            short rightStickX = (short)(m * values[3 + o] + short.MinValue);
            //short leftStickY = (short)(m * (255 - values[2 + o]) - short.MinValue);
            short rightStickY = (short)(m * (255 - values[4 + o]) + short.MinValue);
            //This is currently a bug fix for one of my controllers, shouldn't really affect normal-functioning controllers
            short leftStickY = (short)(Math.Max(Math.Min((m * (255 - values[2 + o]) + short.MinValue) * 1.2, short.MaxValue), short.MinValue));

            byte l2value = values[8 + o];
            byte r2value = values[9 + o];

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
                l2value,
                r2value,
                upPressed,
                downPressed,
                leftPressed,
                rightPressed,
                optionsPressed,
                sharePressed,
                l3Pressed,
                r3Pressed);

        }
    }
}
