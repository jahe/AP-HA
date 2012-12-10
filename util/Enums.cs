using System;

namespace AP_HA
{
    [Serializable()]
    public enum Wheel { Up, Down, None };
    [Serializable()]
    public enum Tool { CropSize, CropLocation, Pen, ZoomIn, ZoomOut, Move, None };
    [Serializable()]
    public enum MouseMoveDirection { Left, Up, Right, Down, None };
}
