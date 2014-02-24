using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRobozzle
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * DEBUT DESCRIPTION PUZZLE
             */ 
            Puzzle zzle = new Puzzle(9, 9);
            zzle.insert(0, 4, Color.GREEN);
            zzle.insert(4, 0, Color.GREEN);
            zzle.insert(4, 8, Color.GREEN);
            zzle.insert(8, 4, Color.GREEN);
            zzle.putStar(0, 4);
            zzle.putStar(4, 0);
            zzle.putStar(4, 8);
            zzle.putStar(8, 4);

            zzle.insert(1, 4, Color.BLUE);
            zzle.insert(2, 4, Color.BLUE);
            zzle.insert(3, 4, Color.BLUE);
            zzle.insert(4, 4, Color.BLUE);
            zzle.insert(5, 4, Color.BLUE);
            zzle.insert(6, 4, Color.BLUE);
            zzle.insert(7, 4, Color.BLUE);
            zzle.putStar(1, 4);
            zzle.putStar(2, 4);
            zzle.putStar(3, 4);
            zzle.move(4, 4);
            zzle.putStar(5, 4);
            zzle.putStar(6, 4);
            zzle.putStar(7, 4);

            zzle.insert(4, 1, Color.BLUE);
            zzle.insert(4, 2, Color.BLUE);
            zzle.insert(4, 3, Color.BLUE);
            zzle.insert(4, 5, Color.BLUE);
            zzle.insert(4, 6, Color.BLUE);
            zzle.insert(4, 7, Color.BLUE);
            zzle.putStar(4, 1);
            zzle.putStar(4, 2);
            zzle.putStar(4, 3);
            zzle.putStar(4, 5);
            zzle.putStar(4, 6);
            zzle.putStar(4, 7);

            zzle.countdownActive = true;
            zzle.countdown = 200;
            /*Puzzle zzle = new Puzzle(13,17);
            zzle.insert(0, 0, Color.BLUE);
            zzle.move(0, 0);
            zzle.insert(0, 1, Color.RED);
            zzle.putStar(0, 1);
            zzle.insert(1, 1, Color.GREEN);
            zzle.putStar(1, 1);
            zzle.insert(1, 2, Color.RED);
            zzle.putStar(1, 2);
            zzle.insert(2, 2, Color.GREEN);
            zzle.putStar(2, 2);
            zzle.insert(2, 3, Color.RED);
            zzle.putStar(2, 3);
            zzle.insert(3, 3, Color.GREEN);
            zzle.putStar(3, 3);
            zzle.insert(3, 4, Color.RED);
            zzle.putStar(3, 4);
            zzle.insert(4, 4, Color.GREEN);
            zzle.putStar(4, 4);
            zzle.insert(4, 5, Color.RED);
            zzle.putStar(4, 5);
            zzle.insert(5, 5, Color.GREEN);
            zzle.putStar(5, 5);
            zzle.insert(5, 6, Color.RED);
            zzle.putStar(5, 6);
            zzle.insert(6, 6, Color.GREEN);
            zzle.putStar(6, 6);
            zzle.insert(6, 7, Color.BLUE);
            zzle.putStar(6, 7);
            zzle.insert(6, 8, Color.BLUE);
            zzle.putStar(6, 8);
            zzle.insert(6, 9, Color.BLUE);
            zzle.putStar(6, 9);
            zzle.insert(6, 10, Color.RED);
            zzle.putStar(6, 10);
            zzle.insert(7, 10, Color.GREEN);
            zzle.putStar(7, 10); 
            zzle.insert(7, 11, Color.RED);
            zzle.putStar(7, 11);
            zzle.insert(8, 11, Color.GREEN);
            zzle.putStar(8, 11);
            zzle.insert(8, 12, Color.RED);
            zzle.putStar(8, 12);
            zzle.insert(9, 12, Color.GREEN);
            zzle.putStar(9, 12);
            zzle.insert(9, 13, Color.RED);
            zzle.putStar(9, 13);
            zzle.insert(10, 13, Color.GREEN);
            zzle.putStar(10, 13);
            zzle.insert(10, 14, Color.RED);
            zzle.putStar(10, 14);
            zzle.insert(11, 14, Color.GREEN);
            zzle.putStar(11, 14);
            zzle.insert(11, 15, Color.RED);
            zzle.putStar(11, 15);
            zzle.insert(12, 15, Color.GREEN);
            zzle.putStar(12, 15);
            zzle.insert(12, 16, Color.BLUE);
            zzle.putStar(12, 16);

            zzle.countdownActive = true;
            zzle.countdown = 200;*/
            /*
             * FIN DESCRIPTION PUZZLE
             */

            Console.WriteLine(zzle.ToString());
           // Console.ReadLine();
            Robo Joe = new Robo(zzle);
            //Joe.right();
            
            /*
             * DEBUT DESCRIPTION SEQUENCE
             */

            //Sequence seq = new Sequence(4);
            
            Sequence seq = new Sequence(2);
            seq.addFunction(5);
            /*seq.setInstruction(0,0,new Instruction(Color.GRAY,Action.PAINT_R));
            seq.setInstruction(0,1, new Instruction(Color.GRAY, Action.FUNCTION, 1));

            seq.setInstruction(1, 0, new Instruction(Color.GRAY, Action.FORWARD));
            seq.setInstruction(1, 1, new Instruction(Color.GREEN, Action.RIGHT));
            seq.setInstruction(1, 2, new Instruction(Color.GREEN, Action.RIGHT));
            seq.setInstruction(1, 3, new Instruction(Color.RED, Action.LEFT));
            seq.setInstruction(1, 4, new Instruction(Color.GRAY, Action.FUNCTION, 1));
            */
            /*
             * FIN DESCRIPTION SEQUENCE
             */ 

           // seq.load();
            //Console.WriteLine(seq.ToString());


            //Joe.setSequence(seq);


            //Joe.run();

            Population pop = new Population(seq, 100000, 0.8, 0.02);

            for (int i = 0; i < 20; i++)
            {
                pop.nextGen();
                pop.run(Joe);
                //if (i % 20 == 0)
                //{
                    Console.WriteLine("BEST GUY GEN : " + pop.generation + " " + pop.bestGuy());
                   // Console.ReadLine();
              //  }

                    if (pop.bestGuy().RReturn.statut.Equals("SUCCESS."))
                        break;
            }
            Console.WriteLine("BEST GUY GEN : " + pop.generation + " " + pop.bestGuy());
            Console.ReadLine();

        }


        public static int fitness(Individual indi)
        {
            RunReturn ret = indi.RReturn;

            int value = 1;
            if (ret.statut.Equals("SUCCESS."))
                value += 1000;
            else if (ret.statut.Equals("TIME OUT BABY"))
                value += 15;
            else if (ret.statut.Equals("Out of colored cells."))
                value += 5;
            else if (ret.statut.Equals("NO MORE INSTRUCTIONS."))
                value += 7;
            else
            if (ret.statut.Equals("INFINITE_LOOP."))
                value = 0;

            ///if (ret.nbMov < ((ret.maxStars - ret.nbStars) * 3))
            //{
                value += ((ret.maxStars - ret.nbStars) * 10);
                if (ret.nbMov * 2 <= ((ret.maxStars - ret.nbStars)))
                    value += (ret.nbMov * 1);
                else
                    value += ((ret.maxStars - ret.nbStars) * 2);
            //}
            //else
            //{
              //  value += ((ret.maxStars - ret.nbStars) * (5+2));
            //}

                if (indi.sequence.nbLoaded > 10)
                {
                    value -= 5;// indi.sequence.nbLoaded;
                }

                if (indi.RReturn.stuck > 0)
                {
                    value -= 5;// indi.sequence.nbLoaded;
                }

                foreach (List<Instruction> l in indi.sequence.Source_code)
                {
                    if (l.Contains(new Instruction(Color.RED, Action.PAINT_R)))
                    {
                        value -= 5;
                    }
                    if (l.Contains(new Instruction(Color.GREEN, Action.PAINT_G)))
                    {
                        value -= 5;
                    }
                    if (l.Contains(new Instruction(Color.BLUE, Action.PAINT_B)))
                    {
                        value -= 5;
                    }
                }

                if (value < 0)
                    value = 0;

            return value;
        }
    }
}
