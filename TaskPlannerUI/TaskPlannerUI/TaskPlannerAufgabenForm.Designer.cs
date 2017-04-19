namespace TaskPlannerUI
{
    partial class TaskPlannerMainForm
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
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.aufgabenliste = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxAufgabeneditor = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.TabPageAufgaben = new System.Windows.Forms.TabPage();
            this.TabPageTags = new System.Windows.Forms.TabPage();
            this.listBoxTags = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.TabPageAufgaben.SuspendLayout();
            this.TabPageTags.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxFilter.Location = new System.Drawing.Point(3, 3);
            this.textBoxFilter.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(429, 29);
            this.textBoxFilter.TabIndex = 0;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // aufgabenliste
            // 
            this.aufgabenliste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aufgabenliste.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aufgabenliste.FormattingEnabled = true;
            this.aufgabenliste.ItemHeight = 21;
            this.aufgabenliste.Location = new System.Drawing.Point(3, 32);
            this.aufgabenliste.Margin = new System.Windows.Forms.Padding(6);
            this.aufgabenliste.Name = "aufgabenliste";
            this.aufgabenliste.Size = new System.Drawing.Size(429, 606);
            this.aufgabenliste.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxAufgabeneditor);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 648);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 30);
            this.panel1.TabIndex = 2;
            // 
            // textBoxAufgabeneditor
            // 
            this.textBoxAufgabeneditor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxAufgabeneditor.Location = new System.Drawing.Point(0, 1);
            this.textBoxAufgabeneditor.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxAufgabeneditor.Name = "textBoxAufgabeneditor";
            this.textBoxAufgabeneditor.Size = new System.Drawing.Size(305, 29);
            this.textBoxAufgabeneditor.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.okButton.Location = new System.Drawing.Point(305, 0);
            this.okButton.Margin = new System.Windows.Forms.Padding(6);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(138, 30);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.TabPageAufgaben);
            this.tabControl.Controls.Add(this.TabPageTags);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(443, 678);
            this.tabControl.TabIndex = 3;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // TabPageAufgaben
            // 
            this.TabPageAufgaben.Controls.Add(this.aufgabenliste);
            this.TabPageAufgaben.Controls.Add(this.textBoxFilter);
            this.TabPageAufgaben.Location = new System.Drawing.Point(4, 33);
            this.TabPageAufgaben.Name = "TabPageAufgaben";
            this.TabPageAufgaben.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageAufgaben.Size = new System.Drawing.Size(435, 641);
            this.TabPageAufgaben.TabIndex = 0;
            this.TabPageAufgaben.Text = "Aufgaben";
            this.TabPageAufgaben.UseVisualStyleBackColor = true;
            // 
            // TabPageTags
            // 
            this.TabPageTags.Controls.Add(this.listBoxTags);
            this.TabPageTags.Location = new System.Drawing.Point(4, 33);
            this.TabPageTags.Name = "TabPageTags";
            this.TabPageTags.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageTags.Size = new System.Drawing.Size(435, 641);
            this.TabPageTags.TabIndex = 1;
            this.TabPageTags.Text = "Tags";
            this.TabPageTags.UseVisualStyleBackColor = true;
            // 
            // listBoxTags
            // 
            this.listBoxTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTags.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxTags.FormattingEnabled = true;
            this.listBoxTags.ItemHeight = 21;
            this.listBoxTags.Location = new System.Drawing.Point(3, 3);
            this.listBoxTags.Margin = new System.Windows.Forms.Padding(6);
            this.listBoxTags.Name = "listBoxTags";
            this.listBoxTags.Size = new System.Drawing.Size(429, 635);
            this.listBoxTags.TabIndex = 2;
            this.listBoxTags.SelectedIndexChanged += new System.EventHandler(this.listBoxTags_SelectedIndexChanged);
            // 
            // TaskPlannerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 678);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "TaskPlannerMainForm";
            this.Text = "TaskPlannerAufgabenForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.TabPageAufgaben.ResumeLayout(false);
            this.TabPageAufgaben.PerformLayout();
            this.TabPageTags.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.ListBox aufgabenliste;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxAufgabeneditor;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage TabPageAufgaben;
        private System.Windows.Forms.TabPage TabPageTags;
        private System.Windows.Forms.ListBox listBoxTags;
    }
}

