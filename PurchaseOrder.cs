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
    public partial class PurchaseOrder : Form
    {
        long _OrderID = 0;
        public PurchaseOrder(long orderID)
        {
            InitializeComponent();
            _OrderID = orderID;
        }

        private void PurchaseOrder_Load(object sender, EventArgs e)
        {
            LoadData();
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
                var getSuppliers = (from x in context.Suppliers
                                    select x.Name).Distinct().ToArray();
                cbSuppliers.Items.AddRange(getSuppliers);

                var getWarehouses = (from x in context.Warehouses
                                     select x.Name).Distinct().ToArray();
                cbWarehouse.Items.AddRange(getWarehouses);

                var getParts = (from x in context.Parts
                                select x.Name).Distinct().ToArray();
                cbPartName.Items.AddRange(getParts);
                if (_OrderID != 0)
                {
                    var getData = (from x in context.Orders
                                   where x.ID == _OrderID
                                   select x).FirstOrDefault();
                    cbSuppliers.SelectedItem = getData.Supplier.Name;
                    cbWarehouse.SelectedItem = context.Warehouses
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
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
                    updateOrder.SupplierID = context.Suppliers.Where(x => x.Name == cbSuppliers.SelectedItem.ToString()).Select(x => x.ID).FirstOrDefault();
                    updateOrder.Date = dtpDate.Value;
                    updateOrder.DestinationWarehouseID = context.Warehouses.Where(x => x.Name == cbWarehouse.SelectedItem.ToString()).Select(x => x.ID).FirstOrDefault();

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
                        SupplierID = context.Suppliers.Where(x => x.Name == cbSuppliers.SelectedItem.ToString()).Select(x => x.ID).FirstOrDefault(),
                        TransactionTypeID = 1,
                        DestinationWarehouseID = context.Warehouses.Where(x => x.Name == cbWarehouse.SelectedItem.ToString()).Select(x => x.ID).FirstOrDefault(),
                        SourceWarehouseID = null

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
    }
}
