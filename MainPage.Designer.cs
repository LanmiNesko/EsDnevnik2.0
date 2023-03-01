
namespace EsDnevnik2._0
{
    partial class MainPage
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.jedanBezFKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.osobaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jedanSaFKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raspodelaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridVIewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabelaUceniciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.obradaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ocenaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jedanBezFKToolStripMenuItem,
            this.jedanSaFKToolStripMenuItem,
            this.dataGridVIewToolStripMenuItem,
            this.obradaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // jedanBezFKToolStripMenuItem
            // 
            this.jedanBezFKToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.osobaToolStripMenuItem});
            this.jedanBezFKToolStripMenuItem.Name = "jedanBezFKToolStripMenuItem";
            this.jedanBezFKToolStripMenuItem.Size = new System.Drawing.Size(109, 24);
            this.jedanBezFKToolStripMenuItem.Text = "Jedan Bez FK";
            // 
            // osobaToolStripMenuItem
            // 
            this.osobaToolStripMenuItem.Name = "osobaToolStripMenuItem";
            this.osobaToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.osobaToolStripMenuItem.Text = "Osobe";
            this.osobaToolStripMenuItem.Click += new System.EventHandler(this.osobaToolStripMenuItem_Click);
            // 
            // jedanSaFKToolStripMenuItem
            // 
            this.jedanSaFKToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.raspodelaToolStripMenuItem});
            this.jedanSaFKToolStripMenuItem.Name = "jedanSaFKToolStripMenuItem";
            this.jedanSaFKToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.jedanSaFKToolStripMenuItem.Text = "Jedan Sa FK";
            // 
            // raspodelaToolStripMenuItem
            // 
            this.raspodelaToolStripMenuItem.Name = "raspodelaToolStripMenuItem";
            this.raspodelaToolStripMenuItem.Size = new System.Drawing.Size(162, 26);
            this.raspodelaToolStripMenuItem.Text = "Raspodela";
            this.raspodelaToolStripMenuItem.Click += new System.EventHandler(this.raspodelaToolStripMenuItem_Click);
            // 
            // dataGridVIewToolStripMenuItem
            // 
            this.dataGridVIewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabelaUceniciToolStripMenuItem});
            this.dataGridVIewToolStripMenuItem.Name = "dataGridVIewToolStripMenuItem";
            this.dataGridVIewToolStripMenuItem.Size = new System.Drawing.Size(115, 24);
            this.dataGridVIewToolStripMenuItem.Text = "DataGridVIew";
            // 
            // tabelaUceniciToolStripMenuItem
            // 
            this.tabelaUceniciToolStripMenuItem.Name = "tabelaUceniciToolStripMenuItem";
            this.tabelaUceniciToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.tabelaUceniciToolStripMenuItem.Text = "Tabela ucenici";
            this.tabelaUceniciToolStripMenuItem.Click += new System.EventHandler(this.tabelaUceniciToolStripMenuItem_Click);
            // 
            // obradaToolStripMenuItem
            // 
            this.obradaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ocenaToolStripMenuItem});
            this.obradaToolStripMenuItem.Name = "obradaToolStripMenuItem";
            this.obradaToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.obradaToolStripMenuItem.Text = "Obrada";
            // 
            // ocenaToolStripMenuItem
            // 
            this.ocenaToolStripMenuItem.Name = "ocenaToolStripMenuItem";
            this.ocenaToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.ocenaToolStripMenuItem.Text = "Ocene";
            this.ocenaToolStripMenuItem.Click += new System.EventHandler(this.ocenaToolStripMenuItem_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainPage";
            this.Text = "MainPage";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem jedanBezFKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem osobaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jedanSaFKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raspodelaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataGridVIewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tabelaUceniciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem obradaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ocenaToolStripMenuItem;
    }
}