using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRobozzle
{
    class Population
    {
        private Selection select;
        private uint POPSIZE;
        private double CROSSRATE;
        private double MUTRATE;

        private Sequence sample;

        private uint _generation;
        public uint generation
        {
            get { return _generation; }
        }

        private List<Individual> _people;
        public List<Individual> people
        {
            get { return _people; }
        }

        public Population(Sequence sample, uint popsize, double crossrate, double mutrate)
        {
            this.sample = sample;
            this.POPSIZE = popsize;
            this.CROSSRATE = crossrate;
            this.MUTRATE = mutrate;
            this._generation = 0;
            this._people = new List<Individual>();
            this.select = new Selection(POPSIZE, CROSSRATE, MUTRATE);
        }

        private void generate()
        {
            Random r = new Random();

            for (uint i = 0; i < POPSIZE; i++)
            {
               /* Sequence neo = new Sequence(sample, false);
                
                int function = 0;
                int size = neo.getSizeOfFunction(function);

                while (size != -1)
                {
                    for (int u = 0; u < size; u++)
                    {
                        neo.setInstruction(function, u, randomInstruction(r, neo.nbFunctions));
                    }
                    function++;
                    size = neo.getSizeOfFunction(function);
                }*/
                _people.Add(RandomIndividual(r, sample));
            }
        }

        public static Individual RandomIndividual(Random r, Sequence sample)
        {
            Sequence neo = new Sequence(sample, false);

            int function = 0;
            int size = neo.getSizeOfFunction(function);

            while (size != -1)
            {
                for (int u = 0; u < size; u++)
                {
                    neo.setInstruction(function, u, randomInstruction(r, neo.nbFunctions));
                }
                function++;
                size = neo.getSizeOfFunction(function);
            }
            return new Individual(neo);
        }

        public static Instruction randomInstruction(Random r, int nbFunction)
        {
            Color color = (Color)(r.Next(3 +1) + 1);
                        Action action= (Action)(r.Next(7 +1));
                        int f = -1;
                        if (action.Equals(Action.FUNCTION))
                            f = r.Next(nbFunction);
                        return new Instruction(color, action, f);
        }

        public uint nextGen()
        {
            if (_people.Count.Equals(0))
                generate();
            else if (_people.First().fitnessValue >= 0)
            {

                List<Individual> tmp = select.reproduction(_people);
                _people.Clear();
                foreach (Individual i in tmp)
                {
                    _people.Add(i);
                }
                _generation++;
            }
            else
            {
                throw new Exception("Current population need to be ran for further exploitation");
            }
            return _generation;
        }

        public Individual bestGuy()
        {
            _people.Sort();
            _people.Reverse();
            return _people.First();
        }

        public void run(Robo Robot)
        {
            foreach (Individual i in _people)
            {
                Robo copy = new Robo(Robot);
                i.sequence.load();
                copy.setSequence(i.sequence);
                i.RReturn = copy.run();
                i.fitnessValue = Program.fitness(i);
            }
            _people.Sort();
        }
    }
}
