using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms.Design;

[ToolboxItem("Placeholder TextBox")]
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
[Browsable(true)]
[Category("Placeholder TextBox")]
[Description("A textbox which allows for placeholder text to be placed in .NET 4.8")]
public class PlaceHolderTextBox : System.Windows.Forms.TextBox
{
    private bool doHide = true;
    System.Drawing.Color DefaultColor;
    public string PlaceHolderText { get; set; }
    public Color PlaceHolderColor { get; set; }
    public PlaceHolderTextBox(string placeholdertext)
    {
        if (!this.DesignMode)
        {
            // get default color of text
            DefaultColor = this.ForeColor;
            // Add event handler for when the control gets focus
            this.GotFocus += (object sender, EventArgs e) =>
            {
                this.Text = String.Empty;
                this.ForeColor = DefaultColor;
            };

            // add event handling when focus is lost
            this.LostFocus += (Object sender, EventArgs e) =>
            {
                if (String.IsNullOrEmpty(this.Text) || this.Text == PlaceHolderText)
                {
                    this.ForeColor = System.Drawing.Color.Gray;
                    this.Text = PlaceHolderText;
                }
                else
                {
                    this.ForeColor = DefaultColor;
                }
            };

            if (!string.IsNullOrEmpty(placeholdertext))
            {
                // change style   
                this.ForeColor = System.Drawing.Color.Gray;
                // Add text
                PlaceHolderText = placeholdertext;
                this.Text = placeholdertext;
            }
        }
        else
        {
            Debugger.Log(2, "PlaceHolderTextBox", "Running in design mode!");
        }
    }

    public void ShowPlaceHolderText()
    {
        if (this.Text == "")
        {
            doHide = true;

        }
        else
        {
            doHide = false;
        }

        if (doHide == true)
        {
            this.Text = PlaceHolderText;
            this.ForeColor = PlaceHolderColor;
        }
    }

    public void HidePlaceholder()
    {
        if (doHide == true)
        {
            this.Text = "";
            this.ForeColor = DefaultColor;
        }
    }
}