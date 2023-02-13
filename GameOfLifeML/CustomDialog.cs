using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLifeML
{
    public partial class CustomDialog : Form
    {
        public CustomDialog()
        {
            InitializeComponent();
        }

        public int Interval
        {
            get
            {
                return (int)TimeInterval.Value;
            }
            set
            {
                TimeInterval.Value = value;
            }
        }

        public int Universe_Width
        {
            get
            {
                return (int)UniverseWidth.Value;
            }
            set
            {
                UniverseWidth.Value = value;
            }
        }
        public int Universe_Height
        {
            get
            {
                return (int)UniverseHeight.Value;
            }
            set
            {
                UniverseHeight.Value = value;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {

        }

    }
}
