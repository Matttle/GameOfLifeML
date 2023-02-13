using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GameOfLifeML
{
    public partial class Form1 : Form
    {
        // The universe array
        bool[,] universe = new bool[30, 30]; 
        bool[,] nextUniverse = new bool[5, 5];

        // Number of cells alive
        private int cells;

        // View options
        private bool toroidal0Finite1; // 0/False/Toroidal 1/True/Finite
        private bool HUD;
        private bool Grid;
        private bool NeighborCount;

        // Current random seed
        private int seed;

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;
        Color brush = Color.Red;
        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 20; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running

            // Initialize settings
            graphicsPanel1.BackColor = Properties.Settings.Default.BackColor;
            timer.Interval = Properties.Settings.Default.Interval;
            gridColor = Properties.Settings.Default.GridColor;
            cellColor = Properties.Settings.Default.CellColor;
            universe = new bool[Properties.Settings.Default.UniverseWidth, Properties.Settings.Default.UniverseHeight];
            seed = Properties.Settings.Default.Seed;
            toroidal0Finite1 = Properties.Settings.Default.IsFinite;
            HUD = Properties.Settings.Default.HUD;
            Grid = Properties.Settings.Default.Grid;
            NeighborCount = Properties.Settings.Default.NeighborCount;


            // Initialize interval text
            Interval.Text = "Interval: " + timer.Interval;

            // Initialize random seed
            CurrentSeed.Text = "Seed: " + seed.ToString();
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            cells = 0;
            nextUniverse = new bool[universe.GetLength(0), universe.GetLength(1)];
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int nCount;
                    if (toroidal0Finite1)
                        nCount = CountNeighborsFinite(x, y);
                    else
                        nCount = CountNeighborsToroidal(x, y);

                    if (universe[x, y] == true) // Rules of the living cells
                    {
                        switch (nCount) 
                        {
                            case 2:
                                nextUniverse[x, y] = true;
                                break;
                            case 3:
                                nextUniverse[x, y] = true;
                                break;
                            default:
                                nextUniverse[x, y] = false;
                                break;
                        }
                    }
                    else if (universe[x, y] == false) // Rules of dead cells
                    {
                        if (nCount == 3)
                        {
                            nextUniverse[x, y] = true;
                        }
                    }
                }
            }
            bool[,] universeCopy = universe; // Swapping the data so the original universe is changed all at once
            universe = nextUniverse;
            nextUniverse = universeCopy;
            // Count alive cells
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (universe[x, y] == true)
                        cells++;
                }
            }
                    // Increment generation count
                    generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations: " + generations.ToString();
            AliveCells.Text = "Alive: " + cells.ToString();
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = (float)graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = (float)graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // Pens for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);
            Pen gridPenx10 = new Pen(gridColor, 2);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Brush for info text color
            Brush newBrush = new SolidBrush(brush);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = RectangleF.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    RectangleF cellRectx10 = RectangleF.Empty;
                    cellRectx10.X = x * cellWidth; 
                    cellRectx10.Y = y * cellHeight;
                    cellRectx10.Width = cellWidth * 10;
                    cellRectx10.Height = cellHeight * 10;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    if (Grid)
                    {
                        e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                        if (x % 10 == 0 && y % 10 == 0)
                            e.Graphics.DrawRectangle(gridPenx10, cellRectx10.X, cellRectx10.Y, cellRectx10.Width, cellRectx10.Height);
                    }

                    // Draw number of neighbors within each cell
                    Font font = new Font("Arial", 8f);

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    int neighbors;
                    if (NeighborCount)
                    {
                        if (toroidal0Finite1)
                        {
                            neighbors = CountNeighborsFinite(x, y);
                        }
                        else
                        {
                            neighbors = CountNeighborsToroidal(x, y);
                        }

                        if (neighbors > 0)
                        {
                            switch (neighbors) // Draw red or green based off of whether it'll be dead or alive next generation
                            {
                                case 2:
                                    if (universe[x, y] == true)
                                        e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Green, cellRect, stringFormat);
                                    else
                                        e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Red, cellRect, stringFormat);
                                    break;
                                case 3:
                                    e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Green, cellRect, stringFormat);
                                    break;
                                default:
                                    e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Red, cellRect, stringFormat);
                                    break;
                            }
                        }
                    }
                }
            }
            if (HUD)
            {
                string info = $"Generations:{generations}\nAlive Cells: {cells}\nUniverse Size: {universe.GetLength(0)}x{universe.GetLength(1)}\nBoundary Type: ";
                if (toroidal0Finite1)
                    info += "Finite";
                else
                    info += "Toroidal";
                e.Graphics.DrawString(info, graphicsPanel1.Font, newBrush, new PointF(0, graphicsPanel1.ClientSize.Height - 73));
            }

            // Cleaning up pens and brushes
            newBrush.Dispose();
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                float cellWidth = (float)graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                float cellHeight = (float)graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = (int)(e.X / cellWidth);
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = (int)(e.Y / cellHeight);

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Add/Remove cell
                if (universe[x, y] == true)
                    cells++;
                else if (universe[x, y] == false)
                    cells--;
                AliveCells.Text = "Alive: " + cells.ToString();

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private int CountNeighborsFinite(int x, int y) 
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;

                    if (xOffset == 0 && yOffset == 0) // if the offset is 0 for both that means were checking the cell were looking at and theres no need for that
                        continue;
                    else if (xCheck < 0) // this and the yCheck being less than 0 means the universe ends so that means no neighbors to count
                        continue;
                    else if (yCheck < 0)
                        continue;
                    else if (xCheck >= xLen) // if x/yCheck are greater than or equal to x/yLen that means were checking cells out of bounds so no need
                        continue;
                    else if (yCheck >= yLen)
                        continue;
                    else if (universe[xCheck, yCheck] == true) // check if there is a cell here to count as a neighbor
                        count++;
                }
            }
            return count;
        }

        private int CountNeighborsToroidal(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);
            int xCheck;
            int yCheck;
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    xCheck = x + xOffset;
                    yCheck = y + yOffset;
                    if (xOffset == 0 && yOffset == 0) // If checking current cell/no offset
                        continue;
                    if (xCheck < 0) // If x/yCheck are less than the start of the universe then wrap around the universe
                        xCheck = xLen - 1;
                    if (yCheck < 0)
                        yCheck = yLen - 1;
                    if (xCheck >= xLen) // If x/yCheck are greater than the universe length then wrap around the universe
                        xCheck = 0;
                    if (yCheck >= yLen)
                        yCheck = 0;
                    if (universe[xCheck, yCheck] == true) 
                        count++;
                }
            }
            return count;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            NextGeneration();
        }

        
        private void newToolStripMenuItem_Click(object sender, EventArgs e) // Clear universe
        {
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                }
            }
            cells = 0;
            AliveCells.Text = "Alive: " + cells.ToString();
            graphicsPanel1.Invalidate();
        }

        

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e) // Change back color
        {
            ColorDialog dlg = new ColorDialog();

            dlg.Color = graphicsPanel1.BackColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                graphicsPanel1.BackColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
        }

        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e) // Change cell color
        {
            ColorDialog dlg = new ColorDialog();

            dlg.Color = cellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                cellColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
        }

        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e) // Change grid color
        {
            ColorDialog dlg = new ColorDialog();

            dlg.Color = gridColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                gridColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e) // Change interval and universe size
        {
            CustomDialog dlg = new CustomDialog();

            dlg.Interval = timer.Interval;
            dlg.Universe_Width = universe.GetLength(0);
            dlg.Universe_Height = universe.GetLength(1);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                timer.Interval = dlg.Interval;
                Interval.Text = "Interval: " + timer.Interval;
                if (universe.GetLength(0) != dlg.Universe_Width || universe.GetLength(1) != dlg.Universe_Height)
                {
                    universe = new bool[dlg.Universe_Width, dlg.Universe_Height];
                    cells = 0;
                    AliveCells.Text = "Alive: " + cells.ToString();
                }
            }
            graphicsPanel1.Invalidate();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.BackColor = graphicsPanel1.BackColor;
            Properties.Settings.Default.Interval = timer.Interval;
            Properties.Settings.Default.GridColor = gridColor;
            Properties.Settings.Default.CellColor = cellColor;
            Properties.Settings.Default.UniverseWidth = universe.GetLength(0);
            Properties.Settings.Default.UniverseHeight = universe.GetLength(1);
            Properties.Settings.Default.Seed = seed;
            Properties.Settings.Default.IsFinite = toroidal0Finite1;
            Properties.Settings.Default.HUD = HUD;
            Properties.Settings.Default.Grid = Grid;
            Properties.Settings.Default.NeighborCount = NeighborCount;

            Properties.Settings.Default.Save();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();

            graphicsPanel1.BackColor = Properties.Settings.Default.BackColor;
            timer.Interval = Properties.Settings.Default.Interval;
            gridColor = Properties.Settings.Default.GridColor;
            cellColor = Properties.Settings.Default.CellColor;
            universe = new bool[Properties.Settings.Default.UniverseWidth, Properties.Settings.Default.UniverseHeight];
            seed = Properties.Settings.Default.Seed;
            Properties.Settings.Default.IsFinite = toroidal0Finite1;
            HUD = Properties.Settings.Default.HUD;
            Grid = Properties.Settings.Default.Grid;
            NeighborCount = Properties.Settings.Default.NeighborCount;

            CurrentSeed.Text = "Seed: " + seed.ToString();
            Interval.Text = "Interval: " + timer.Interval;
            graphicsPanel1.Invalidate();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();

            graphicsPanel1.BackColor = Properties.Settings.Default.BackColor;
            timer.Interval = Properties.Settings.Default.Interval;
            gridColor = Properties.Settings.Default.GridColor;
            cellColor = Properties.Settings.Default.CellColor;
            universe = new bool[Properties.Settings.Default.UniverseWidth, Properties.Settings.Default.UniverseHeight];
            seed = Properties.Settings.Default.Seed;
            toroidal0Finite1 = Properties.Settings.Default.IsFinite;
            HUD = Properties.Settings.Default.HUD;
            Grid = Properties.Settings.Default.Grid;
            NeighborCount = Properties.Settings.Default.NeighborCount;

            CurrentSeed.Text = "Seed: " + seed.ToString();
            Interval.Text = "Interval: " + timer.Interval;
            graphicsPanel1.Invalidate();
        }
        private void randomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void fromCurrentSeedToolStripMenuItem_Click(object sender, EventArgs e) // Randomize universe from the current seed
        {
            cells = 0;
            Random rnd = new Random(seed);
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (rnd.Next(0, 2) == 0)
                        universe[x, y] = false;
                    else
                        universe[x, y] = true;
                }
            }
            // Count alive cells (after it's changed)
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (universe[x, y] == true)
                        cells++;
                }
            }
            AliveCells.Text = "Alive: " + cells.ToString();
            graphicsPanel1.Invalidate();
        }

        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e) // Randomize universe from specific seed
        {
            SeedDialog dlg = new SeedDialog();

            dlg.Seed = seed;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                seed = dlg.Seed;
                CurrentSeed.Text = "Seed: " + seed.ToString();
                cells = 0;
                Random rnd = new Random(seed);

                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        if (rnd.Next(0, 2) == 0)
                            universe[x, y] = false;
                        else
                            universe[x, y] = true;
                    }
                }
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        if (universe[x, y] == true)
                            cells++;
                    }
                }
                AliveCells.Text = "Alive: " + cells.ToString();
                graphicsPanel1.Invalidate();
            }
        }

        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e) // Randomize universe from default random operator
        {
            cells = 0;
            Random rnd = new Random();
            seed = rnd.Next();
            Random rndSeed = new Random(seed);
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (rndSeed.Next(0, 2) == 0)
                        universe[x, y] = false;
                    else
                        universe[x, y] = true;
                }
            }
            // Count alive cells (after it's changed)
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    if (universe[x, y] == true)
                        cells++;
                }
            }
            AliveCells.Text = "Alive: " + cells.ToString();
            CurrentSeed.Text = "Seed: " + seed.ToString();
            graphicsPanel1.Invalidate();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                cells = 0;
                StreamReader reader = new StreamReader(dlg.FileName);

                // Create a couple variables to calculate the width and height
                // of the data in the file.
                int maxWidth = 0;
                int maxHeight = 0;

                // Iterate through the file once to get its size.
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then it is a comment
                    // and should be ignored.
                    if (row.StartsWith("!"))
                    {
                        continue;
                    }
                    // If the row is not a comment then it is a row of cells.
                    // Increment the maxHeight variable for each row read.
                    maxHeight++;
                    // Get the length of the current row string
                    // and adjust the maxWidth variable if necessary.
                    maxWidth = row.Length;
                }

                // Resize the current universe and scratchPad
                // to the width and height of the file calculated above.
                if (universe.GetLength(0) < maxWidth && universe.GetLength(1) < maxHeight)
                {
                    universe = new bool[maxWidth, maxHeight];
                }
                else if (universe.GetLength(0) < maxWidth)
                {
                    universe = new bool[maxWidth, universe.GetLength(1)];
                }
                else if (universe.GetLength(1) < maxHeight)
                {
                    universe = new bool[universe.GetLength(0), maxHeight];
                }
                // Reset the file pointer back to the beginning of the file.
                reader.BaseStream.Seek(0, SeekOrigin.Begin);

                // Iterate through the file again, this time reading in the cells.
                int yPos = 0;
                while (!reader.EndOfStream)
                {
                    // Read one row at a time.
                    string row = reader.ReadLine();

                    // If the row begins with '!' then
                    // it is a comment and should be ignored.
                    if (row.StartsWith("!"))
                        continue;
                    // If the row is not a comment then 
                    // it is a row of cells and needs to be iterated through.
                    for (int xPos = 0; xPos < row.Length; xPos++)
                    {
                        // If row[xPos] is a 'O' (capital O) then
                        // set the corresponding cell in the universe to alive.
                        if (row[xPos] == 'O')
                        {
                            universe[xPos, yPos] = true;
                        }
                        // If row[xPos] is a '.' (period) then
                        // set the corresponding cell in the universe to dead.
                        else if (row[xPos] == '.')
                        {
                            universe[xPos, yPos] = false;
                        }
                    }
                    yPos++;
                }

                // Update cells
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        if (universe[x, y] == true)
                            cells++;
                    }
                }
                AliveCells.Text = "Alive: " + cells.ToString();
                // Close the file.
                reader.Close();
                graphicsPanel1.Invalidate();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files|*.*|Cells|*.cells";
            dlg.FilterIndex = 2; dlg.DefaultExt = "cells";


            if (DialogResult.OK == dlg.ShowDialog())
            {
                cells = 0;
                StreamWriter writer = new StreamWriter(dlg.FileName);

                // Write any comments you want to include first.
                // Prefix all comment strings with an exclamation point.
                // Use WriteLine to write the strings to the file. 
                // It appends a CRLF for you.
                writer.WriteLine("!" + System.DateTime.Now.ToString());

                // Iterate through the universe one row at a time.
                for (int y = 0; y < universe.GetLength(1); y++)
     {
                    // Create a string to represent the current row.
                    String currentRow = string.Empty;

                    // Iterate through the current row one cell at a time.
                    for (int x = 0; x < universe.GetLength(0); x++)
          {
                        // If the universe[x,y] is alive then append 'O' (capital O)
                        // to the row string.
                        if (universe[x, y] == true)
                            currentRow += "O";
                        // Else if the universe[x,y] is dead then append '.' (period)
                        // to the row string.
                        else
                            currentRow += ".";
                    }

                    // Once the current row has been read through and the 
                    // string constructed then write it to the file using WriteLine.
                    writer.WriteLine(currentRow);
                }

                // After all rows and columns have been written then close the file.
                writer.Close();
            }
        }

        private void toroidalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toroidal0Finite1 = false;
            graphicsPanel1.Invalidate();
        }

        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toroidal0Finite1 = true;
            graphicsPanel1.Invalidate();
        }

        private void hUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HUD = !HUD;
            graphicsPanel1.Invalidate();
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grid = !Grid;
            graphicsPanel1.Invalidate();
        }

        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NeighborCount = !NeighborCount;
            graphicsPanel1.Invalidate();
        }
    }
}
