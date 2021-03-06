﻿using CSharpGameDevFoundation.GameBaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;

namespace CSharpGameDevFoundation
{
    class Window
    {
        private int Height { get; set; } = 35; //height boundary
        private int Width { get; set; } = 75; //width boundary

        private int MinX { get { return -Width / 2; } }
        private int MaxX { get { return Width / 2; } }
        private int MinY { get { return -Height / 2; } }
        private int MaxY { get { return Height / 2; } }

        Dictionary<string, Object> GameBoard { get; set; } = new Dictionary<string, Object>();
        private List<string> RowArray { get; set; } = new List<string>();
        /// <summary>
        /// Set the Height and Width of the window, and the ascii border
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="ascii"></param>
        public Window(int height, int width, string ascii)
        {
            Height = height;
            Width = width;
            InitGameBoard();
            BuildBorder(ascii);
            InitWindow(MinY);
        }

        private void InitGameBoard()
        {
            Console.SetWindowSize(Width + 5, Height + 5);

            for (int j = MinX; j <= MaxX; j++)
            {
                for (int i = MinY; i <= MaxY; i++)
                {
                    GameBoard[$"{ j },{ i }"] = null;
                }
            }
        }

        /// <summary>
        /// builds the border of the window
        /// </summary>
        private void BuildBorder(string ascii = "*")
        {
            for (int i = MinY; i <= MaxY; i++)
            {
                Border leftSide = new Border(0, i, ascii);
                GameBoard[$"{MinX},{i}"] = leftSide;

                Border rightSide = new Border(MaxX, i, ascii);
                GameBoard[$"{ MaxX },{ i }"] = rightSide;
            }


            for (int i = MinX; i <= MaxX; i++)
            {
                Border topRow = new Border(i, MaxY, ascii);
                GameBoard[$"{ i },{ MaxY }"] = topRow;

                Border bottomRow = new Border(i, MinY, ascii);
                GameBoard[$"{ i },{ MinY }"] = bottomRow;
            }
        }
        /// <summary>
        /// Displays the window
        /// </summary>
        public void InitWindow(int minY)
        {
            
            string row = null;
            int nextRow = minY;

            for (int i = MinX; i <= MaxX; i++)
            {
                if (GameBoard[$"{ i },{ nextRow }"] != null)
                {
                    row += GameBoard[$"{ i },{ nextRow }"].Icon;
                }
                else
                {
                    row += " ";
                }
            }

            if (nextRow < MaxY)
            {
                nextRow = nextRow + 1;
                InitWindow(nextRow);
            }
            RowArray.Add(row);
        }

        public void AddObject(Object @object)
        {
            GameBoard[$"{@object.X},{@object.Y}"] = @object;
            RowArray.Clear();
            InitWindow(MinY);
        }

        public void PrintWindow(int count = -1)
        {
            int countInit = count + 1;
            string gameWindow = "";


            if(countInit < RowArray.Count)
            {
                gameWindow += RowArray[countInit];
                PrintWindow(countInit);
                
            }
            Console.WriteLine(gameWindow);
        }


        public void Refresh()
        {
            while(0==0)
            {
                Thread.Sleep(500);
                Console.Clear();
                PrintWindow();
            }
        }
    }
}