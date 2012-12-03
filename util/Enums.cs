using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AP_HA
{
    [Serializable()]
    public enum Wheel { Up, Down, None };
    [Serializable()]
    public enum Tool { ZoomIn, ZoomOut, Move, None };
    [Serializable()]
    public enum MouseMoveDirection { Left, Up, Right, Down, None };
}
