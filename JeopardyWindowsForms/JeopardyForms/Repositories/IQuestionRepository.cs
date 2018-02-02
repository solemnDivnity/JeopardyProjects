using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeopardyForms.Models;

namespace JeopardyForms.Repositories
{
    interface IQuestionRepository
    {
        IEnumerable<Category> GetCategories();
    }
}
