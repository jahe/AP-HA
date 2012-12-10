﻿using System;

namespace AP_HA
{
    [Serializable()]
    public enum Wheel { Up, Down, None };
    [Serializable()]
    public enum Tool { Crop, Pen, Eraser, ZoomIn, ZoomOut, Move, None };
    [Serializable()]
    public enum MouseMoveDirection { Left, Up, Right, Down, None };
}
