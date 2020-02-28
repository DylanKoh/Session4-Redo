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
    public partial class InventoryManagement : Form
    {
        public InventoryManagement()
        {
            InitializeComponent();
        }

        private void InventoryManagement_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            dataGridView1.Rows.Clear();
            using (var context = new Session4Entities())
            {
                var getOrderItems = (from x in context.OrderItems
                                     orderby x.Order.Date
                                     orderby x.Order.TransactionTypeID
                                     select x);

                var EditColumn = new DataGridViewLinkColumn()
                {
                    Name = "Edit",
                    HeaderText = "Edit",
                    UseColumnTextForLinkValue = true,
                    Text = "Edit"
                };
                var RemoveColumn = new DataGridViewLinkColumn()
                {
                    Name = "Remove",
                    HeaderText = "Remove",
                    UseColumnTextForLinkValue = true,
                    Text = "Remove"

                };
                dataGridView1.Columns.Add(EditColumn);
                dataGridView1.Columns.Add(RemoveColumn);

                foreach (var item in getOrderItems)
                {
                    var rows = new List<string>()
                    {
                        item.Part.Name, item.Order.TransactionType.Name, item.Order.Date.ToString("dd/MM/yyyy"),
                        item.Amount.ToString()
                    };

                    var getSourceWarehouse = (from x in context.Warehouses
                                              where x.ID == item.Order.SourceWarehouseID
                                              select x.Name).FirstOrDefault();

                    var getDestinationWarehouse = (from x in context.Warehouses
                                                   where x.ID == item.Order.DestinationWarehouseID
                                                   select x.Name).FirstOrDefault();

                    if (getSourceWarehouse == null)
                    {
                        rows.Add("");
                        rows.Add(getDestinationWarehouse);
                    }
                    else
                    {
                        rows.Add(getSourceWarehouse);
                        rows.Add(getDestinationWarehouse);
                    }
                    rows.Add(item.ID.ToString());
                    rows.Add(item.OrderID.ToString());
                    dataGridView1.Rows.Add(rows.ToArray());
                }
                
            }
        }

        private void purchaseOrderManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new PurchaseOrder(0)).ShowDialog();
            dataGridView1.Columns.Remove(dataGridView1.Columns["Edit"]);
            dataGridView1.Columns.Remove(dataGridView1.Columns["Remove"]);
            LoadGrid();
        }

        private void warehouseManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new WarehouseManagement(0)).ShowDialog();
            dataGridView1.Columns.Remove(dataGridView1.Columns["Edit"]);
            dataGridView1.Columns.Remove(dataGridView1.Columns["Remove"]);
            LoadGrid();
        }

        private void inventoryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new InventoryReport()).ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                var getOrderID = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["OrderID"].Value);
                if (dataGridView1.Rows[e.RowIndex].Cells["TransactionType"].Value.ToString() == "Purchase Order")
                {
                    (new PurchaseOrder(getOrderID)).ShowDialog();
                }
                else
                {
                    (new WarehouseManagement(getOrderID)).ShowDialog();
                }
                
                dataGridView1.Columns.Remove(dataGridView1.Columns["Edit"]);
                dataGridView1.Columns.Remove(dataGridView1.Columns["Remove"]);
                LoadGrid();
            }
            else if (e.ColumnIndex == 9)
            {
                var checkPromt = MessageBox.Show("Remove", "Are you sure you want to delete this Order Item?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (checkPromt == DialogResult.Yes)
                {
                    using (var context = new Session4Entities())
                    {
                        var OrderItemID = Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells["OrderItemID"].Value);
                        var getOrderItemToDelete = (from x in context.OrderItems
                                                    where x.ID == OrderItemID
                                                    select x).FirstOrDefault();
                        var getCountOfPart = (from x in context.OrderItems
                                              where x.PartID == getOrderItemToDelete.PartID
                                              select x.Amount).Count();
                        var getMinimumAmount = (from x in context.Parts
                                                where x.ID == getOrderItemToDelete.PartID
                                                select x.MinimumAmount).FirstOrDefault();
                        if (getCountOfPart - getOrderItemToDelete.Amount > getMinimumAmount)
                        {
                            context.OrderItems.Remove(getOrderItemToDelete);
                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                            context.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Remove", "Unable to remove item as the inventory will have less than the required minimum amount of the part!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                    }
                }
            }
        }
    }
}
