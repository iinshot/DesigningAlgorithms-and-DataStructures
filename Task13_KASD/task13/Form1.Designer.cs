namespace task13
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ZedGraph = new ZedGraph.ZedGraphControl();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ZedGraph
            // 
            this.ZedGraph.Location = new System.Drawing.Point(1, -1);
            this.ZedGraph.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ZedGraph.Name = "ZedGraph";
            this.ZedGraph.ScrollGrace = 0D;
            this.ZedGraph.ScrollMaxX = 0D;
            this.ZedGraph.ScrollMaxY = 0D;
            this.ZedGraph.ScrollMaxY2 = 0D;
            this.ZedGraph.ScrollMinX = 0D;
            this.ZedGraph.ScrollMinY = 0D;
            this.ZedGraph.ScrollMinY2 = 0D;
            this.ZedGraph.Size = new System.Drawing.Size(1149, 712);
            this.ZedGraph.TabIndex = 0;
            this.ZedGraph.UseExtendedPrintDialog = true;
            this.ZedGraph.Load += new System.EventHandler(this.ZedGraph_Load);
            // 
            // comboBox2
            // 
            this.comboBox2.BackColor = System.Drawing.SystemColors.Info;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Random nums of module 1000",
            "Subarrays with different size",
            "Random nums with exchange",
            "Sorted arrays"});
            this.comboBox2.Location = new System.Drawing.Point(1202, 45);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(342, 24);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.Text = "Group of tests";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.BackColor = System.Drawing.SystemColors.Info;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "First group",
            "Second group",
            "Third group"});
            this.comboBox3.Location = new System.Drawing.Point(1202, 227);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(342, 24);
            this.comboBox3.TabIndex = 3;
            this.comboBox3.Text = "Group of sorts";
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(1202, 457);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(342, 84);
            this.button1.TabIndex = 4;
            this.button1.Text = "Generate Array";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(1202, 580);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(342, 84);
            this.button2.TabIndex = 5;
            this.button2.Text = "Common! Lets go!";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1567, 711);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.ZedGraph);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl ZedGraph;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

