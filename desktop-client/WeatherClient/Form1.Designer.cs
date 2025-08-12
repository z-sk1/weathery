namespace WeatherClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.btnGet = new System.Windows.Forms.Button();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.toggleFarenheit = new System.Windows.Forms.CheckBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.lblTemperature = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCondition = new System.Windows.Forms.Label();
            this.lblWindSpeed = new System.Windows.Forms.Label();
            this.lblPrecipitation = new System.Windows.Forms.Label();
            this.lblHumidity = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(349, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "weathery";
            // 
            // btnGet
            // 
            this.btnGet.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnGet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGet.Location = new System.Drawing.Point(523, 197);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(154, 40);
            this.btnGet.TabIndex = 1;
            this.btnGet.Text = "Get Weather";
            this.btnGet.UseVisualStyleBackColor = false;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // txtCity
            // 
            this.txtCity.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtCity.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.ForeColor = System.Drawing.Color.DimGray;
            this.txtCity.Location = new System.Drawing.Point(309, 204);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(198, 27);
            this.txtCity.TabIndex = 2;
            this.txtCity.Text = "Enter a city name...";
            this.txtCity.Enter += new System.EventHandler(this.txtCity_Enter);
            this.txtCity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCity_KeyDown);
            this.txtCity.Leave += new System.EventHandler(this.txtCity_Leave);
            // 
            // toggleFarenheit
            // 
            this.toggleFarenheit.AutoSize = true;
            this.toggleFarenheit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toggleFarenheit.Location = new System.Drawing.Point(694, 204);
            this.toggleFarenheit.Name = "toggleFarenheit";
            this.toggleFarenheit.Size = new System.Drawing.Size(41, 20);
            this.toggleFarenheit.TabIndex = 4;
            this.toggleFarenheit.Text = "°F";
            this.toggleFarenheit.UseVisualStyleBackColor = true;
            this.toggleFarenheit.CheckedChanged += new System.EventHandler(this.toggleFarenheit_CheckedChanged);
            // 
            // btnCopy
            // 
            this.btnCopy.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.Location = new System.Drawing.Point(310, 222);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(96, 36);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // lblTemperature
            // 
            this.lblTemperature.AutoSize = true;
            this.lblTemperature.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemperature.Location = new System.Drawing.Point(45, 35);
            this.lblTemperature.Name = "lblTemperature";
            this.lblTemperature.Size = new System.Drawing.Size(0, 22);
            this.lblTemperature.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.lblCondition);
            this.panel1.Controls.Add(this.lblWindSpeed);
            this.panel1.Controls.Add(this.lblPrecipitation);
            this.panel1.Controls.Add(this.lblHumidity);
            this.panel1.Controls.Add(this.btnCopy);
            this.panel1.Controls.Add(this.lblTemperature);
            this.panel1.Location = new System.Drawing.Point(309, 243);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(409, 261);
            this.panel1.TabIndex = 7;
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCondition.Location = new System.Drawing.Point(45, 206);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(0, 22);
            this.lblCondition.TabIndex = 10;
            // 
            // lblWindSpeed
            // 
            this.lblWindSpeed.AutoSize = true;
            this.lblWindSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWindSpeed.Location = new System.Drawing.Point(45, 156);
            this.lblWindSpeed.Name = "lblWindSpeed";
            this.lblWindSpeed.Size = new System.Drawing.Size(0, 22);
            this.lblWindSpeed.TabIndex = 9;
            // 
            // lblPrecipitation
            // 
            this.lblPrecipitation.AutoSize = true;
            this.lblPrecipitation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecipitation.Location = new System.Drawing.Point(45, 113);
            this.lblPrecipitation.Name = "lblPrecipitation";
            this.lblPrecipitation.Size = new System.Drawing.Size(0, 22);
            this.lblPrecipitation.TabIndex = 8;
            // 
            // lblHumidity
            // 
            this.lblHumidity.AutoSize = true;
            this.lblHumidity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHumidity.Location = new System.Drawing.Point(45, 73);
            this.lblHumidity.Name = "lblHumidity";
            this.lblHumidity.Size = new System.Drawing.Size(0, 22);
            this.lblHumidity.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(938, 553);
            this.Controls.Add(this.toggleFarenheit);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "weathery";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.CheckBox toggleFarenheit;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHumidity;
        private System.Windows.Forms.Label lblPrecipitation;
        private System.Windows.Forms.Label lblWindSpeed;
        private System.Windows.Forms.Label lblCondition;
    }
}

