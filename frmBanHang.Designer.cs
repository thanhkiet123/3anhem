namespace QUANLYNHAHANG
{
    partial class frmBanHang
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDatBan = new System.Windows.Forms.Button();
            this.btnTachBan = new System.Windows.Forms.Button();
            this.btnGopBan = new System.Windows.Forms.Button();
            this.btnChuyenBan = new System.Windows.Forms.Button();
            this.btnB = new System.Windows.Forms.Button();
            this.btnVip = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.flpBan = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 116);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flpBan);
            this.splitContainer1.Size = new System.Drawing.Size(1477, 716);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.SplitterWidth = 9;
            this.splitContainer1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnDatBan, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnTachBan, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnGopBan, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnChuyenBan, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnB, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnVip, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnA, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(235, 716);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnDatBan
            // 
            this.btnDatBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDatBan.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatBan.Location = new System.Drawing.Point(8, 621);
            this.btnDatBan.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.btnDatBan.Name = "btnDatBan";
            this.btnDatBan.Size = new System.Drawing.Size(219, 86);
            this.btnDatBan.TabIndex = 7;
            this.btnDatBan.Text = "Đặt bàn ";
            this.btnDatBan.UseVisualStyleBackColor = true;
            this.btnDatBan.Click += new System.EventHandler(this.btnDatBan_Click);
            // 
            // btnTachBan
            // 
            this.btnTachBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTachBan.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTachBan.Location = new System.Drawing.Point(8, 519);
            this.btnTachBan.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.btnTachBan.Name = "btnTachBan";
            this.btnTachBan.Size = new System.Drawing.Size(219, 84);
            this.btnTachBan.TabIndex = 6;
            this.btnTachBan.Text = "Tách bàn";
            this.btnTachBan.UseVisualStyleBackColor = true;
            this.btnTachBan.Click += new System.EventHandler(this.btnTachBan_Click);
            // 
            // btnGopBan
            // 
            this.btnGopBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGopBan.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGopBan.Location = new System.Drawing.Point(8, 417);
            this.btnGopBan.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.btnGopBan.Name = "btnGopBan";
            this.btnGopBan.Size = new System.Drawing.Size(219, 84);
            this.btnGopBan.TabIndex = 5;
            this.btnGopBan.Text = "Gộp bàn";
            this.btnGopBan.UseVisualStyleBackColor = true;
            this.btnGopBan.Click += new System.EventHandler(this.btnGopBan_Click);
            // 
            // btnChuyenBan
            // 
            this.btnChuyenBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChuyenBan.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChuyenBan.Location = new System.Drawing.Point(8, 315);
            this.btnChuyenBan.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.btnChuyenBan.Name = "btnChuyenBan";
            this.btnChuyenBan.Size = new System.Drawing.Size(219, 84);
            this.btnChuyenBan.TabIndex = 4;
            this.btnChuyenBan.Text = "Chuyển bàn";
            this.btnChuyenBan.UseVisualStyleBackColor = true;
            this.btnChuyenBan.Click += new System.EventHandler(this.btnChuyenBan_Click);
            // 
            // btnB
            // 
            this.btnB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnB.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnB.Location = new System.Drawing.Point(8, 111);
            this.btnB.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(219, 84);
            this.btnB.TabIndex = 0;
            this.btnB.Text = "KHU B";
            this.btnB.UseVisualStyleBackColor = true;
            this.btnB.Click += new System.EventHandler(this.btnB_Click);
            // 
            // btnVip
            // 
            this.btnVip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnVip.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVip.Location = new System.Drawing.Point(8, 213);
            this.btnVip.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.btnVip.Name = "btnVip";
            this.btnVip.Size = new System.Drawing.Size(219, 84);
            this.btnVip.TabIndex = 1;
            this.btnVip.Text = "VIP";
            this.btnVip.UseVisualStyleBackColor = true;
            this.btnVip.Click += new System.EventHandler(this.btnVip_Click);
            // 
            // btnA
            // 
            this.btnA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnA.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnA.Location = new System.Drawing.Point(8, 9);
            this.btnA.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(219, 84);
            this.btnA.TabIndex = 3;
            this.btnA.Text = "KHU A";
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // flpBan
            // 
            this.flpBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBan.Location = new System.Drawing.Point(0, 0);
            this.flpBan.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.flpBan.Name = "flpBan";
            this.flpBan.Size = new System.Drawing.Size(1233, 716);
            this.flpBan.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(8, 154);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 39);
            this.label1.TabIndex = 3;
            this.label1.Text = "KHU VỰC BÀN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(684, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(315, 40);
            this.label2.TabIndex = 4;
            this.label2.Text = "BETU RESTAURANT";
            // 
            // frmBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(1477, 832);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Cambria", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.Name = "frmBanHang";
            this.Padding = new System.Windows.Forms.Padding(0, 116, 0, 0);
            this.Text = "BÁN HÀNG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBanHang_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnTachBan;
        private System.Windows.Forms.Button btnGopBan;
        private System.Windows.Forms.Button btnChuyenBan;
        private System.Windows.Forms.Button btnB;
        private System.Windows.Forms.Button btnVip;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.FlowLayoutPanel flpBan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDatBan;
    }
}

