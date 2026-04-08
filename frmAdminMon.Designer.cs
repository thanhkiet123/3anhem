namespace QUANLYNHAHANG
{
    partial class frmAdminMon
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
            this.dgvMonAn = new System.Windows.Forms.DataGridView();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.pnlInput = new System.Windows.Forms.FlowLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMaMon = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTenMon = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtSLBan = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtSLNhap = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDonViTinh = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHinhAnh = new System.Windows.Forms.TextBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cboDanhMuc = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.bntThoat = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaBan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTenBan = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSoChoNgoi = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonAn)).BeginInit();
            this.pnlGrid.SuspendLayout();
            this.pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMonAn
            // 
            this.dgvMonAn.AllowUserToAddRows = false;
            this.dgvMonAn.AllowUserToDeleteRows = false;
            this.dgvMonAn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMonAn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(124)))));
            this.dgvMonAn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMonAn.Location = new System.Drawing.Point(0, 0);
            this.dgvMonAn.Name = "dgvMonAn";
            this.dgvMonAn.ReadOnly = true;
            this.dgvMonAn.RowHeadersWidth = 51;
            this.dgvMonAn.Size = new System.Drawing.Size(631, 601);
            this.dgvMonAn.TabIndex = 0;
            this.dgvMonAn.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMonAn_CellClick);
            this.dgvMonAn.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMonAn_CellContentClick);
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dgvMonAn);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(633, 80);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(10);
            this.pnlGrid.Size = new System.Drawing.Size(631, 601);
            this.pnlGrid.TabIndex = 50;
            // 
            // pnlInput
            // 
            this.pnlInput.BackColor = System.Drawing.Color.White;
            this.pnlInput.Controls.Add(this.label12);
            this.pnlInput.Controls.Add(this.txtMaMon);
            this.pnlInput.Controls.Add(this.label7);
            this.pnlInput.Controls.Add(this.txtTenMon);
            this.pnlInput.Controls.Add(this.label13);
            this.pnlInput.Controls.Add(this.txtGiaBan);
            this.pnlInput.Controls.Add(this.label17);
            this.pnlInput.Controls.Add(this.txtGiaNhap);
            this.pnlInput.Controls.Add(this.label18);
            this.pnlInput.Controls.Add(this.txtSLBan);
            this.pnlInput.Controls.Add(this.label19);
            this.pnlInput.Controls.Add(this.txtSLNhap);
            this.pnlInput.Controls.Add(this.label14);
            this.pnlInput.Controls.Add(this.txtDonViTinh);
            this.pnlInput.Controls.Add(this.label8);
            this.pnlInput.Controls.Add(this.txtHinhAnh);
            this.pnlInput.Controls.Add(this.picPreview);
            this.pnlInput.Controls.Add(this.btnChonAnh);
            this.pnlInput.Controls.Add(this.label9);
            this.pnlInput.Controls.Add(this.txtMoTa);
            this.pnlInput.Controls.Add(this.label10);
            this.pnlInput.Controls.Add(this.cboTrangThai);
            this.pnlInput.Controls.Add(this.label15);
            this.pnlInput.Controls.Add(this.cboDanhMuc);
            this.pnlInput.Controls.Add(this.label11);
            this.pnlInput.Controls.Add(this.label16);
            this.pnlInput.Controls.Add(this.btnThem);
            this.pnlInput.Controls.Add(this.btnSua);
            this.pnlInput.Controls.Add(this.btnXoa);
            this.pnlInput.Controls.Add(this.btnLamMoi);
            this.pnlInput.Controls.Add(this.btnLuu);
            this.pnlInput.Controls.Add(this.bntThoat);
            this.pnlInput.Controls.Add(this.txtTimKiem);
            this.pnlInput.Controls.Add(this.btnTimKiem);
            this.pnlInput.Controls.Add(this.btnImportExcel);
            this.pnlInput.Controls.Add(this.btnExportExcel);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlInput.Location = new System.Drawing.Point(0, 80);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Padding = new System.Windows.Forms.Padding(20);
            this.pnlInput.Size = new System.Drawing.Size(633, 601);
            this.pnlInput.TabIndex = 49;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(145, 31);
            this.label12.TabIndex = 73;
            this.label12.Text = "🔢Mã Món";
            // 
            // txtMaMon
            // 
            this.txtMaMon.Location = new System.Drawing.Point(174, 23);
            this.txtMaMon.Name = "txtMaMon";
            this.txtMaMon.Size = new System.Drawing.Size(112, 39);
            this.txtMaMon.TabIndex = 72;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(292, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 31);
            this.label7.TabIndex = 71;
            this.label7.Text = "📝Tên Món";
            // 
            // txtTenMon
            // 
            this.txtTenMon.Location = new System.Drawing.Point(449, 23);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.Size = new System.Drawing.Size(134, 39);
            this.txtTenMon.TabIndex = 70;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 65);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 31);
            this.label13.TabIndex = 78;
            this.label13.Text = "💰Giá Bán";
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Location = new System.Drawing.Point(171, 68);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(115, 39);
            this.txtGiaBan.TabIndex = 77;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(292, 65);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(128, 31);
            this.label17.TabIndex = 90;
            this.label17.Text = "Giá Nhập";
            // 
            // txtGiaNhap
            // 
            this.txtGiaNhap.Location = new System.Drawing.Point(426, 68);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaNhap.Size = new System.Drawing.Size(157, 39);
            this.txtGiaNhap.TabIndex = 91;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(23, 110);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 31);
            this.label18.TabIndex = 92;
            this.label18.Text = "SL Bán";
            // 
            // txtSLBan
            // 
            this.txtSLBan.Location = new System.Drawing.Point(129, 113);
            this.txtSLBan.Name = "txtSLBan";
            this.txtSLBan.Size = new System.Drawing.Size(58, 39);
            this.txtSLBan.TabIndex = 93;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(193, 110);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(117, 31);
            this.label19.TabIndex = 94;
            this.label19.Text = "SL Nhập";
            // 
            // txtSLNhap
            // 
            this.txtSLNhap.Location = new System.Drawing.Point(316, 113);
            this.txtSLNhap.Name = "txtSLNhap";
            this.txtSLNhap.Size = new System.Drawing.Size(55, 39);
            this.txtSLNhap.TabIndex = 95;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(377, 110);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(147, 31);
            this.label14.TabIndex = 80;
            this.label14.Text = "🔢ĐV Tính";
            // 
            // txtDonViTinh
            // 
            this.txtDonViTinh.Location = new System.Drawing.Point(530, 113);
            this.txtDonViTinh.Name = "txtDonViTinh";
            this.txtDonViTinh.Size = new System.Drawing.Size(80, 39);
            this.txtDonViTinh.TabIndex = 79;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(160, 31);
            this.label8.TabIndex = 69;
            this.label8.Text = "🖼️Hình Ảnh";
            // 
            // txtHinhAnh
            // 
            this.txtHinhAnh.Location = new System.Drawing.Point(189, 158);
            this.txtHinhAnh.Name = "txtHinhAnh";
            this.txtHinhAnh.Size = new System.Drawing.Size(112, 39);
            this.txtHinhAnh.TabIndex = 76;
            this.txtHinhAnh.TextChanged += new System.EventHandler(this.txtHinhAnh_TextChanged);
            // 
            // picPreview
            // 
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(307, 158);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(230, 154);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 74;
            this.picPreview.TabStop = false;
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(124)))));
            this.btnChonAnh.ForeColor = System.Drawing.Color.Yellow;
            this.btnChonAnh.Location = new System.Drawing.Point(543, 158);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(52, 44);
            this.btnChonAnh.TabIndex = 75;
            this.btnChonAnh.Text = "📁 ";
            this.btnChonAnh.UseVisualStyleBackColor = false;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 315);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 31);
            this.label9.TabIndex = 68;
            this.label9.Text = "📝Mô Tả";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(152, 318);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(344, 39);
            this.txtMoTa.TabIndex = 67;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 360);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(146, 31);
            this.label10.TabIndex = 66;
            this.label10.Text = "Trạng Thái";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(175, 363);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(120, 39);
            this.cboTrangThai.TabIndex = 65;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(301, 360);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(137, 31);
            this.label15.TabIndex = 81;
            this.label15.Text = "Danh Mục";
            // 
            // cboDanhMuc
            // 
            this.cboDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMuc.FormattingEnabled = true;
            this.cboDanhMuc.Location = new System.Drawing.Point(444, 363);
            this.cboDanhMuc.Name = "cboDanhMuc";
            this.cboDanhMuc.Size = new System.Drawing.Size(152, 39);
            this.cboDanhMuc.TabIndex = 82;
            this.cboDanhMuc.SelectedIndexChanged += new System.EventHandler(this.cboDanhMuc_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(602, 360);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 31);
            this.label11.TabIndex = 64;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(23, 405);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(576, 32);
            this.label16.TabIndex = 89;
            this.label16.Text = "                                      ";
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(23, 440);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(184, 38);
            this.btnThem.TabIndex = 58;
            this.btnThem.Text = "➕Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(213, 440);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(196, 38);
            this.btnSua.TabIndex = 59;
            this.btnSua.Text = "✏️Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(415, 440);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(184, 38);
            this.btnXoa.TabIndex = 60;
            this.btnXoa.Text = "🗑️Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(23, 484);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(184, 38);
            this.btnLamMoi.TabIndex = 61;
            this.btnLamMoi.Text = "🔄Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(213, 484);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(196, 38);
            this.btnLuu.TabIndex = 62;
            this.btnLuu.Text = "💾Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // bntThoat
            // 
            this.bntThoat.Location = new System.Drawing.Point(415, 484);
            this.bntThoat.Name = "bntThoat";
            this.bntThoat.Size = new System.Drawing.Size(184, 38);
            this.bntThoat.TabIndex = 63;
            this.bntThoat.Text = "🚪Thoát";
            this.bntThoat.UseVisualStyleBackColor = true;
            this.bntThoat.Click += new System.EventHandler(this.bntThoat_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(23, 528);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(514, 39);
            this.txtTimKiem.TabIndex = 83;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.ForeColor = System.Drawing.Color.Navy;
            this.btnTimKiem.Location = new System.Drawing.Point(543, 528);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(56, 39);
            this.btnTimKiem.TabIndex = 86;
            this.btnTimKiem.Text = "🔍";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(124)))));
            this.btnImportExcel.ForeColor = System.Drawing.Color.Yellow;
            this.btnImportExcel.Location = new System.Drawing.Point(23, 573);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(278, 38);
            this.btnImportExcel.TabIndex = 88;
            this.btnImportExcel.Text = "📂📥";
            this.btnImportExcel.UseVisualStyleBackColor = false;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(124)))));
            this.btnExportExcel.ForeColor = System.Drawing.Color.Yellow;
            this.btnExportExcel.Location = new System.Drawing.Point(307, 573);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(288, 38);
            this.btnExportExcel.TabIndex = 87;
            this.btnExportExcel.Text = "📄📤";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 347);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 31);
            this.label6.TabIndex = 41;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1264, 80);
            this.pnlHeader.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(124)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "🍲 QUẢN LÝ MÓN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(124)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(241, 31);
            this.lblTitle.TabIndex = 39;
            this.lblTitle.Text = "🪑 QUẢN LÝ BÀN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 31);
            this.label3.TabIndex = 42;
            this.label3.Text = "Trạng Thái";
            // 
            // txtMaBan
            // 
            this.txtMaBan.Location = new System.Drawing.Point(12, 122);
            this.txtMaBan.Name = "txtMaBan";
            this.txtMaBan.Size = new System.Drawing.Size(191, 39);
            this.txtMaBan.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 31);
            this.label4.TabIndex = 46;
            this.label4.Text = "Tên Bàn           ";
            // 
            // txtTenBan
            // 
            this.txtTenBan.Location = new System.Drawing.Point(12, 198);
            this.txtTenBan.Name = "txtTenBan";
            this.txtTenBan.Size = new System.Drawing.Size(191, 39);
            this.txtTenBan.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 31);
            this.label2.TabIndex = 44;
            this.label2.Text = "Số Chỗ Ngồi";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 31);
            this.label5.TabIndex = 48;
            this.label5.Text = "Mã Bàn             ";
            // 
            // txtSoChoNgoi
            // 
            this.txtSoChoNgoi.Location = new System.Drawing.Point(12, 274);
            this.txtSoChoNgoi.Name = "txtSoChoNgoi";
            this.txtSoChoNgoi.Size = new System.Drawing.Size(191, 39);
            this.txtSoChoNgoi.TabIndex = 43;
            // 
            // frmAdminMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMaBan);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTenBan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSoChoNgoi);
            this.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "frmAdminMon";
            this.Text = "frmAdminMon";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAdminMon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonAn)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMonAn;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.FlowLayoutPanel pnlInput;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMaMon;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTenMon;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDonViTinh;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtHinhAnh;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboDanhMuc;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button bntThoat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaBan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTenBan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSoChoNgoi;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtSLBan;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtSLNhap;
    }
}