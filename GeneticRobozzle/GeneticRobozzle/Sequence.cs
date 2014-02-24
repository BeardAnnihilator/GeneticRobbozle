using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticRobozzle
{
    public enum Action
    {
        EMPTY,
        FORWARD,
        RIGHT,
        LEFT,
        PAINT_R,
        PAINT_G,
        PAINT_B,
        FUNCTION,
        NO_MORE,
        INFINITE_LOOP
    }

    public struct Instruction
    {
        public Color color;
        public Action action;
        public int function;

      // public int used;

        public Instruction(Color color, Action action, int function = -1)
        {
          //  this.used = 0;
            this.color = color;
            this.action = action;
            this.function = function;
        }

        public override string ToString()
        {
            string ret = color.ToString() + action;
            if (action.Equals(Action.FUNCTION))
                ret += function;
            return ret;
        }
    }

    class Sequence
    {
        private List<List<Instruction>> source_code;
        public List<List<Instruction>> Source_code
        {
            get { return source_code; }
        }

        private LinkedList<Instruction> instructions;

        public int nbLoaded
        {
            get { return instructions.Count(); }
        }

        public int nbFunctions
        {
            get { return source_code.Count; }
        }

        public Sequence(int length)
        {
            source_code = new List<List<Instruction>>();
            instructions = new LinkedList<Instruction>();
            addFunction(length);   
        }

        public Sequence(Sequence other, Boolean load = true)
        {
            source_code = new List<List<Instruction>>();
            instructions = new LinkedList<Instruction>();
            foreach (List<Instruction> l in other.source_code)
            {
                addFunction(l.Count);
                int index = 0;
                foreach (Instruction i in l)
                {
                    setInstruction(source_code.Count - 1, index,i);
                    index++;
                }
            }
            if (load)
            foreach (Instruction ins in other.instructions)
            {
                this.instructions.AddLast(ins);
            }
        }

        public void addFunction(int length)
        {
            source_code.Add(new List<Instruction>());
            for (int i = 0; i < length; i++)
            {
                source_code[source_code.Count-1].Add(new Instruction(Color.GRAY, Action.EMPTY));
            } 
        }

        public void remFunction(int index)
        {
            source_code.RemoveAt(index);
        }

        public void setInstruction(int function, int index, Instruction other)
        {
            source_code[function].RemoveAt(index);
            source_code[function].Insert(index, other);
        }

        public void setInstruction(int GlobalIndex, Instruction other)
        {
            int curs = 0;
            int x = 0;
            int y = 0;
            for (x = 0; x < source_code.Count && curs < GlobalIndex; x++)
            {
                for (y= 0; y < source_code[x].Count && curs < GlobalIndex; y++)
                {
                    curs++;
                    if (curs >= GlobalIndex)
                        break;
                }
                if (curs >= GlobalIndex)
                    break;
            }
            setInstruction(x, y, other);
        }

        public void load(int index=0)
        {
            for (int i = source_code[index].Count - 1; i >= 0; i--)
            {
                instructions.AddFirst(source_code[index][i]);
            }
        }

        public Instruction next(Color color)
        {
            try
            {
                Instruction ret = instructions.First();
                instructions.RemoveFirst();
                int countdown = 1000; // against infinite loops
                while (countdown > 0 && ((!ret.color.Equals(Color.GRAY) && !ret.color.Equals(color)) || ret.action.Equals(Action.FUNCTION)))
                {
                    if (((ret.color.Equals(Color.GRAY) || ret.color.Equals(color)) && ret.action.Equals(Action.FUNCTION)))
                    {
                        this.load(ret.function);
                        countdown--;
                    }
                    ret = instructions.First();
                    instructions.RemoveFirst();
                }
                if (countdown <= 0)
                    return new Instruction(Color.GRAY, Action.INFINITE_LOOP);
                return ret;
            }
            catch (Exception e)
            {
                return new Instruction(Color.GRAY, Action.NO_MORE);
            }
            
        }

       public override string ToString()
        {
            string ret = "SOURCE CODE : ";
            foreach (List<Instruction> t in source_code)
            {
                foreach (Instruction i in t)
                {
                    ret += i.ToString()+ " ";
                }
                ret += "\n";
            }
           ret += "\n\nLOADED :";
           foreach (Instruction i in instructions)
           {
               ret += i.ToString() + " ";
           }
           ret += "\n";
            return ret;
        }

       public int getSizeOfFunction(int function)
       {
           if (source_code.Count > function)
               return source_code[function].Count;
           else
               return -1;
       }

       public Sequence reproduction(Sequence other, Random r)
       {
           
           /*int len = getSourceLen();

           int cut = r.Next(len -1) + 1;
           */
           Sequence offspring = splitAndConcat(other, r);//, cut);
           return offspring;
       }

       private Sequence splitAndConcat(Sequence other, Random r)//, int index)
       {
           Sequence result = new Sequence(other, false);
           //int curs = 0;

           for (int x = 0; x < source_code.Count /*&& curs < index*/; x++)
           {
               for (int y = 0; y < source_code[x].Count /*&& curs < index*/; y++) 
               {
                   if (r.NextDouble() < 0.5)
                   {
                       result.setInstruction(x, y, source_code[x][y]);
                   }
                   //curs++;
               }
           }

           return result;
       }

       public Sequence recombine(double swap_proba, Random r)
       {
           Sequence neo = new Sequence(this, false);


           for (int x = 0; x < neo.source_code.Count; x++)
           {
               Instruction first = new Instruction();
               int ystock = -1;

               for (int y = 0; y < neo.source_code[x].Count; y++)
               {
                   double dice = r.NextDouble();
                   if (dice < swap_proba)
                   {
                       if (ystock == -1)
                       {
                           ystock = y;
                           first = neo.source_code[x][y];
                       }
                       else
                       {
                           neo.setInstruction(x, ystock, neo.source_code[x][y]);
                           neo.setInstruction(x, y, first);
                           ystock = -1;   
                       }
                   }
                   
               }
           }

           return neo;
       }

       public int getSourceLen()
       {
           int sum = 0;

           foreach (List<Instruction> t in source_code)
           {
               sum += t.Count();
           }
           return sum;
       }
    }
}
