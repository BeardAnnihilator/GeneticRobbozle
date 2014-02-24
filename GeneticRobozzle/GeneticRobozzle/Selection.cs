using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRobozzle
{
    class Selection
    {
        private uint POPSIZE;
        private double CROSSRATE;
        private double MUTRATE;

        public Selection(uint popsize, double crossrate, double mutrate)
        {
            this.POPSIZE = popsize;
            this.CROSSRATE = crossrate;
            this.MUTRATE = mutrate;
        }

        public List<Individual> reproduction(List<Individual> Gen)
        {
            return ClockSelection(Gen);
        }

        private List<Individual> crossOrRecombi(List<Individual> matingPool, Random r)
        {
            int Sfitness = 0;
            List<Individual> neo = new List<Individual>();
            for (int i = 0; i < (POPSIZE - (matingPool.Count * 2)); i++)
            {
                Individual offspring;
                Individual lucky = peekAGuy(matingPool, Sfitness, r);
                
                if (r.NextDouble() < CROSSRATE)
                {
                    Individual other = peekAGuy(matingPool, Sfitness, r);
                    offspring = lucky.reproduction(other, r);
                }
                else
                {
                    offspring = lucky.recombinaison(r);
                }
                neo.Add(offspring);
            }
            return neo;
        }

        private void mutation(List<Individual> Gen, Random r)
        {
            foreach (Individual Indi in Gen)
            {
                for (int i = 0; i < Indi.sequence.getSourceLen(); i++)
                {
                    if (r.NextDouble() > MUTRATE)
                    {
                        Indi.sequence.setInstruction(i, Population.randomInstruction(r, Indi.sequence.nbFunctions));
                    }
                }
            }
        }

        private List<Individual> ClockSelection(List<Individual> Gen)
        {
            Random r = new Random();
            
            int Sfitness = 0;
            
            List<Individual> matingPool = GenerateMatingPool(Gen, ref Sfitness);


            List<Individual> neo = crossOrRecombi(matingPool, r);

            mutation(neo, r);

            // REPROC

            // MUTATION

            /*for (int i = 0; i < (POPSIZE - matingPool.Count); i++)
            {
                Individual offspring;
                Individual lucky = peekAGuy(matingPool, Sfitness, r);
                if (crossover(r))
                {
                    Individual other = peekAGuy(matingPool, Sfitness, r);
                    offspring = lucky.reproduction(other, r);
                }
                else
                {
                    int len = lucky.sequence.getSourceLen();
                    int cut = r.Next(len);
                    Sequence offseq = new Sequence(lucky.sequence, false);
                    offseq.setInstruction(cut, Population.randomInstruction(r, offseq.nbFunctions));
                    offspring = new Individual(offseq);
                }
                neo.Add(offspring);
            }
            */
            foreach (Individual i in matingPool)
            {
                i.fitnessValue = -1;
                neo.Add(i);
                neo.Add(Population.RandomIndividual(r, i.sequence));
            }
            


            return neo;
        }

        private Individual peekAGuy(List<Individual> matingPool, int Sfitness, Random r)
        {
            int peek = r.Next(Sfitness);
            int index = 0;
            int cursor = matingPool[index].fitnessValue;
            while (cursor < peek)
            {
                index++;
                cursor += matingPool[index].fitnessValue;
            }
            return matingPool[index];
        }

        private List<Individual> GenerateMatingPool(List<Individual> Gen, ref int Sfitness)
        {
            List<Individual> matingPool = new List<Individual>();
            int matingPoolSize = (int)POPSIZE / 10;
            if (matingPoolSize < 5)
                matingPoolSize = 5;
            Gen.Sort();
            Gen.Reverse();

            for (int i = 0; i < matingPoolSize; i++)
            {
                matingPool.Add(Gen[i]);
                Sfitness += Gen[i].fitnessValue;
            }
            return matingPool;
        }
    }
}
