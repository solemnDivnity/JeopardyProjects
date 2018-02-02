using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeopardyForms.Models
{
    public class Question
    {
        public int points;
        public string question;
        public List<string> possibleAnswers = new List<string>();
    }
}
