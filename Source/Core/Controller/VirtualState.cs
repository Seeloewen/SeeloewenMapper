namespace SeeloewenMapper.Core.Controller
{
    internal record VirtualState(bool XPressed,
            bool YPressed,
            bool APressed,
            bool BPressed,
            short leftStickX,
            short leftStickY,
            short rightStickX,
            short rightStickY,
            bool l1Pressed,
            bool r1Pressed,
            byte l2Value,
            byte r2Value,
            bool upPressed,
            bool downPressed,
            bool leftPressed,
            bool rightPressed,
            bool startPressed,
            bool backPressed,
            bool l3Pressed,
            bool r3Pressed
            )
    { }
}
