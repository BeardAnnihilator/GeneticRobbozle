using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRobozzle
{
    public enum Color
    {
        BLACK,
        GRAY,
        BLUE,
        GREEN,
        RED
    }

    struct Case
    {
        public Color color;
        public Boolean thereIsAStar;
        public Boolean thereIsARobo;

        public Case(Color color, Boolean star, Boolean robo)
        {
            this.color = color;
            this.thereIsAStar = star;
            this.thereIsARobo = robo;
        }

        public override string ToString()
        {
            string ret = "";
            if (color.Equals(Color.BLACK)) ret = " ";
            if (color.Equals(Color.GRAY)) ret = "?";
            if (color.Equals(Color.BLUE)) ret = "b";
            if (color.Equals(Color.GREEN)) ret = "g";
            if (color.Equals(Color.RED)) ret = "r";
            if (thereIsAStar) ret = ret.ToUpper();
            if (thereIsARobo) ret = "0";
            return ret;
        }
    }
    
    class Puzzle
    {
        private Case[][] map;
        private int _RoboX;
        public int RoboX
        {
            get { return _RoboX; }
        }
        private int _RoboY;
        public int RoboY
        {
            get { return _RoboY; }
        }
        private uint _maxStar;
        public uint maxStar
        {
            get { return _maxStar; }
        }
        private uint _nbStar;
        public uint nbStar
        {
            get { return _nbStar; }
        }
        private static void fillLineWith(Case[] line, Case sample)
        {
            for (int i = 0; i < line.Length; i++)
            {
                line[i] = new Case(sample.color, sample.thereIsAStar, sample.thereIsARobo);
            }
        }

        public Boolean countdownActive;

        private int _countdown;
        public int countdown
        {
            set { if (countdownActive) { _countdown = value; if (_countdown <= 0)throw new Exception("TIME OUT BABY"); } }
            get { return _countdown; }
        }

        private static Boolean isValidCoordinate(int x, int y, Case[][] map)
        {
            if (x >= 0 && x < map.Length)
                if (y >= 0 && y < map[0].Length)
                    return true;
            return false;
        }

        public Puzzle(int heigth, int length)
        {
            _RoboX = -1;
            _RoboY = -1;
            _maxStar = 0;
            _nbStar = 0;
            countdownActive = false;
            countdown = 0;
            this.resize(heigth, length);
        }

        public Puzzle(Puzzle other)
        {
            this.countdownActive = other.countdownActive;
            this.countdown = other.countdown;
            this.resize(other.map.Length,other.map[0].Length);
            for (int i = 0; i < other.map.Length; i++)
            {
                for (int j = 0; j < other.map[0].Length; j++)
                {
                    insert(i, j, other.map[i][j].color);
                    if (other.map[i][j].thereIsARobo)
                    {
                        move(i, j);
                    }
                    if (other.map[i][j].thereIsAStar)
                    {
                        putStar(i, j);
                    }
                }
            }
        }

        public Boolean resize(int heigth, int length)
        {
            Case sample = new Case(Color.BLACK, false, false);

            try
            {
                this.map = new Case[heigth][];
                for (int i = 0; i < heigth; i++)
                {
                    this.map[i] = new Case[length];
                    fillLineWith(this.map[i], sample);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Boolean insert(int heigth, int length, Color color)
        {
            try
            {
                this.map[heigth][length].color = color;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Boolean move(int x, int y)
        {
            countdown--;
            try
            {
                if (isValidCoordinate(RoboX, RoboY, this.map))
                    this.map[RoboX][RoboY].thereIsARobo = false;
                this.map[x][y].thereIsARobo = true;
                if (this.map[x][y].thereIsAStar)
                {
                    this.map[x][y].thereIsAStar = false;
                    _nbStar--;
                }
                _RoboX = x;
                _RoboY = y;
            }
            catch (Exception e)
            {
                throw new Exception("Out of colored cells.");
            }
            if (this.map[x][y].color == Color.BLACK)
                throw new Exception("Out of colored cells.");
            return true;
        }

        public Boolean putStar(int x, int y)
        {
            if (this.map[x][y].thereIsARobo)
                return false;
            this.map[x][y].thereIsAStar = true;
            this._maxStar++;
            this._nbStar++;
            return true;
        }

        public Boolean removeStar(int x, int y)
        {
            if (!this.map[x][y].thereIsAStar)
                return false;
            this.map[x][y].thereIsAStar = false;
            this._maxStar--;
            this._nbStar--;
            return true;
        }

        public override string ToString()
        {
            string ret = "";
            for (int j = 0; j < this.map[0].Length + 2; j++)
                ret += "-";
            ret += "\n";
            for (int i = 0; i < this.map.Length; i++)
            {
                ret += "|";
                for (int j = 0; j < this.map[0].Length; j++)
                {
                    ret += this.map[i][j].ToString();
                }
                ret += "|\n";
            }
            for (int j = 0; j < this.map[0].Length + 2; j++)
                ret += "-";
           return ret;
        }

        public Color getColor(int x, int y)
        {
            return(map[x][y].color);
        }
    }
}
