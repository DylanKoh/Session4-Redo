using System;
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
    public partial class InventoryReport : Form
    {
        public InventoryReport()
        {
            InitializeComponent();
        }

        private void InventoryReport_Load(object sender, EventArgs e)
        {
            using (var context = new Session4Entities())
            {
                var getWarehouses = (from x in context.Warehouses
                                     select x.Name).Distinct().ToArray();
                cbWarehouse.Items.AddRange(getWarehouses);
            }
            rbCurrent.Checked = false;
            rbReceived.Checked = false;
            rbOut.Checked = false;

            rbCurrent.Enabled = false;
            rbReceived.Enabled = false;
            rbOut.Enabled = false;
        }
        private void LoadResults()
        {

            using (var context = new Session4Entities())
            {
                var getWarehouseID = (from x in context.Warehouses
                                      where x.Name == cbWarehouse.SelectedItem.ToString()
                                      select x.ID).FirstOrDefault();
                if (rbCurrent.Checked)
                {
                    var getOrders = (from x in context.OrderItems
                                     where x.Order.TransactionTypeID == 1 && x.Order.DestinationWarehouseID == getWarehouseID
                                     group x by x.Part.Name into y
                                     select y);

                    foreach (var item in getOrders)
                    {
                        var checkBatch = (from x in context.Parts
                                          where x.Name == item.Key
                                          select x.BatchNumberHasRequired).FirstOrDefault();
                        if (checkBatch == true)
                        {
                            var rows = new List<string>()
                            {
                                item.Key, item.Count().ToString(), "", "", "View Batch Numbers"
                            };
                            dataGridView1.Rows.Add(rows.ToArray());
                        }
                        else
                        {
                            var rows = new List<string>()
                            {
                                item.Key, item.Count().ToString(), "", "", ""
                            };
                            dataGridView1.Rows.Add(rows.ToArray());
                        }

                    }
                }
                else if (rbReceived.Checked)
                {
                    var getOrders = (from x in context.OrderItems
                                     where x.Order.TransactionTypeID == 2 && x.Order.DestinationWarehouseID == getWarehouseID
                                     group x by x.Part.Name into y
                                     select y);
                    foreach (var item in getOrders)
                    {
                        var checkBatch = (from x in context.Parts
                                          where x.Name == item.Key
                                          select x.BatchNumberHasRequired).FirstOrDefault();
                        if (checkBatch == true)
                        {
                            var rows = new List<string>()
                            {
                                item.Key, "", item.Count().ToString(), "", "View Batch Numbers"
                            };
                            dataGridView1.Rows.Add(rows.ToArray());
                        }
                        else
                        {
                            var rows = new List<string>()
                            {
                                item.Key, "", item.Count().ToString(), "", ""
                            };
                            dataGridView1.Rows.Add(rows.ToArray());
                        }
                        
                    }
                }
                else
                {
                    var getOrders = (from x in context.OrderItems
                                     where x.Order.TransactionTypeID == 2 && x.Order.SourceWarehouseID == getWarehouseID
                                     group x by x.Part.Name into y
                                     select y);
                    foreach (var item in getOrders)
                    {
                        var checkBatch = (from x in context.Parts
                                          where x.Name == item.Key
                                          select x.BatchNumberHasRequired).FirstOrDefault();
                        if (checkBatch == true)
                        {
                            var rows = new List<string>()
                            {
                                item.Key, "", "",item.Count().ToString(), "View Batch Numbers"
                            };
                            dataGridView1.Rows.Add(rows.ToArray());
                        }
                        else
                        {
                            var rows = new List<string>()
                            {
                                item.Key, "", "",item.Count().ToString(), ""
                            };
                            dataGridView1.Rows.Add(rows.ToArray());
                        }

                    }
                }

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Cells["Action"].Value.ToString() == "View Batch Numbers")
                    {
                        item.Cells["Action"].Style.ForeColor = Color.Blue;
                    }
                }
                dataGridView1.Columns["Action"].DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Underline);
                
            }
        }

        private void rbCurrent_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCurrent.Checked)
            {
                dataGridView1.Columns["ReceivedStock"].Visible = false;
                dataGridView1.Columns["OutOfStock"].Visible = false;
                dataGridView1.Columns["CurrentStock"].Visible = true;
                dataGridView1.Rows.Clear();
                LoadResults();
            }
        }

        private void rbReceived_CheckedChanged(object sender, EventArgs e)
        {
            if (rbReceived.Checked)
            {
                dataGridView1.Columns["ReceivedStock"].Visible = true;
                dataGridView1.Columns["OutOfStock"].Visible = false;
                dataGridView1.Columns["CurrentStock"].Visible = false;
                dataGridView1.Rows.Clear();
                LoadResults();
            }
        }

        private void rbOut_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOut.Checked)
            {
                dataGridView1.Columns["ReceivedStock"].Visible = false;
                dataGridView1.Columns["OutOfStock"].Visible = true;
                dataGridView1.Columns["CurrentStock"].Visible = false;
                dataGridView1.Rows.Clear();
                LoadResults();
            }
        }

        private void cbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbCurrent.Enabled = true;
            rbReceived.Enabled = true;
            rbOut.Enabled = true;

            rbCurrent.Checked = false;
            rbReceived.Checked = false;
            rbOut.Checked = false;
        }
    }
}
