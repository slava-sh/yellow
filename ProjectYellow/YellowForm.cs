﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectYellow
{
    public partial class YellowForm : Form
    {
        private const int FieldWidth = 10;
        private const int FieldHeight = 20;
        private const int CellSize = 25;
        private static readonly Color EmptyCellColor = Color.Yellow;
        private static readonly Color OccupiedCellColor = Color.Black;

        private readonly Button[,] buttons;

        private Game game;
        private Timer ticker;

        public YellowForm()
        {
            InitializeComponent();
            buttons = new Button[FieldWidth, FieldHeight];
            for (var x = 0; x < FieldWidth; ++x)
            {
                for (var y = 0; y < FieldHeight; ++y)
                {
                    buttons[x, y] = new Button
                    {
                        Size = new Size(CellSize, CellSize),
                        Location = new Point(x * CellSize, y * CellSize),
                        Enabled = false
                    };
                    Controls.Add(buttons[x, y]);
                }
            }
        }

        private void YellowForm_Load(object sender, EventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            game = new Game(FieldWidth, FieldHeight, 2017);
            HandleTick(null, null);
            ticker = new Timer();
            ticker.Tick += HandleTick;
            ticker.Interval = 300;
            ticker.Start();
        }

        private void HandleTick(object sender, EventArgs e)
        {
            game.Tick();
            Render();
            if (game.IsOver)
            {
                HandleGameOver();
            }
        }

        private void HandleGameOver()
        {
            ticker.Stop();
            // TODO: Add funny icon.
            var result = MessageBox.Show("Game over. Try again?", "Game Over", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                Application.Exit();
            }
            NewGame();
        }

        private void Render()
        {
            var mask = game.GetFieldMask();
            for (var x = 0; x < mask.GetLength(0); ++x)
            {
                for (var y = 0; y < mask.GetLength(1); ++y)
                {
                    buttons[x, y].BackColor = mask[x, y] ? OccupiedCellColor : EmptyCellColor;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    game.Rotate();
                    Render();
                    return true;
                case Keys.Left:
                    game.ShiftLeft();
                    Render();
                    return true;
                case Keys.Right:
                    game.ShiftRight();
                    Render();
                    return true;
                case Keys.Down:
                    game.SoftDrop();
                    Render();
                    return true;
                case Keys.Space:
                    game.HardDrop();
                    Render();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}