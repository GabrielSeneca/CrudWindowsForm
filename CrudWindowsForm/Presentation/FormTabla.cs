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

namespace CrudWindowsForm.Presentation
{
    public partial class FormTabla : Form
    {
        public int? id;
        tabla oTabla=null;
        public FormTabla(int? id = null)
        {
            InitializeComponent();

            this.id = id;
            if (id != null)
                CargaDatos();
        }

        private void CargaDatos()
        {
            using (CRUDEntities db = new CRUDEntities())
            {
                oTabla = db.CRUD.Find(id);
                txtNombre.Text = oTabla.nombre;
                txtCorreo.Text = oTabla.correo;
                dtpFechaNac.Value = oTabla.fecha_nacimiento;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (CRUDEntities db = new CRUDEntities())
            {
                if(id == null)
                    oTabla = new tabla();

                oTabla.nombre = txtNombre.Text;
                oTabla.fecha_nacimiento = dtpFechaNac.Value;
                oTabla.correo = txtCorreo.Text;

                if (id == null)
                    db.CRUD.Add(oTabla);
                else
                {
                    db.Entry(oTabla).State= System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();

                this.Close();

            }
        }

        private void FormTabla_Load(object sender, EventArgs e)
        {

        }
    }
}
