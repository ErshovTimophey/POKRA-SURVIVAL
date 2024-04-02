using System;
using System.Collections.Generic;

namespace Quiz

{
    [Serializable]
    public class FinalQuestion
    {
        public string question;
        public List<string> answers = new(4);

        public FinalQuestion(string question, List<string> answers)
        {
            this.question = question;
            this.answers = answers;
        }
    }
}
