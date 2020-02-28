namespace Session4
{
    partial class InventoryReport
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbWarehouse = new System.Windows.Forms.ComboBox();
            this.rbCurrent = new System.Windows.Forms.RadioButton();
            this.rbReceived = new System.Windows.Forms.RadioButton();
            this.rbOut = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PartName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceivedStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutOfStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Warehouse: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOut);
            this.groupBox1.Controls.Add(this.rbReceived);
            this.groupBox1.Controls.Add(this.rbCurrent);
            this.groupBox1.Location = new System.Drawing.Point(419, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 70);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inventory Type: ";
            // 
            // cbWarehouse
            // 
            this.cbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWarehouse.FormattingEnabled = true;
            this.cbWarehouse.Location = new System.Drawing.Point(15, 29);
            this.cbWarehouse.Name = "cbWarehouse";
            this.cbWarehouse.Size = new System.Drawing.Size(398, 24);
            this.cbWarehouse.TabIndex = 2;
            this.cbWarehouse.SelectedIndexChanged += new System.EventHandler(this.cbWarehouse_SelectedIndexChanged);
            // 
            // rbCurrent
            // 
            this.rbCurrent.AutoSize = true;
            this.rbCurrent.Location = new System.Drawing.Point(6, 23);
            this.rbCurrent.Name = "rbCurrent";
            this.rbCurrent.Size = new System.Drawing.Size(115, 21);
            this.rbCurrent.TabIndex = 0;
            this.rbCurrent.Text = "Current Stock";
            this.rbCurrent.UseVisualStyleBackColor = true;
            this.rbCurrent.CheckedChanged += new System.EventHandler(this.rbCurrent_CheckedChanged);
            // 
            // rbReceived
            // 
            this.rbReceived.AutoSize = true;
            this.rbReceived.Location = new System.Drawing.Point(127, 23);
            this.rbReceived.Name = "rbReceived";
            this.rbReceived.Size = new System.Drawing.Size(127, 21);
            this.rbReceived.TabIndex = 3;
            this.rbReceived.Text = "Received Stock";
            this.rbReceived.UseVisualStyleBackColor = true;
            this.rbReceived.CheckedChanged += new System.EventHandler(this.rbReceived_CheckedChanged);
            // 
            // rbOut
            // 
            this.rbOut.AutoSize = true;
            this.rbOut.Location = new System.Drawing.Point(260, 23);
            this.rbOut.Name = "rbOut";
            this.rbOut.Size = new System.Drawing.Size(107, 21);
            this.rbOut.TabIndex = 4;
            this.rbOut.Text = "Out of Stock";
            this.rbOut.UseVisualStyleBackColor = true;
            this.rbOut.CheckedChanged += new System.EventHandler(this.rbOut_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Result: ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartName,
            this.CurrentStock,
            this.ReceivedStock,
            this.OutOfStock,
            this.Action});
            this.dataGridView1.Location = new System.Drawing.Point(12, 100);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(794, 338);
            this.dataGridView1.TabIndex = 4;
            // 
            // PartName
            // 
            this.PartName.HeaderText = "Part Name";
            this.PartName.MinimumWidth = 6;
            this.PartName.Name = "PartName";
            this.PartName.ReadOnly = true;
            this.PartName.Width = 96;
            // 
            // CurrentStock
            // 
            this.CurrentStock.HeaderText = "Current Stock";
            this.CurrentStock.MinimumWidth = 6;
            this.CurrentStock.Name = "CurrentStock";
            this.CurrentStock.ReadOnly = true;
            this.CurrentStock.Width = 113;
            // 
            // ReceivedStock
            // 
            this.ReceivedStock.HeaderText = "Received Stock";
            this.ReceivedStock.MinimumWidth = 6;
            this.ReceivedStock.Name = "ReceivedStock";
            this.ReceivedStock.ReadOnly = true;
            this.ReceivedStock.Width = 124;
            // 
            // OutOfStock
            // 
            this.OutOfStock.HeaderText = "Out Of Stock";
            this.OutOfStock.MinimumWidth = 6;
            this.OutOfStock.Name = "OutOfStock";
            this.OutOfStock.ReadOnly = true;
            this.OutOfStock.Width = 109;
            // 
            // Action
            // 
            this.Action.HeaderText = "Action";
            this.Action.MinimumWidth = 6;
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            this.Action.Width = 76;
            // 
            // InventoryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbWarehouse);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "InventoryReport";
            this.Text = "Inventory Report";
            this.Load += new System.EventHandler(this.InventoryReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbOut;
        private System.Windows.Forms.RadioButton rbReceived;
        private System.Windows.Forms.RadioButton rbCurrent;
        private System.Windows.Forms.ComboBox cbWarehouse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivedStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutOfStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
    }
}