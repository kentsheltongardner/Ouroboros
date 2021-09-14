using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ouroboros
{
    using static Constants;
    public partial class OuroborosForm : Form
    {
        public Data data;

        public OuroborosForm()
        {
            InitializeComponent();
            data = new Data(7, 7);
            Input.data = data;
        }

        private void OuroborosForm_Paint(object sender, PaintEventArgs e)
        {
            Renderer.Render(data, e.Graphics, ClientSize);
        }

        private void Tick(object sender, EventArgs e)
        {
            Invalidate();
            data.Update();
        }

        private void OuroborosForm_MouseDown(object sender, MouseEventArgs e)
        {
            Input.MouseDown(e, ClientSize);
        }
        private void OuroborosForm_MouseUp(object sender, MouseEventArgs e)
        {
            Input.MouseUp(e);
        }
        private void OuroborosForm_MouseMove(object sender, MouseEventArgs e)
        {
            Input.MouseMove(e);
        }
        private void OuroborosForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Input.KeyTyped(e.KeyChar);
        }
    }
}
