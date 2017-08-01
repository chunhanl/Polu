namespace SQL_Client
{
    partial class Form2
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imgBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.TextBox();
            this.modelID = new System.Windows.Forms.TextBox();
            this.GetNext = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.list_category = new System.Windows.Forms.ListBox();
            this.list_gender = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_LMT = new System.Windows.Forms.TextBox();
            this.list_manufact = new System.Windows.Forms.ListBox();
            this.textB_weight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textB_cost = new System.Windows.Forms.TextBox();
            this.btn_stone = new System.Windows.Forms.Button();
            this.textB_comment = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Upload = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.status = new System.Windows.Forms.TextBox();
            this.btn_pre = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtB_preimage = new System.Windows.Forms.TextBox();
            this.txtB_3dm = new System.Windows.Forms.TextBox();
            this.txtB_stl = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(109, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(106, 22);
            this.textBox1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "ACCOUNT";
            // 
            // imgBox
            // 
            this.imgBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgBox.Cursor = System.Windows.Forms.Cursors.No;
            this.imgBox.Location = new System.Drawing.Point(35, 75);
            this.imgBox.Name = "imgBox";
            this.imgBox.Size = new System.Drawing.Size(538, 374);
            this.imgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgBox.TabIndex = 5;
            this.imgBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(54, 470);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "資料庫 ID";
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(136, 470);
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Size = new System.Drawing.Size(106, 22);
            this.id.TabIndex = 17;
            this.id.TabStop = false;
            // 
            // modelID
            // 
            this.modelID.Location = new System.Drawing.Point(403, 470);
            this.modelID.Name = "modelID";
            this.modelID.ReadOnly = true;
            this.modelID.Size = new System.Drawing.Size(106, 22);
            this.modelID.TabIndex = 10;
            this.modelID.TabStop = false;
            // 
            // GetNext
            // 
            this.GetNext.Font = new System.Drawing.Font("PMingLiU", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GetNext.Location = new System.Drawing.Point(996, 632);
            this.GetNext.Name = "GetNext";
            this.GetNext.Size = new System.Drawing.Size(185, 59);
            this.GetNext.TabIndex = 8;
            this.GetNext.Text = "下一筆";
            this.GetNext.UseVisualStyleBackColor = true;
            this.GetNext.Click += new System.EventHandler(this.GetNext_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(327, 470);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "模型 ID";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // list_category
            // 
            this.list_category.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list_category.FormattingEnabled = true;
            this.list_category.ItemHeight = 37;
            this.list_category.Location = new System.Drawing.Point(644, 75);
            this.list_category.Name = "list_category";
            this.list_category.Size = new System.Drawing.Size(259, 263);
            this.list_category.TabIndex = 0;
            // 
            // list_gender
            // 
            this.list_gender.Font = new System.Drawing.Font("Arial Narrow", 24F);
            this.list_gender.FormattingEnabled = true;
            this.list_gender.ItemHeight = 37;
            this.list_gender.Location = new System.Drawing.Point(911, 75);
            this.list_gender.MultiColumn = true;
            this.list_gender.Name = "list_gender";
            this.list_gender.Size = new System.Drawing.Size(254, 78);
            this.list_gender.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(54, 522);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "上次修改";
            // 
            // txt_LMT
            // 
            this.txt_LMT.Location = new System.Drawing.Point(136, 516);
            this.txt_LMT.Name = "txt_LMT";
            this.txt_LMT.ReadOnly = true;
            this.txt_LMT.Size = new System.Drawing.Size(373, 22);
            this.txt_LMT.TabIndex = 16;
            this.txt_LMT.TabStop = false;
            // 
            // list_manufact
            // 
            this.list_manufact.ColumnWidth = 80;
            this.list_manufact.Font = new System.Drawing.Font("Arial Narrow", 15F);
            this.list_manufact.FormattingEnabled = true;
            this.list_manufact.ItemHeight = 24;
            this.list_manufact.Location = new System.Drawing.Point(911, 160);
            this.list_manufact.MultiColumn = true;
            this.list_manufact.Name = "list_manufact";
            this.list_manufact.Size = new System.Drawing.Size(254, 28);
            this.list_manufact.TabIndex = 2;
            // 
            // textB_weight
            // 
            this.textB_weight.Location = new System.Drawing.Point(1009, 204);
            this.textB_weight.Name = "textB_weight";
            this.textB_weight.Size = new System.Drawing.Size(156, 22);
            this.textB_weight.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(924, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "重量(錢)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(924, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 21;
            this.label4.Text = "工資";
            // 
            // textB_cost
            // 
            this.textB_cost.Location = new System.Drawing.Point(1009, 248);
            this.textB_cost.Name = "textB_cost";
            this.textB_cost.Size = new System.Drawing.Size(156, 22);
            this.textB_cost.TabIndex = 4;
            // 
            // btn_stone
            // 
            this.btn_stone.Font = new System.Drawing.Font("Arial Narrow", 24F);
            this.btn_stone.Location = new System.Drawing.Point(646, 375);
            this.btn_stone.Name = "btn_stone";
            this.btn_stone.Size = new System.Drawing.Size(257, 85);
            this.btn_stone.TabIndex = 5;
            this.btn_stone.Text = "石頭";
            this.btn_stone.UseVisualStyleBackColor = true;
            this.btn_stone.Click += new System.EventHandler(this.btn_stone_Click);
            // 
            // textB_comment
            // 
            this.textB_comment.Location = new System.Drawing.Point(927, 341);
            this.textB_comment.Multiline = true;
            this.textB_comment.Name = "textB_comment";
            this.textB_comment.Size = new System.Drawing.Size(238, 249);
            this.textB_comment.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(924, 322);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 16);
            this.label7.TabIndex = 25;
            this.label7.Text = "備註";
            // 
            // Upload
            // 
            this.Upload.Font = new System.Drawing.Font("PMingLiU", 15.75F);
            this.Upload.Location = new System.Drawing.Point(819, 632);
            this.Upload.Name = "Upload";
            this.Upload.Size = new System.Drawing.Size(171, 59);
            this.Upload.TabIndex = 7;
            this.Upload.Text = "上傳";
            this.Upload.UseVisualStyleBackColor = true;
            this.Upload.Click += new System.EventHandler(this.Upload_Click);
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Sanpya", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(646, 466);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(257, 124);
            this.textBox3.TabIndex = 28;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "石頭資訊:";
            // 
            // status
            // 
            this.status.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.status.BackColor = System.Drawing.Color.Cornsilk;
            this.status.Font = new System.Drawing.Font("PMingLiU", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.status.Location = new System.Drawing.Point(1055, 12);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(110, 42);
            this.status.TabIndex = 29;
            // 
            // btn_pre
            // 
            this.btn_pre.Font = new System.Drawing.Font("PMingLiU", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_pre.Location = new System.Drawing.Point(628, 632);
            this.btn_pre.Name = "btn_pre";
            this.btn_pre.Size = new System.Drawing.Size(185, 59);
            this.btn_pre.TabIndex = 30;
            this.btn_pre.Text = "上一筆";
            this.btn_pre.UseVisualStyleBackColor = true;
            this.btn_pre.Click += new System.EventHandler(this.btn_pre_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(20, 591);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 31;
            this.label8.Text = "預覽圖";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(20, 622);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 16);
            this.label9.TabIndex = 32;
            this.label9.Text = "3dm模型";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(22, 655);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 16);
            this.label10.TabIndex = 33;
            this.label10.Text = "stl模型";
            // 
            // txtB_preimage
            // 
            this.txtB_preimage.Location = new System.Drawing.Point(123, 585);
            this.txtB_preimage.Name = "txtB_preimage";
            this.txtB_preimage.ReadOnly = true;
            this.txtB_preimage.Size = new System.Drawing.Size(321, 22);
            this.txtB_preimage.TabIndex = 34;
            this.txtB_preimage.TabStop = false;
            // 
            // txtB_3dm
            // 
            this.txtB_3dm.Location = new System.Drawing.Point(123, 616);
            this.txtB_3dm.Name = "txtB_3dm";
            this.txtB_3dm.ReadOnly = true;
            this.txtB_3dm.Size = new System.Drawing.Size(321, 22);
            this.txtB_3dm.TabIndex = 35;
            this.txtB_3dm.TabStop = false;
            // 
            // txtB_stl
            // 
            this.txtB_stl.Location = new System.Drawing.Point(123, 649);
            this.txtB_stl.Name = "txtB_stl";
            this.txtB_stl.ReadOnly = true;
            this.txtB_stl.Size = new System.Drawing.Size(321, 22);
            this.txtB_stl.TabIndex = 36;
            this.txtB_stl.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(450, 585);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 37;
            this.button1.Text = "瀏覽...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(450, 615);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 38;
            this.button2.Text = "瀏覽...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(450, 649);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 39;
            this.button3.Text = "瀏覽...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 723);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtB_stl);
            this.Controls.Add(this.txtB_3dm);
            this.Controls.Add(this.txtB_preimage);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_pre);
            this.Controls.Add(this.status);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.Upload);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textB_comment);
            this.Controls.Add(this.btn_stone);
            this.Controls.Add(this.textB_cost);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textB_weight);
            this.Controls.Add(this.list_manufact);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_LMT);
            this.Controls.Add(this.list_gender);
            this.Controls.Add(this.list_category);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.GetNext);
            this.Controls.Add(this.modelID);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.imgBox);
            this.Controls.Add(this.textBox1);
            this.Name = "Form2";
            this.Text = "入庫程式1.0";
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox imgBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.TextBox modelID;
        private System.Windows.Forms.Button GetNext;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox list_category;
        private System.Windows.Forms.ListBox list_gender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_LMT;
        private System.Windows.Forms.ListBox list_manufact;
        private System.Windows.Forms.TextBox textB_weight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textB_cost;
        private System.Windows.Forms.Button btn_stone;
        private System.Windows.Forms.TextBox textB_comment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Upload;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox status;
        private System.Windows.Forms.Button btn_pre;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtB_preimage;
        private System.Windows.Forms.TextBox txtB_3dm;
        private System.Windows.Forms.TextBox txtB_stl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}