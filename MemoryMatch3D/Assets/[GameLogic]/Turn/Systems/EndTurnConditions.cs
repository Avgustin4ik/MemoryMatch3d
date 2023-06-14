using System.Collections.Generic;

namespace Turn
{
    public class EndTurnConditions
    {
        public delegate bool ConditionsDelegate();
        public List<ConditionsDelegate> ListOfConditions;
        
        public EndTurnConditions()
        {
            ListOfConditions = new List<ConditionsDelegate>();
        }
    
        public void AddCondition(ConditionsDelegate compareFunc)
        {
            ListOfConditions.Add(compareFunc);
        }
    }
}