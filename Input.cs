using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ouroboros
{
    using static Constants;
    public static class Input
    {
        public static int mouseX, mouseY;
        public static bool lDown, rDown, lHandled, rHandled;
        public static int inputMode;
        public static Data data;

        public static void MouseDown(MouseEventArgs e, Size displaySize)
        {
            if (inputMode == InputModeEdit) {
                data.Edit(e, displaySize);
            }
            else
            {
                data.Play(e, displaySize);
            }
            if (e.Button == MouseButtons.Left)
            {
                lDown = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                rDown = true;
            }
        }
        public static void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lDown = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                rDown = false;
            }
        }
        public static void MouseMove(MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
        }
        public static void KeyTyped(char c)
        {
            if (c == 'm' || c == 'M')
            {
                if (inputMode == InputModeEdit)
                {
                    inputMode = InputModePlay;
                }
                else
                {
                    data.ResetOuroboros();
                    inputMode = InputModeEdit;
                }
            }
            if (c == 'r' || c == 'R')
            {
                data.ResetOuroboros();
            }
            if (c == 'c' || c == 'C')
            {
                data.ClearLevel();
            }
            if (c == 'o' || c == 'O')
            {
                data.Open();
            }
            if (c == 's' || c == 'S')
            {
                data.Save();
            }
            if (c == 'q' || c == 'Q')
            {
                Application.Exit();
            }
        }
    }
}
