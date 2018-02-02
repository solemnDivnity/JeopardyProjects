using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JeopardyForms.Models;
using JeopardyForms.Repositories;

namespace JeopardyForms
{
    public partial class Form1 : Form
    {
        IQuestionRepository repository = new XmlQuestionRepository();
        List<Category> Categories = new List<Category>();
        List<Button> Buttons = new List<Button>();
        Size buttonSize = new Size(90, 45);
        Size columnPadding = new Size(6,6);
        Point startingPos = new Point(12, 12);

        public Form1()
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            Categories = repository.GetCategories().ToList();
            int categoryNum = 0;
            int buttonNum = 0;
            int highestQuestionCount = 0;

            foreach(Category category in Categories)
            {
                //for calculating the position of the entry form for the questions
                if(category.Questions.Count > highestQuestionCount)
                {
                    highestQuestionCount = category.Questions.Count;
                }

                //Add a category Label
                AddCategoryLabel(category.CategoryName, new Point(startingPos.X + categoryNum * buttonSize.Width + columnPadding.Width, startingPos.Y));
                //Add the categories buttons for each question
                foreach (Question question in category.Questions)
                {
                    var buttonPos = new Point(startingPos.X + categoryNum * buttonSize.Width + columnPadding.Width, startingPos.Y + buttonSize.Height / 2 + buttonNum * buttonSize.Height + columnPadding.Height);
                    var button = CreateButton(category.CategoryName+question.points, question.points.ToString(),buttonPos);
                    Buttons.Add(button);
                    buttonNum++;
                }

                categoryNum++;
                buttonNum = 0;
            }
            placeAnswerForms(highestQuestionCount);
        }

        private void AddCategoryLabel(string categoryName, Point location)
        {
            Label label = new Label();
            label.Text = categoryName;
            label.Location = location;
            label.Size = new Size(buttonSize.Width, buttonSize.Height / 2);
            this.Controls.Add(label);
        }

        private Button CreateButton(string categoryName, string questionPoints,Point location )
        {
            Button button = new Button();
            button.Name = categoryName + questionPoints;
            button.Text = questionPoints;
            button.Size = buttonSize;
            button.Location = location;
            this.Controls.Add(button);

            return button;
        }

        private void placeAnswerForms(int highestQuestionCount)
        {
            TextBox answerBox = new TextBox();
            var answerBoxPlacement = new Point(startingPos.X, startingPos.Y + ((highestQuestionCount + 1) * buttonSize.Height) + columnPadding.Height * (highestQuestionCount + 1));
            answerBox.Location = answerBoxPlacement;
            answerBox.Size = new Size((Categories.Count - 1) * buttonSize.Width + (Categories.Count - 1) * columnPadding.Width, buttonSize.Height / 2);
            this.Controls.Add(answerBox);

            Button submit = new Button();
            var submitPlacement = new Point(startingPos.X + (Categories.Count-1) * buttonSize.Width + (Categories.Count-1) * columnPadding.Width, startingPos.Y + ((highestQuestionCount + 1) * buttonSize.Height) + columnPadding.Height * (highestQuestionCount + 1));
            submit.Location = submitPlacement;
            submit.Size = new Size(buttonSize.Width, buttonSize.Height / 2);
            submit.Text = "Submit";
            this.Controls.Add(submit);

        }
    }
}
