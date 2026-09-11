namespace TimeCalculator;

public class MainForm : Form
{
    private readonly TextBox startTimeTextBox;
    private readonly TextBox stopTimeTextBox;
    private readonly TextBox resultTextBox;
    private readonly Label resultDetailLabel;
    private readonly Label statusLabel;

    public MainForm()
    {
        Text = "Time Calculator";
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(204, 204, 204);
        Size = new Size(300, 360);
        MinimumSize = new Size(300, 360);
        MaximumSize = new Size(300, 360);
        KeyPreview = true;
        Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        var headerLabel = new Label
        {
            Text = "Time Calculator",
            Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point),
            Location = new Point(20, 20),
            Size = new Size(240, 40),
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.Black
        };

        var startLabel = new Label
        {
            Text = "Start Time",
            Location = new Point(20, 70),
            Size = new Size(160, 18),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.Black
        };

        startTimeTextBox = new TextBox
        {
            Location = new Point(20, 90),
            Size = new Size(240, 30),
            Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point),
            TextAlign = HorizontalAlignment.Center,
            MaxLength = 5,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.FromArgb(245, 245, 245)
        };

        var stopLabel = new Label
        {
            Text = "Stop Time",
            Location = new Point(20, 135),
            Size = new Size(160, 18),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.Black
        };

        stopTimeTextBox = new TextBox
        {
            Location = new Point(20, 155),
            Size = new Size(240, 30),
            Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point),
            TextAlign = HorizontalAlignment.Center,
            MaxLength = 5,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.FromArgb(245, 245, 245)
        };

        var calculateButton = new Button
        {
            Text = "Calculate",
            Location = new Point(20, 205),
            Size = new Size(110, 32),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
            BackColor = Color.FromArgb(225, 225, 225),
            FlatStyle = FlatStyle.Standard
        };

        var clearButton = new Button
        {
            Text = "Clear",
            Location = new Point(150, 205),
            Size = new Size(110, 32),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
            BackColor = Color.FromArgb(225, 225, 225),
            FlatStyle = FlatStyle.Standard
        };

        var resultLabel = new Label
        {
            Text = "Result",
            Location = new Point(20, 250),
            Size = new Size(80, 18),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.Black
        };

        resultTextBox = new TextBox
        {
            Location = new Point(20, 270),
            Size = new Size(240, 35),
            Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point),
            TextAlign = HorizontalAlignment.Center,
            ReadOnly = true,
            BorderStyle = BorderStyle.FixedSingle,
            BackColor = Color.White
        };

        resultDetailLabel = new Label
        {
            Location = new Point(20, 310),
            Size = new Size(240, 20),
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = Color.FromArgb(80, 80, 80),
            TextAlign = ContentAlignment.MiddleCenter
        };

        statusLabel = new Label
        {
            Location = new Point(20, 332),
            Size = new Size(240, 20),
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = Color.DarkRed,
            TextAlign = ContentAlignment.MiddleCenter
        };

        Controls.Add(headerLabel);
        Controls.Add(startLabel);
        Controls.Add(startTimeTextBox);
        Controls.Add(stopLabel);
        Controls.Add(stopTimeTextBox);
        Controls.Add(calculateButton);
        Controls.Add(clearButton);
        Controls.Add(resultLabel);
        Controls.Add(resultTextBox);
        Controls.Add(resultDetailLabel);
        Controls.Add(statusLabel);

        calculateButton.Click += CalculateButton_Click;
        clearButton.Click += ClearButton_Click;
        KeyDown += MainForm_KeyDown;
        startTimeTextBox.KeyDown += MainForm_KeyDown;
        stopTimeTextBox.KeyDown += MainForm_KeyDown;
        startTimeTextBox.KeyPress += TimeTextBox_KeyPress;
        stopTimeTextBox.KeyPress += TimeTextBox_KeyPress;
        startTimeTextBox.TextChanged += TimeTextBox_TextChanged;
        stopTimeTextBox.TextChanged += TimeTextBox_TextChanged;
        startTimeTextBox.Enter += (_, _) => startTimeTextBox.Select(startTimeTextBox.Text.Length, 0);
        stopTimeTextBox.Enter += (_, _) => stopTimeTextBox.Select(stopTimeTextBox.Text.Length, 0);
        Shown += (_, _) => startTimeTextBox.Focus();

        ClearForm();
    }

    private void CalculateButton_Click(object? sender, EventArgs e)
    {
        try
        {
            var totalMinutes = TimeCalculator.CalculateDurationInMinutes(startTimeTextBox.Text, stopTimeTextBox.Text);
            resultTextBox.Text = TimeCalculator.FormatMainResult(totalMinutes);
            resultDetailLabel.Text = TimeCalculator.FormatDetailedDuration(totalMinutes);
            statusLabel.Text = string.Empty;
        }
        catch (ArgumentException ex)
        {
            ShowError(ex.Message);
        }
    }

    private void ClearButton_Click(object? sender, EventArgs e)
    {
        ClearForm();
    }

    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = true;
            CalculateButton_Click(this, EventArgs.Empty);
        }
        else if (e.KeyCode == Keys.Escape)
        {
            e.Handled = true;
            ClearForm();
        }
    }

    private void ShowError(string message)
    {
        statusLabel.Text = message;
        resultTextBox.Text = string.Empty;
        resultDetailLabel.Text = string.Empty;
    }

    private void TimeTextBox_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
        {
            return;
        }

        e.Handled = true;
    }

    private void TimeTextBox_TextChanged(object? sender, EventArgs e)
    {
        if (sender is not TextBox textBox)
        {
            return;
        }

        var value = textBox.Text ?? string.Empty;
        var formattedText = TimeCalculator.NormalizeInputTime(value);

        if (formattedText == value)
        {
            if (formattedText.Length == 2 && !formattedText.EndsWith(':'))
            {
                textBox.Text = formattedText + ":";
                textBox.SelectionStart = textBox.Text.Length;
            }
            return;
        }

        textBox.Text = formattedText;

        if (formattedText.Length >= 3)
        {
            textBox.SelectionStart = formattedText.Length;
        }
        else if (formattedText.Length == 2)
        {
            textBox.SelectionStart = 3;
        }
        else
        {
            textBox.SelectionStart = formattedText.Length;
        }

        textBox.SelectionLength = 0;
    }

    private void ClearForm()
    {
        startTimeTextBox.Clear();
        stopTimeTextBox.Clear();
        resultTextBox.Clear();
        resultDetailLabel.Text = string.Empty;
        statusLabel.Text = string.Empty;
        startTimeTextBox.Focus();
    }
}
