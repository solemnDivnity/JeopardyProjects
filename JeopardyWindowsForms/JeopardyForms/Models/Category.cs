using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeopardyForms.Models
{
    public class Category
    {
        public string CategoryName;
        public List<Question> Questions = new List<Question>();
    }
}
