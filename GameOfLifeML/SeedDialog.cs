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
    public partial class SeedDialog : Form
    {
        public SeedDialog()
        {
            InitializeComponent();
        }

        public int Seed
        {
            get
            {
                return (int)SeedNum.Value;
            }
            set
            {
                SeedNum.Value = value;
            }
        }

        private void RandomizeSeed_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Seed = rnd.Next();
        }
    }
}
