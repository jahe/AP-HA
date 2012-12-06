using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AP_HA
{
    [Serializable()]
    public class ShortCut
    {
        public String Name { get; private set; }
        private List<Key> Keys { get; set; }
        private List<MouseButton> MouseButtons { get; set; }
        private Wheel MouseWheelDirection { get; set; }
        private MouseMoveDirection MoveDirection { get; set; }

        public event Action Execute;

        public ShortCut()
        {
            Name = "";
            Keys = new List<Key>();
            MouseButtons = new List<MouseButton>();
            MouseWheelDirection = Wheel.None;
            MoveDirection = MouseMoveDirection.None;
        }

        public ShortCut(String name)
            : this()
        {
            Name = name;
        }

        public void reset()
        {
            Keys = new List<Key>();
            MouseButtons = new List<MouseButton>();
            MouseWheelDirection = Wheel.None;
            MoveDirection = MouseMoveDirection.None;
        }

        public override string ToString()
        {
            String retValue = "";

            foreach (Key k in Keys)
            {
                retValue += k.ToString() + " + ";
            }

            foreach (MouseButton mb in MouseButtons)
            {
                retValue += mb.ToString() + " + ";
            }

            if (MouseWheelDirection != Wheel.None)
            {
                retValue += "MouseWheel" + MouseWheelDirection.ToString() + " + ";
            }

            if (MoveDirection != MouseMoveDirection.None)
            {
                retValue += "MouseMovement" + MoveDirection.ToString();
            }

            return retValue;
        }

        public void executeFunction()
        {
            if (Execute != null)
            {
                Execute();
            }
        }

        public void register(Key k)
        {
            if (!Keys.Contains(k))
                Keys.Add(k);
        }

        public void register(MouseButton mb)
        {
            if (!MouseButtons.Contains(mb))
                MouseButtons.Add(mb);
        }

        public void register(Wheel wheelDir)
        {
            MouseWheelDirection = wheelDir;
        }

        public void register(MouseMoveDirection moveDir)
        {
            MoveDirection = moveDir;
        }

        public bool Remove(Key k)
        {
            return Keys.Remove(k);
        }

        public bool Remove(MouseButton mb)
        {
            return MouseButtons.Remove(mb);
        }

        public bool matches(ShortCut sc)
        {
            if (MoveDirection != sc.MoveDirection)
                return false;
            if (MouseWheelDirection != sc.MouseWheelDirection)
                return false;
            if (!ShortCut.ScrambledEquals<MouseButton>(MouseButtons, sc.MouseButtons))
                return false;
            if (!ShortCut.ScrambledEquals<Key>(Keys, sc.Keys))
                return false;

            return true;
        }

        public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }
    }
}
