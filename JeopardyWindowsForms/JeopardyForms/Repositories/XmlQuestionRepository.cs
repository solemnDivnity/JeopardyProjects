using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using JeopardyForms.Models;

namespace JeopardyForms.Repositories
{
    public class XmlQuestionRepository : IQuestionRepository
    {
        public IEnumerable<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            XmlDocument document = new XmlDocument();
            document.Load("C:\\Users\\SolemnDivinity\\source\\repos\\JeopardyForms\\JeopardyForms\\Questions.xml");

            foreach(XmlNode node in document.GetElementsByTagName("Category"))
            {
                Category category = new Category();
                category.CategoryName = node.Attributes["name"].Value;
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    Question question = new Question();
                    if (!int.TryParse(childNode.Attributes.GetNamedItem("value").Value.ToString(), out question.points))
                    {
                        throw new Exception("Xml Improperly formated missing points Attribute or it's not a number for Node:" + childNode.InnerXml);
                    }
                    question.question = childNode.SelectNodes("question")[0].InnerText;
                    question.possibleAnswers = getPossibleAnswers(childNode);
                    category.Questions.Add(question);
                }

                categories.Add(category);
            }

            return categories;
        }

        private List<string> getPossibleAnswers(XmlNode tileNode)
        {
            List<string> possibleAnswers = new List<string>();

            var answers = tileNode.SelectNodes("possibleAnswers/answer");
            foreach(XmlNode node in answers)
            {
                possibleAnswers.Add(node.InnerText);
            }
            return possibleAnswers;
        }
    }
}
