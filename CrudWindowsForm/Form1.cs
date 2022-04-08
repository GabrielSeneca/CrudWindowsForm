using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudWindowsForm.Models;

namespace CrudWindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Refrescar();
        }

        // Para no repetir codigo
        #region Helper 
        private void Refrescar()
        {
            using (CRUDEntities db = new CRUDEntities())
            {
                var lista = from d in db.CRUD
                            select d;
                dataGridView1.DataSource = lista.ToList();
            }
        }

        private int? GetId()
        {
            try
            {
                return int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }
        #endregion

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Presentation.FormTabla oFormTabla = new Presentation.FormTabla();
            oFormTabla.ShowDialog();

            Refrescar();
        }

        private void txtEditar_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if (id != null)
            {
                Presentation.FormTabla oFormTabla= new Presentation.FormTabla(id);
                oFormTabla.ShowDialog();

                Refrescar();

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? id = GetId();
            if (id != null)
            {
                using (CRUDEntities db = new CRUDEntities())
                {
                    tabla oTabla = db.CRUD.Find(id);
                    db.CRUD.Remove(oTabla);

                    db.SaveChanges();
                }

                Refrescar();

            }
        }
    }
}
