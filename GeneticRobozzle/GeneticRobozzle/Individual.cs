using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRobozzle
{
    class Individual : IComparable
    {
        private double SWAP_PROBA = 0.33;

        private Sequence seq;
        public Sequence sequence
        {
            get { return seq; }
        }
        private RunReturn ret;
        public RunReturn RReturn
        {
            get { return ret; }
            set { ret = value; }
        }
        private int _fitnessValue;
        public int fitnessValue
        {
            get { return _fitnessValue; }
            set { _fitnessValue = value;}
        }

        public Individual(Sequence seq)
        {
            this.seq = seq;
            this._fitnessValue = -1;
        }

        public int CompareTo(object obj)
        {
            Individual other = (Individual)(obj);
            return fitnessValue.CompareTo(other.fitnessValue);
        }

        public Individual reproduction(Individual other, Random r)
        {
            return new Individual(this.sequence.reproduction(other.sequence, r));
        }

        public Individual recombinaison(Random r)
        {
            return new Individual(this.sequence.recombine(SWAP_PROBA, r));
        }

        public override string ToString()
        {
            return seq.ToString() + "Fitness : " + fitnessValue
                + "\n"+RReturn;
        }
    }
}
