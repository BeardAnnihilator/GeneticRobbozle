using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRobozzle
{
    public enum Orientation
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    public struct RunReturn
    {
        public string statut;
        public int nbStars;
        public int maxStars;
        public int nbMov;
        public int stuck;

        public RunReturn(string statut, int nbStars, int maxStars, int nbMov, int stuck)
        {
            this.statut = statut;
            this.nbStars = nbStars;
            this.maxStars = maxStars;
            this.nbMov = nbMov;
            this.stuck = stuck;
        }

        public override string ToString()
        {
            return "Statut : " + statut
          + "\n Star :" + nbStars + "/" + maxStars
          + "\n nb Mov : " + nbMov;
        }
 
    }

    class Robo
    {
        private Orientation orientation;
        private Puzzle zzle;
        private Sequence seq;
        private int X;
        private int Y;

        public Robo(Puzzle zzle, Orientation orientation = Orientation.UP, Sequence seq = null)
        {
            this.orientation = orientation;
            this.zzle = zzle;
            this.seq = seq;
            this.X = zzle.RoboX;
            this.Y = zzle.RoboY;
        }

        public Robo(Robo other)
        {
            this.orientation = other.orientation;
            this.zzle = new Puzzle(other.zzle);
            if (other.seq != null)
            this.seq = new Sequence(other.seq);
            this.X = other.X;
            this.Y = other.Y;

        }

        public void setSequence(Sequence seq)
        {
            this.seq = seq;
        }

        public Boolean move(int x, int y)
        {
            this.X = x;
            this.Y = y;
            return zzle.move(this.X, this.Y); 
        }

        public Boolean forward()
        {
            switch ((int)this.orientation)
            {
                case 0:
                    this.X--;
                    break;
                case 1:
                    this.Y++;
                    break;
                case 2:
                    this.X++;
                    break;
                case 3:
                    this.Y--;
                    break;
            }
            return zzle.move(this.X, this.Y);
        }

        public void right()
        {
            int dir = (int)this.orientation;
            dir = (dir + 1) % 4;
            this.orientation = (Orientation)dir;
        }
        
        public void left()
        {
            int dir = (int)this.orientation;
            dir = (dir + 3) % 4;
            this.orientation = (Orientation)dir;
        }

        public Boolean paint(Color color)
        {
            return this.zzle.insert(this.X, this.Y, color);
        }

        public RunReturn run()
        {
            string message = "";
            int nbMov = 0;
            if (this.seq == null)
                message = "NO MORE INSTRUCTIONS.";


            int stuck = 0;
            int currentstuck = 0;
            Instruction ins = seq.next(zzle.getColor(this.X, this.Y));
            try
            {
                while (zzle.nbStar > 0 && ins.action != Action.NO_MORE && ins.action != Action.INFINITE_LOOP)
                {

                    switch ((int)ins.action)
                    {
                        case 1:
                            forward();
                            nbMov++;
                            if (currentstuck > 3)
                                stuck += currentstuck;
                            currentstuck = 0;
                            break;
                        case 2:
                            right();
                            currentstuck++;
                            break;
                        case 3:
                            left();
                            currentstuck++;
                            break;
                        case 4:
                            paint(Color.RED);
                            currentstuck++;
                            break;
                        case 5:
                            paint(Color.GREEN);
                            currentstuck++;
                            break;
                        case 6:
                            paint(Color.BLUE);
                            currentstuck++;
                            break;
                    }
                    zzle.countdown--;
                    //Console.WriteLine(ins);
                   // Console.WriteLine(zzle.ToString());
                   // Console.ReadLine();

                    ins = seq.next(zzle.getColor(this.X, this.Y));
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                if (e.Message.Equals("Out of colored cells.") || e.Message.Equals("TIME OUT BABY"))
                {
                    //Console.WriteLine(e.Message);
                    message = e.Message;
                }
            }

            if (message.Equals(""))
            {
                if (zzle.nbStar == 0)
                    message = "SUCCESS.";
                else if (ins.action == Action.NO_MORE)
                    message = "NO MORE INSTRUCTIONS.";
                else if (ins.action == Action.INFINITE_LOOP)
                    message = "INFINITE_LOOP.";
            }

            //Console.WriteLine(message);
            return new RunReturn(message, (int)zzle.nbStar, (int)zzle.maxStar, nbMov, stuck);
        }
    }
}
