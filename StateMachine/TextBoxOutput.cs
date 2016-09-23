using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StateMachines
{
    public class TextBoxOutput : IOutput
    {
        public TextBoxOutput(TextBox outputBox)
        {
            this.OutputBox = outputBox;
        }
        private TextBox OutputBox { get; set; }
        public void OutputLine(string line)
        {
            OutputBox.Text = line + System.Environment.NewLine + OutputBox.Text;
            System.Diagnostics.Debug.WriteLine(line);
        }

        public void OutputObject(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
