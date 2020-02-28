﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session4
{
    public partial class WarehouseManagement : Form
    {
        long _OrderID = 0;
        public WarehouseManagement(long orderID)
        {
            InitializeComponent();
            _OrderID = orderID;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadData()
        {
            var RemoveColumn = new DataGridViewLinkColumn()
            {
                Text = "Remove",
                Name = "Action",
                HeaderText = "Action",
                UseColumnTextForLinkValue = true
            };
            dataGridView1.Columns.Add(RemoveColumn);
            using (var context = new Session4Entities())
            {
                var getWarehouses = (from x in context.Warehouses
                                     select x.Name).Distinct().ToArray();
                cbSourceWarehouse.Items.AddRange(getWarehouses);
                cbDestinationWarehouse.Items.AddRange(getWarehouses);

                var getParts = (from x in context.Parts
                                select x.Name).Distinct().ToArray();
                cbPartName.Items.AddRange(getParts);
                if (_OrderID != 0)
                {
                    var getData = (from x in context.Orders
                                   where x.ID == _OrderID
                                   select x).FirstOrDefault();
                    cbSourceWarehouse.SelectedItem = context.Warehouses
                        .Where(x => x.ID == getData.SourceWarehouseID)
                        .Select(x => x.Name).FirstOrDefault();
                    cbDestinationWarehouse.SelectedItem = context.Warehouses
                        .Where(x => x.ID == getData.DestinationWarehouseID)
                        .Select(x => x.Name).FirstOrDefault();
                    dtpDate.Value = getData.Date;

                    var getPartsOfOrder = (from x in context.OrderItems
                                           where x.OrderID == _OrderID
                                           select x);
                    foreach (var item in getPartsOfOrder)
                    {
                        var rows = new List<string>()
                        {
                            item.Part.Name, item.BatchNumber, item.Amount.ToString(), item.PartID.ToString()
                        };
                        dataGridView1.Rows.Add(rows.ToArray());
                    }
                }
            }

        }

        private void WarehouseManagement_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Remove"].Index)
            {
                var checkPromt = MessageBox.Show("Remove", "Are you sure you want to delete this Part?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (checkPromt == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var context = new Session4Entities())
            {
                var checkBatchNumberRequired = (from x in context.Parts
                                                where x.Name == cbPartName.SelectedItem.ToString()
                                                select x.BatchNumberHasRequired).FirstOrDefault();
                if (checkBatchNumberRequired == true && txtBatchNumber.Text.Trim() == "")
                {
                    MessageBox.Show("Add to list", "Please enter a batch number! Selected part requires a batch number",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (nudAmount.Value == 0)
                {
                    MessageBox.Show("Add to list", "Please enter a valid amount!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    var addToList = false;
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        if (item.Cells[0].Value.ToString() == cbPartName.SelectedItem.ToString() && item.Cells[1].Value.ToString() == txtBatchNumber.Text.Trim())
                        {
                            item.Cells["Amount"].Value = Convert.ToDecimal(item.Cells["Amount"].Value) + nudAmount.Value;
                            addToList = true;
                            txtBatchNumber.Text = string.Empty;
                            nudAmount.Value = decimal.Zero;
                            break;

                        }
                    }
                    if (!addToList)
                    {
                        var rows = new List<string>()
                        {
                            cbPartName.SelectedItem.ToString(), txtBatchNumber.Text.Trim(),
                            nudAmount.Value.ToString(), context.Parts
                            .Where(x => x.Name == cbPartName.SelectedItem.ToString())
                            .Select(x => x.ID).FirstOrDefault().ToString()
                        };
                        dataGridView1.Rows.Add(rows.ToArray());
                        txtBatchNumber.Text = string.Empty;
                        nudAmount.Value = decimal.Zero;
                    }

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cbSourceWarehouse.SelectedItem.ToString() == cbDestinationWarehouse.SelectedItem.ToString())
            {
                MessageBox.Show("Submit", "Please make sure the Source and Destination are not the same!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cbDestinationWarehouse.SelectedItem == null || cbSourceWarehouse.SelectedItem == null)
            {
                MessageBox.Show("Submit", "Please select your Source and Destination!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (var context = new Session4Entities())
                {
                    if (_OrderID != 0)
                    {
                        var getOrderItem = (from x in context.OrderItems
                                            where x.OrderID == _OrderID
                                            select x);
                        foreach (var item in getOrderItem)
                        {
                            context.OrderItems.Remove(item);
                        }

                        var updateOrder = (from x in context.Orders
                                           where x.ID == _OrderID
                                           select x).FirstOrDefault();
                        updateOrder.Date = dtpDate.Value;
                        updateOrder.DestinationWarehouseID = context.Warehouses.Where(x => x.Name == cbDestinationWarehouse.SelectedItem.ToString()).Select(x => x.ID).FirstOrDefault();
                        updateOrder.SourceWarehouseID = context.Warehouses.Where(x => x.Name == cbSourceWarehouse.SelectedItem.ToString()).Select(x => x.ID).FirstOrDefault();

                        foreach (DataGridViewRow item in dataGridView1.Rows)
                        {
                            context.OrderItems.Add(new OrderItem()
                            {
                                OrderID = _OrderID,
                                BatchNumber = item.Cells["BatchNumber"].Value.ToString(),
                                PartID = Convert.ToInt64(item.Cells["PartID"].Value),
                                Amount = Convert.ToDecimal(item.Cells["Amount"].Value)
                            });
                        }
                        context.SaveChanges();
                        Close();
                    }
                    else
                    {
                        context.Orders.Add(new Order()
                        {
                            Date = dtpDate.Value,
                            TransactionTypeID = 2,
                            DestinationWarehouseID = context.Warehouses.Where(x => x.Name == cbDestinationWarehouse.SelectedItem.ToString()).Select(x => x.ID).FirstOrDefault(),
                            SourceWarehouseID = context.Warehouses.Where(x => x.Name == cbSourceWarehouse.SelectedItem.ToString()).Select(x => x.ID).FirstOrDefault(),

                        });
                        context.SaveChanges();
                        var getLatestOrderID = (from x in context.Orders
                                                orderby x.ID descending
                                                select x.ID).FirstOrDefault() + 1;
                        foreach (DataGridViewRow item in dataGridView1.Rows)
                        {
                            context.OrderItems.Add(new OrderItem()
                            {
                                OrderID = getLatestOrderID,
                                BatchNumber = item.Cells["BatchNumber"].Value.ToString(),
                                PartID = Convert.ToInt64(item.Cells["PartID"].Value),
                                Amount = Convert.ToDecimal(item.Cells["Amount"].Value)
                            });
                        }
                        context.SaveChanges();
                        Close();
                    }

                }
            }
            
        }
    }
}
