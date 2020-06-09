using System;
using System.Windows.Forms;
using System.Drawing;

public class GorilaGame : Form
{
    private Label vxLabel;
    private Label vzLabel;

    private TextBox vxTextBox;
    private TextBox vzTextBox;

    private Button fireButton;
    private Button resetButton;

    private Panel drawingPanel;

    //  This field is for the images used in the game.
    private Image playerIcon;

    //  The Timer is used to control the execution speed
    //  of the game.
    private Timer gameTimer;

    // The physics of the GorilaGame!
    private bagBall bBall;

    public GorilaGame()
    {
        //  Initialize the GorilaGame parameters.
        bBall = new bagBall(0.5, 1.7, 0.0, 0.0, 0.0);

        //  Set up images
        playerIcon = Image.FromFile("gorila.png");

        //  Create a Timer object that will be used
        //  to slow the action down.
        gameTimer = new Timer();
        gameTimer.Interval = 50;  //  delay in milliseconds.
        gameTimer.Tick += new EventHandler(ActionPerformed);

        //  Create some Labels
        vxLabel = new Label();
        vxLabel.Text = "Initial horizontal velocity, m/s";
        vxLabel.Font = new Font(vxLabel.Font, FontStyle.Bold);
        vxLabel.Top = 30;
        vxLabel.Left = 10;
        vxLabel.Width = 180;

        vzLabel = new Label();
        vzLabel.Text = "Initial vertical velocity, m/s";
        vzLabel.Font = new Font(vzLabel.Font, FontStyle.Bold);
        vzLabel.Top = 60;
        vzLabel.Left = 10;
        vzLabel.Width = 180;

        //  Create TextBox objects to display the outcome.
        vxTextBox = new TextBox();
        vxTextBox.Width = 50;
        vxTextBox.Text = String.Format("{0}", 2.5m);
        vxTextBox.AutoSize = true;
        vxTextBox.Top = vxLabel.Top;
        vxTextBox.Left = 200;

        vzTextBox = new TextBox();
        vzTextBox.Width = 50;
        vzTextBox.Text = String.Format("{0}", 2.0m);
        vzTextBox.AutoSize = true;
        vzTextBox.Top = vzLabel.Top;
        vzTextBox.Left = 200;

        //  Create Button objects 
        int buttonHeight = 30;
        int buttonWidth = 50;
        int buttonLeft = 50;

        fireButton = new Button();
        fireButton.Text = "Fire";
        fireButton.Height = buttonHeight;
        fireButton.Width = buttonWidth;
        fireButton.Top = 100;
        fireButton.Left = buttonLeft;
        fireButton.Click += new EventHandler(FireButtonClicked);

        resetButton = new Button();
        resetButton.Text = "Reset";
        resetButton.Height = buttonHeight;
        resetButton.Width = buttonWidth;
        resetButton.Top = 160;
        resetButton.Left = buttonLeft;
        resetButton.Click += new EventHandler(ResetButtonClicked);

        //  Create a drawing panel.
        drawingPanel = new Panel();
        drawingPanel.Width = 251;
        drawingPanel.Height = 151;
        drawingPanel.Left = 280;
        drawingPanel.Top = 50;
        drawingPanel.BorderStyle = BorderStyle.FixedSingle;

        //  Add the GUI components to the Form
        this.Controls.Add(vxLabel);
        this.Controls.Add(vzLabel);
        this.Controls.Add(vxTextBox);
        this.Controls.Add(vzTextBox);
        this.Controls.Add(fireButton);
        this.Controls.Add(resetButton);
        this.Controls.Add(drawingPanel);

        // Set the size and title of the form
        this.Height = 300;
        this.Width = 600;
        this.Text = "GorilaGame Game";

        //  Center the form on the screen and make
        //  it visible.
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Visible = true;

        //  Update the GUI display
        UpdateDisplay();
    }

    //  Event handling method for the "Fire" button
    public void FireButtonClicked(object source, EventArgs e)
    {

        //  Extract initial data from the textfields.
        double vx0 = Convert.ToDouble(vxTextBox.Text);
        double vz0 = Convert.ToDouble(vzTextBox.Text);
        bBall = new bagBall(0.5, 1.7, vx0, vz0, 0.0);

        //  Start the box sliding using a Timer object
        //  to slow down the action.
        gameTimer.Start();
    }

    //  Event handling method for the "Reset" button
    public void ResetButtonClicked(object source, EventArgs e)
    {
        //  stop the timer.
        gameTimer.Stop();

        //  Reset the bagBall
        bBall = new bagBall(0.5, 1.7, 0.0, 0.0, 0.0);

        //  Update the display.
        UpdateDisplay();
    }

    //  This method redraws the GUI display.
    private void UpdateDisplay()
    {
        Graphics g = drawingPanel.CreateGraphics();
        int width = drawingPanel.Width - 1;
        int height = drawingPanel.Height - 1;

        //  Clear the current display.
        g.Clear(drawingPanel.BackColor);

        //  Draw the GorilaGame tosser.
        int zLocation = 125 - 55;
        g.DrawImage(playerIcon, 7, zLocation, 41, 55);

        Pen blackPen = new Pen(Color.Black, 2);
        g.DrawLine(blackPen, 0, 125, width, 125);
        g.DrawLine(blackPen, 150, 125, 150, height);
        g.DrawLine(blackPen, 175, 125, 175, height);
        g.DrawLine(blackPen, 200, 125, 200, height);
        g.DrawLine(blackPen, 225, 125, 225, height);

        SolidBrush brush = new SolidBrush(Color.Black);
        Font font = new Font("Arial", 12);
        g.DrawString("10", font, brush, 152, 127);
        g.DrawString("20", font, brush, 177, 127);
        g.DrawString("50", font, brush, 202, 127);
        g.DrawString("0", font, brush, 227, 127);

        //  Update the position of the GorilaGame
        //  on the screen.
        double x = bBall.GetX();
        double z = bBall.GetZ();

        int xPosition = (int)(90.0 * x);
        double deltaZ = z - 1.45;
        int zPosition = (int)(125 - 100.0 * deltaZ);
        g.FillEllipse(brush, xPosition, zPosition, 10, 10);

        //  Clean up the Graphics object.
        g.Dispose();
    }

    //  This method is called by the Timer every 0.05 seconds.
    public void ActionPerformed(object source, EventArgs e)
    {
        //  Update the time and compute the new position
        //  of the GorilaGame. 
        double timeIncrement = 0.05;
        bBall.UpdateLocationAndVelocity(timeIncrement);

        // Descomenteu per mirar si s'actualitzen les posicions
        Console.WriteLine("time=" + (float)bBall.GetTime() +
        "  x=" + (float)bBall.GetX() + "  z=" + (float)bBall.GetZ());

        //  Update the display
        UpdateDisplay();

        //  If the GorilaGame hits the ground, stop 
        //  the simulation.
        if (bBall.GetZ() <= 1.35)
        {
            gameTimer.Stop();
        }
    }

    static void Main()
    {
        Application.Run(new GorilaGame());
    }
}
